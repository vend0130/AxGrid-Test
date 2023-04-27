using System.Collections.Generic;
using AxGrid.FSM;
using Result.Task2.Code.Data;
using Result.Task2.Code.Extensions;
using Result.Task2.Code.View;
using UnityEngine;

namespace Result.Task2.Code.States
{
    [State(nameof(FactoryState))]
    public class FactoryState : FSMState
    {
        private readonly List<GameObject> _cards;

        public FactoryState(List<GameObject> cards) =>
            _cards = cards;

        [Enter]
        private void EnterThis()
        {
            if (Model.Get<List<CardView>>(Keys.FirstCollection).Count >= GameData.MaxCardInCollection)
            {
                Parent.Change(nameof(DormantState));
                return;
            }

            Model.Set(Keys.CurrentCollection, Keys.FirstCollection);
            Model.Set(Keys.DrawCardButton, false);

            Create();
            Parent.Change(nameof(MoveState));
        }

        private void Create()
        {
            int counter = Model.GetInt(Keys.Counter);

            GameObject prefab = _cards.GetRandomElement();
            CardView card = Object.Instantiate(prefab).GetComponent<CardView>();

            card.InitId($"Card_{counter}");
            card.gameObject.SetActive(false);
            Model.Get<List<CardView>>(Keys.FirstCollection).Add(card);

            counter++;
            Model.Set(Keys.Counter, counter);
        }
    }
}