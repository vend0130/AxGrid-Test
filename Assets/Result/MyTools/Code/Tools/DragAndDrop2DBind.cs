using System;
using AxGrid;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Result.MyTools.Code.Tools
{
    [RequireComponent(typeof(Collider2D))]
    public class DragAndDrop2DBind : Binder, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private string _fieldName;

        [Header("Scale")] [SerializeField] private Transform _scaleObject;

        [Tooltip("На сколько процент будет увеличен объект при драге")] [Range(0, 100), SerializeField]
        private int _dragScale = 2;

        [Tooltip("Время за которое будет меняться скейл")] [SerializeField]
        private float _duration = .15f;

        private const string NotDragState = "NotDrag";
        private const string BeginDragState = "BeginDrag";
        private const string DragState = "Drag";
        private const string EndDragState = "EndDrag";

        private int? _pointerId;
        private Camera _camera;
        private Collider2D _collider;
        private Vector2 _offset;
        private Vector3 _defaultScale;

        [OnAwake]
        private void AwakeThis()
        {
            try
            {
                _collider = GetComponent<Collider2D>();
            }
            catch (Exception e)
            {
                Log.Error($"Error get Component:{e.Message}");
            }
            
            _fieldName = string.IsNullOrEmpty(_fieldName) ? name : _fieldName;
            _scaleObject = _scaleObject ? _scaleObject : transform;

            _camera = Camera.main;
            _defaultScale = _scaleObject.localScale;

            Model.EventManager.AddParameterAction<bool>($"On{_fieldName}StateChanged", ChangeState);

            CallEvents(NotDragState);
        }

        [OnDestroy]
        private void DestroyThis()
        {
            Model.EventManager.RemoveParameterAction<bool>($"On{_fieldName}StateChanged", ChangeState);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_pointerId != null)
                return;

            _pointerId = eventData.pointerId;
            _offset = (Vector2)transform.position - GetWorldPosition(eventData);
            PlayEffect(_defaultScale + Vector3.one * ((float)_dragScale / 100));

            CallEvents(BeginDragState);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_pointerId == null)
                return;

            transform.position = GetWorldPosition(eventData) + _offset;

            CallEvents(DragState);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_pointerId == null)
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
            PlayEffect(_defaultScale);
            CallEvents(EndDragState);
        }

        private Vector2 GetWorldPosition(PointerEventData eventData) =>
            _camera.ScreenToWorldPoint(eventData.position);

        private void PlayEffect(Vector2 target)
        {
            Path.StopPath();
            Path = new CPath();

            Vector2 current = _scaleObject.localScale;
            Path.EasingLinear(_duration, 0, 1, (f) =>
                _scaleObject.localScale = Vector2.Lerp(current, target, f));
        }

        private void CallEvents(string eventName)
        {
            Settings.Fsm?.Invoke(eventName, _fieldName);
            Settings.Fsm?.Invoke($"{_fieldName}Drag", eventName);
        }
    }
}