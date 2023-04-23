using System.Collections.Generic;
using AxGrid.FSM;
using Result.Task2.Code.Extensions;
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
            Model.Set(Keys.DrawCardButton, false);
            Create();
            Parent.Change(nameof(DormantState)); //TODO: move state
        }

        private void Create()
        {
            int counter = Model.GetInt(Keys.Counter);
            counter++;

            GameObject prefab = _cards.GetRandomElement();
            GameObject cardObject = Object.Instantiate(prefab);

            Model.Set(Keys.Counter, counter);
        }
    }
}