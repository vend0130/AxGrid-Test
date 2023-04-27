using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using Result.Task2.Code.States;

namespace Result.Task2.Code
{
    public class Startup : MonoBehaviourExt
    {
        [OnAwake]
        private void AwakeThis()
        {
            CreateFsm();
        }

        [OnStart]
        private void StartThis() =>
            Settings.Fsm.Start(nameof(BootstrapState));

        private void CreateFsm()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new BootstrapState());
            Settings.Fsm.Add(new DormantState());
            Settings.Fsm.Add(new ClickOnCardState());
            Settings.Fsm.Add(new ClickOnButtonState());
            Settings.Fsm.Add(new MoveState());
        }
    }
}