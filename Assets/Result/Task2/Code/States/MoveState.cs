using AxGrid;
using AxGrid.FSM;

namespace Result.Task2.Code.States
{
    [State(nameof(MoveState))]
    public class MoveState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Log.Debug("enter move");
        }
    }
}