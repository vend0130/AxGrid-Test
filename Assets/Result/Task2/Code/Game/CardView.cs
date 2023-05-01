using System;
using AxGrid;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Result.Task2.Code.Game
{
    [RequireComponent(typeof(SortingGroup))]
    [RequireComponent(typeof(Collider2D))]
    public class CardView : MonoBehaviourExt, IPointerClickHandler
    {
        public string Id { get; private set; }
        public event Action<CardView> OnClickHandler;

        private const float TimeMove = .2f;

        private SortingGroup _sortingGroup;
        private Collider2D _collider;

        public void Init(string id)
        {
            Id = id;
            gameObject.SetActive(false);
        }

        [OnAwake]
        private void AwakeThis()
        {
            _sortingGroup = GetComponent<SortingGroup>();
            _collider = GetComponent<Collider2D>();
        }

        public void MoveTo(Vector2 targetPoint, int order, bool callBack, bool colliderEnable)
        {
            ResetPath();
            ActivateObject();
            ChangeColliderState(false);
            ChangeSortOrder(callBack ? -1 : order);

            Vector2 currentPoint = transform.position;

            Path
                .EasingLinear(TimeMove, 0, 1,
                    (f) => transform.position = Vector2.Lerp(currentPoint, targetPoint, f))
                .Action(() =>
                {
                    ChangeSortOrder(order);
                    ChangeColliderState(colliderEnable);

                    if (callBack)
                        Settings.Fsm.Invoke(Keys.CardEndMove);
                });
        }

        private void ResetPath()
        {
            Path.StopPath();
            Path = new CPath();
        }

        private void ActivateObject() =>
            gameObject.SetActive(true);

        private void ChangeColliderState(bool value) =>
            _collider.enabled = value;

        private void ChangeSortOrder(int value) =>
            _sortingGroup.sortingOrder = value;

        public void OnPointerClick(PointerEventData _) =>
            OnClickHandler?.Invoke(this);
    }
}