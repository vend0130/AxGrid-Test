using System.Collections.Generic;
using AxGrid.FSM;
using Result.Task2.Code.Data;
using UnityEngine;

namespace Result.Task2.Code.States
{
    [State(nameof(BootstrapState))]
    public class BootstrapState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Model.Set(Keys.Counter, 0);

            Model.Set(Keys.FirstCollection, new List<CardData>());
            Model.Set(Keys.SecondCollection, new List<CardData>());

            Model.Set(Keys.FirstDefaultPosition, new Vector2(0, -1.8f));
            Model.Set(Keys.SecondDefaultPosition, new Vector2(0, 2.4f));

            Parent.Change(nameof(DormantState));
        }
    }
}