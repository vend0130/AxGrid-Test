using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using Result.Task1.Code.States;
using UnityEngine;

namespace Result.Task1.Code
{
    public class Bootstrap : MonoBehaviourExt
    {
        [SerializeField] private Transform _character;

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

        private void CreateFsm()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new BootstrapState());
            Settings.Fsm.Add(new HomeState());
            Settings.Fsm.Add(new JobState());
            Settings.Fsm.Add(new MagazineState());
            Settings.Fsm.Add(new MoveState());
        }
    }
}