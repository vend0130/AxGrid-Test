using System.Collections.Generic;
using AxGrid.FSM;
using Result.Task2.Code.View;

namespace Result.Task2.Code.States
{
    [State(nameof(BootstrapState))]
    public class BootstrapState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Model.Set(Keys.Counter, 0);

            Model.Set(Keys.FirstCollection, new List<CardView>());
            Model.Set(Keys.SecondCollection, new List<CardView>());

            Parent.Change(nameof(DormantState));
        }
    }
}