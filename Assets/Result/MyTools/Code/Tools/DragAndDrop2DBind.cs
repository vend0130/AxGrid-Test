using System;
using AxGrid;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Result.MyTools.Code.Tools
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SortingGroup))]
    public class DragAndDrop2DBind : Binder, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private string _fieldName;

        [SerializeField] private int _defaultOrder = 1;
        [SerializeField] private int _dragOrder = 2;

        [Header("Scale")] [SerializeField] private Transform _scaleObject;

        [Tooltip("На сколько процент будет увеличен объект при драге")] [Range(0, 100), SerializeField]
        private int _dragScale = 2;

        [Tooltip("Время за которое будет проигрываться анимация скейла и движения")] [SerializeField]
        private float _timePath = .15f;

        private const string NotDragState = "NotDrag";
        private const string BeginDragState = "BeginDrag";
        private const string DragState = "Drag";
        private const string EndDragState = "EndDrag";

        private int? _pointerId;
        private Camera _camera;
        private Collider2D _collider;
        private SortingGroup _sorting;
        private Vector2 _previousPoint;
        private Vector2 _offset;
        private Vector3 _defaultScale;

        private CPath _scalePath;
        private CPath _movePath;

        [OnAwake]
        private void AwakeThis()
        {
            try
            {
                _collider = GetComponent<Collider2D>();
                _sorting = GetComponent<SortingGroup>();
                _camera = Camera.main;
            }
            catch (Exception e)
            {
                Log.Error($"Error get Component:{e.Message}");
            }

            _fieldName = string.IsNullOrEmpty(_fieldName) ? name : _fieldName;
            _scaleObject = _scaleObject ? _scaleObject : transform;
            _defaultScale = _scaleObject.localScale;
            _sorting.sortingOrder = _defaultOrder;

            Model.EventManager.AddParameterAction<bool>($"On{_fieldName}DragStateChanged", ChangeState);
            Model.EventManager.AddAction($"On{_fieldName}FailDrop", FailDrop);

            CallEvents(NotDragState);
        }

        [OnDestroy]
        private void DestroyThis()
        {
            Model.EventManager.RemoveParameterAction<bool>($"On{_fieldName}DragStateChanged", ChangeState);
            Model.EventManager.RemoveAction($"On{_fieldName}FailDrop", FailDrop);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_pointerId != null)
                return;

            _movePath?.StopPath();

            _sorting.sortingOrder = _dragOrder;
            _pointerId = eventData.pointerId;
            _offset = (Vector2)transform.position - GetWorldPosition(eventData);
            _previousPoint = transform.position;
            PlayEffect(_defaultScale + Vector3.one * ((float)_dragScale / 100));

            CallEvents(BeginDragState);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_pointerId == null || _pointerId != eventData.pointerId)
                return;

            transform.position = GetWorldPosition(eventData) + _offset;

            CallEvents(DragState);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_pointerId == null || _pointerId != eventData.pointerId)
                return;

            EndDrag();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                EndDrag();
        }

        private void ChangeState(bool value)
        {
            _collider.enabled = value;
            EndDrag();
        }

        private void EndDrag()
        {
            _pointerId = null;
            _sorting.sortingOrder = _defaultOrder;
            PlayEffect(_defaultScale);
            CallEvents(EndDragState);
        }

        private Vector2 GetWorldPosition(PointerEventData eventData) =>
            _camera.ScreenToWorldPoint(eventData.position);

        private void FailDrop()
        {
            _movePath?.StopPath();
            _movePath = CreateNewPath(); 
            
            Vector2 current = transform.position;
            _movePath.EasingLinear(_timePath, 0, 1, (f) =>
                transform.position = Vector2.Lerp(current, _previousPoint, f));
        }

        private void PlayEffect(Vector2 target)
        {
            _scalePath?.StopPath();
            _scalePath = CreateNewPath();
            
            Vector2 current = _scaleObject.localScale;
            _scalePath.EasingLinear(_timePath, 0, 1, (f) =>
                _scaleObject.localScale = Vector2.Lerp(current, target, f));
        }

        private void CallEvents(string eventName)
        {
            Settings.Fsm?.Invoke(eventName, _fieldName);
            Settings.Fsm?.Invoke($"{_fieldName}Drag", eventName);
        }
    }
}