using AxGrid.FSM;
using AxGrid.Model;

namespace Result.Task2.Code.States
{
    [State(nameof(MoveState))]
    public class MoveState : FSMState
    {
        [Enter]
        private void EnterThis() =>
            Model.Set(Keys.DrawCardButton, false);

        [Bind(Keys.CardEndMove)]
        private void EndMove() =>
            Parent.Change(nameof(DormantState));
    }
}