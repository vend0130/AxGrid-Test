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
        private const float DistanceBetweenCards = 1.8f;
        private const float OffsetY = .15f;

        private readonly Vector2 _defaultPosition = new Vector2(0, -1.8f);

        [Enter]
        private void EnterThis()
        {
            List<CardView> collection = Model.Get<List<CardView>>(Keys.FirstCollection);
            StartMove(collection);
        }

        [Bind(Keys.CardEndMove)]
        private void EndMove() =>
            Parent.Change(nameof(DormantState));

        private void StartMove(List<CardView> collection)
        {
            for (var i = 0; i < collection.Count; i++)
            {
                CardView card = collection[i];

                if (!card.gameObject.activeSelf)
                {
                    card.transform.position = _defaultPosition;
                    card.gameObject.SetActive(true);
                }

                bool last = collection.Count - 1 == i;
                Vector2 targetPosition = GetTargetPosition(collection.Count, i);
                card.MoveTo(targetPosition, i, callBack: last);
            }
        }

        private Vector2 GetTargetPosition(int count, int index)
        {
            Vector2 position = Vector2.zero;

            position.y = Random.Range(_defaultPosition.y - OffsetY, _defaultPosition.y + OffsetY);

            float startPointX = -(float)(count - 1) / 2 * DistanceBetweenCards;
            position.x = startPointX + DistanceBetweenCards * index;

            return position;
        }
    }
}