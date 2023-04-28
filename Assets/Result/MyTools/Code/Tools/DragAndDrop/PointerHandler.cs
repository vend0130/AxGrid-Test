using AxGrid.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Result.MyTools.Code.Tools.DragAndDrop
{
    public abstract class PointerHandler : MonoBehaviourExtBind, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        protected int? pointerId;
        private Camera mainCamera;

        [OnAwake]
        private void AwakeThis() =>
            mainCamera = Camera.main;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (pointerId != null)
                return;

            pointerId = eventData.pointerId;
            OnDown(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (pointerId == null || pointerId != eventData.pointerId)
                return;

            Drag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (pointerId == null || pointerId != eventData.pointerId)
                return;

            OnUp();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                OnUp();
        }

        protected Vector2 GetWorldPosition(PointerEventData eventData) =>
            mainCamera.ScreenToWorldPoint(eventData.position);

        protected abstract void OnDown(PointerEventData eventData);

        protected abstract void Drag(PointerEventData eventData);

        protected abstract void OnUp();
    }
}