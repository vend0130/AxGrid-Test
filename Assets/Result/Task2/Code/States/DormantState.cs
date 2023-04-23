using AxGrid;
using AxGrid.FSM;

namespace Result.Task2.Code.States
{
    [State(nameof(DormantState))]
    public class DormantState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Log.Debug("enter dormant");
        }
    }
}