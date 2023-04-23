using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using Result.Task2.Code.View;
using UnityEngine;

namespace Result.Task2.Code.States
{
    [State(nameof(MoveState))]
    public class MoveState : FSMState
    {
        private const float DistanceBetweenCards = 1.7f;
        private const float OffsetY = .1f;

        private readonly Vector2 _firstDefaultPosition = new Vector2(0, -1.8f);
        private readonly Vector2 _secondDefaultPosition = new Vector2(0, 2.4f);

        [Enter]
        private void EnterThis()
        {
            bool toFirstCollection = Model.GetString(Keys.CurrentCollection) == Keys.FirstCollection;

            List<CardView> collection = Model.Get<List<CardView>>(Keys.FirstCollection);
            StartMove(collection, _firstDefaultPosition, toFirstCollection);

            if (toFirstCollection)
                return;

            collection = Model?.Get<List<CardView>>(Keys.SecondCollection);
            StartMove(collection, _secondDefaultPosition, true, deactivateCollider: true);
        }

        [Bind(Keys.CardEndMove)]
        private void EndMove() =>
            Parent.Change(nameof(DormantState));

        private void StartMove(List<CardView> collection, Vector2 defaultPosition,
            bool callBack, bool deactivateCollider = false)
        {
            for (var i = 0; i < collection.Count; i++)
            {
                CardView card = collection[i];

                ActivateCard(card, defaultPosition);

                if (deactivateCollider)
                    card.ChangeColliderState();

                bool call = collection.Count - 1 == i && callBack;
                Vector2 targetPosition = GetTargetPosition(collection.Count, i, defaultPosition);
                card.MoveTo(targetPosition, i, callBack: call);
            }
        }

        private void ActivateCard(CardView card, Vector2 defaultPosition)
        {
            if (card.gameObject.activeSelf)
                return;

            card.transform.position = defaultPosition;
            card.gameObject.SetActive(true);
        }

        private Vector2 GetTargetPosition(int count, int index, Vector2 defaultPosition)
        {
            Vector2 position = Vector2.zero;

            position.y = Random.Range(defaultPosition.y - OffsetY, defaultPosition.y + OffsetY);

            float startPointX = -(float)(count - 1) / 2 * DistanceBetweenCards;
            position.x = startPointX + DistanceBetweenCards * index;

            return position;
        }
    }
}