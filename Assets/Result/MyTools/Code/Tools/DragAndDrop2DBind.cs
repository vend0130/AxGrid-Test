using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Result.MyTools.Code.Tools
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SortingGroup))]
    public class DragAndDrop2DBind : MonoBehaviourExtBind, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private string _fieldName;
        [SerializeField] private int _defaultOrder = 1;
        [SerializeField] private int _dragOrder = 2;
        [Range(0, 100), SerializeField] private int _dragScale = 2;
        [SerializeField] private float _timePath = .15f;

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
            _collider = GetComponent<Collider2D>();
            _sorting = GetComponent<SortingGroup>();
            _camera = Camera.main;

            _fieldName = string.IsNullOrEmpty(_fieldName) ? name : _fieldName;
            _defaultScale = transform.localScale;
            _sorting.sortingOrder = _defaultOrder;


            CallEvents(NotDragState);
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

        [Bind("On{_fieldName}DragStateChanged")]
        private void ChangeState(bool value)
        {
            _collider.enabled = value;
            EndDrag();
        }

        [Bind("On{_fieldName}FailDrop")]
        private void FailDrop()
        {
            _movePath?.StopPath();
            _movePath = CreateNewPath();

            Vector2 current = transform.position;
            _movePath.EasingLinear(_timePath, 0, 1, (f) =>
                transform.position = Vector2.Lerp(current, _previousPoint, f));
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


        private void PlayEffect(Vector2 target)
        {
            _scalePath?.StopPath();
            _scalePath = CreateNewPath();

            Vector2 current = transform.localScale;
            _scalePath.EasingLinear(_timePath, 0, 1, (f) =>
                transform.localScale = Vector2.Lerp(current, target, f));
        }

        private void CallEvents(string eventName)
        {
            Settings.Fsm?.Invoke(eventName, _fieldName);
            Settings.Fsm?.Invoke($"{_fieldName}Drag", eventName);
        }
    }
}