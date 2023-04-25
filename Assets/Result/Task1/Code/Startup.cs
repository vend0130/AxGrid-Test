using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using Result.Task1.Code.States;
using UnityEngine;

namespace Result.Task1.Code
{
    public class Startup : MonoBehaviourExt
    {
        [SerializeField] private Color _homeColor;
        [SerializeField] private Color _jobColor;
        [SerializeField] private Color _magazineColor;
        [SerializeField] private Color _defaultColor;

        [OnAwake]
        private void AwakeThis()
        {
            CreateFsm();
            AddColors();
        }

        [OnStart]
        private void StartThis()
        {
            Model.Set(Keys.Bank, 0);
            Settings.Fsm.Start(nameof(BootstrapState));
        }

        [OnUpdate]
        public void UpdateFsm() =>
            Settings.Fsm.Update(Time.deltaTime);

        private void CreateFsm()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new BootstrapState());
            Settings.Fsm.Add(new HomeState());
            Settings.Fsm.Add(new JobState());
            Settings.Fsm.Add(new MagazineState());
            Settings.Fsm.Add(new MoveState());
        }

        private void AddColors()
        {
            Model.Set(nameof(MoveState), _defaultColor);
            Model.Set(nameof(HomeState), _homeColor);
            Model.Set(nameof(JobState), _jobColor);
            Model.Set(nameof(MagazineState), _magazineColor);
        }
    }
}