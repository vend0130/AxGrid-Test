using AxGrid;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Result.Task2.Code.View
{
    public class CardView : MonoBehaviourExt, IPointerClickHandler
    {
        [SerializeField] private SortingGroup _sortingGroup;
        [SerializeField] private Collider2D _collider;

        public string Id { get; private set; }

        private const float TimeMove = .2f;

        public void InitId(string id) =>
            Id = id;

        public void MoveTo(Vector2 targetPosition, int order, bool callBack = false)
        {
            Path.StopPath();
            Path = new CPath();

            _sortingGroup.sortingOrder = callBack ? -1 : order;

            Vector2 currentPoint = transform.position;

            Path
                .EasingLinear(TimeMove, 0, 1,
                    (f) => transform.position = Vector2.Lerp(currentPoint, targetPosition, f))
                .Action(() =>
                {
                    _sortingGroup.sortingOrder = order;

                    if (callBack)
                        Settings.Fsm.Invoke(Keys.CardEndMove);
                });
        }

        public void ChangeColliderState() =>
            _collider.enabled = false;

        public void OnPointerClick(PointerEventData _) =>
            Settings.Fsm.Invoke(Keys.ClickOnCard, Id);
    }
}