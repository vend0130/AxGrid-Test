using AxGrid.FSM;

namespace Result.MyTools.Code.States
{
    [State(nameof(BootstrapState))]
    public class BootstrapState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Model.Set(Keys.ToggleDragFrogIsOn, false);
            Model.Set(Keys.FrogDragState, false);

            Parent.Change(nameof(InputChangeState));
        }
    }
}