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
        [SerializeField] private string objectName;
        [SerializeField] private int defaultOrder = 1;
        [SerializeField] private int dragOrder = 2;
        [Range(0, 100), SerializeField] private int dragScaleInPercents = 25;
        [SerializeField] private float timePath = .15f;

        private const string NotDragState = "NotDrag";
        private const string BeginDragState = "BeginDrag";
        private const string DragState = "Drag";
        private const string EndDragState = "EndDrag";

        private Collider2D currentCollider;
        private SortingGroup sorting;
        private Vector2 previousPoint;
        private Vector2 offset;
        private Vector3 defaultScale;

        private Mover mover;
        private Scaler scaler;

        [OnAwake]
        private void AwakeThis()
        {
            currentCollider = GetComponent<Collider2D>();
            sorting = GetComponent<SortingGroup>();

            objectName = string.IsNullOrEmpty(objectName) ? name : objectName;
            defaultScale = transform.localScale;
            sorting.sortingOrder = defaultOrder;

            mover = gameObject.AddComponent<Mover>();
            scaler = gameObject.AddComponent<Scaler>();

            Model.EventManager.AddParameterAction<bool>($"On{objectName}DragStateChanged", ChangeState);

            CallEvents(NotDragState);
        }

        [OnDestroy]
        private void DestroyThis() =>
            Model.EventManager.RemoveParameterAction<bool>($"On{objectName}DragStateChanged", ChangeState);

        protected override void OnDown(PointerEventData eventData)
        {
            if (mover.CurrentPath != null && mover.CurrentPath.IsPlaying)
            {
                pointerId = null;
                return;
            }

            scaler.Stop();

            sorting.sortingOrder = dragOrder;
            pointerId = eventData.pointerId;

            offset = (Vector2)transform.position - GetWorldPosition(eventData);
            previousPoint = transform.position;

            scaler.Play(timePath, defaultScale + Vector3.one * ((float)dragScaleInPercents / 100));
            CallEvents(BeginDragState);
        }

        protected override void Drag(PointerEventData eventData)
        {
            transform.position = GetWorldPosition(eventData) + offset;
            CallEvents(DragState);
        }

        private void ChangeState(bool value)
        {
            currentCollider.enabled = value;
            OnUp();
        }

        [Bind("{" + nameof(objectName) + "}FailDrop")]
        private void FailDrop() =>
            mover.Play(timePath, previousPoint);

        protected override void OnUp()
        {
            pointerId = null;
            sorting.sortingOrder = defaultOrder;

            scaler.Play(timePath, defaultScale);
            CallEvents(EndDragState);
        }

        private void CallEvents(string eventName)
        {
            Model.Set($"{objectName}DragPosition", (Vector2)transform.position);
            Settings.Fsm?.Invoke(eventName, objectName);
            Settings.Fsm?.Invoke($"{objectName}Drag", eventName);
        }
    }
}