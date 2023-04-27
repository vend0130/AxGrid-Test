using AxGrid.FSM;

namespace Result.MyTools.Code.States
{
    [State(nameof(BootstrapState))]
    public class BootstrapState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Parent.Change(nameof(InputChangeState));
        }
    }
}