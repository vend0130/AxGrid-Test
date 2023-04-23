using AxGrid.FSM;

namespace Result.Task2.Code.States
{
    [State(nameof(BootstrapState))]
    public class BootstrapState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Model.Set(Keys.Counter, 0);
            
            Parent.Change(nameof(DormantState));
        }
    }
}