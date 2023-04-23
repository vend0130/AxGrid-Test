using AxGrid;
using AxGrid.FSM;
using Result.Task1.Code.States.Base;

namespace Result.Task1.Code.States
{
    [State(nameof(JobState))]
    public class JobState : IdleState
    {
        protected override void EnterThis()
        {
            base.EnterThis();
            Log.Debug("enter job state");
        }
    }
}