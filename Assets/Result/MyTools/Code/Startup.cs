using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using Result.MyTools.Code.States;

namespace Result.MyTools.Code
{
    public class Startup : MonoBehaviourExt
    {
        [OnAwake]
        private void AwakeThis()
        {
            CreateFsm();
        }

        [OnStart]
        private void StartThis()
        {
            Settings.Fsm.Start(nameof(BootstrapState));
        }

        private static void CreateFsm()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new BootstrapState());
            Settings.Fsm.Add(new InputChangeState());
            Settings.Fsm.Add(new DragState());
        }
    }
}