using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Result.MyTools.Code.Tools.DragAndDrop
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SortingGroup))]
    public class DragAndDrop2DBind : PointerHandler
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

        private Collider2D _collider;
        private SortingGroup _sorting;
        private Vector2 _previousPoint;
        private Vector2 _offset;
        private Vector3 _defaultScale;

        private Mover _mover;
        private Scaler _scaler;

        [OnAwake]
        private void AwakeThis()
        {
            _collider = GetComponent<Collider2D>();
            _sorting = GetComponent<SortingGroup>();

            _fieldName = string.IsNullOrEmpty(_fieldName) ? name : _fieldName;
            _defaultScale = transform.localScale;
            _sorting.sortingOrder = _defaultOrder;

            _mover = gameObject.AddComponent<Mover>();
            _scaler = gameObject.AddComponent<Scaler>();

            Model.EventManager.AddParameterAction<bool>($"On{_fieldName}DragStateChanged", ChangeState);

            CallEvents(NotDragState);
        }

        [OnDestroy]
        private void DestroyThis() =>
            Model.EventManager.RemoveParameterAction<bool>($"On{_fieldName}DragStateChanged", ChangeState);

        protected override void OnDown(PointerEventData eventData)
        {
            _scaler.Stop();

            _sorting.sortingOrder = _dragOrder;
            pointerId = eventData.pointerId;
            _offset = (Vector2)transform.position - GetWorldPosition(eventData);
            _previousPoint = transform.position;

            _scaler.Play(_timePath, _defaultScale + Vector3.one * ((float)_dragScale / 100));
            CallEvents(BeginDragState);
        }

        protected override void Drag(PointerEventData eventData)
        {
            transform.position = GetWorldPosition(eventData) + _offset;
            CallEvents(DragState);
        }

        private void ChangeState(bool value)
        {
            _collider.enabled = value;
            OnUp();
        }

        [Bind("On{_fieldName}FailDrop")]
        private void FailDrop() =>
            _mover.Play(_timePath, _previousPoint);

        protected override void OnUp()
        {
            pointerId = null;
            _sorting.sortingOrder = _defaultOrder;

            _scaler.Play(_timePath, _defaultScale);
            CallEvents(EndDragState);
        }

        private void CallEvents(string eventName)
        {
            Settings.Fsm?.Invoke(eventName, _fieldName);
            Settings.Fsm?.Invoke($"{_fieldName}Drag", eventName);
        }
    }
}