using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using Result.Task1.Code.States;
using UnityEngine;
using UnityEngine.UI;

namespace Result.Task1.Code
{
    public class Bootstrap : MonoBehaviourExt
    {
        [SerializeField] private Transform _character;
        [SerializeField] private Image _imageWithButtons;
        [SerializeField] private Color _homeColor;
        [SerializeField] private Color _jobColor;
        [SerializeField] private Color _magazineColor;
        [SerializeField] private Color _defaultColor;

        [OnAwake]
        private void AwakeThis() =>
            CreateFsm();

        [OnStart]
        private void StartThis()
        {
            Model.Set(Constants.BankKey, 0);
            Settings.Fsm.Start(nameof(BootstrapState));
        }

        [OnUpdate]
        public void UpdateFsm() =>
            Settings.Fsm.Update(Time.deltaTime);

        private void CreateFsm()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new BootstrapState());
            Settings.Fsm.Add(new HomeState(_imageWithButtons, _homeColor));
            Settings.Fsm.Add(new JobState(_imageWithButtons, _jobColor));
            Settings.Fsm.Add(new MagazineState(_imageWithButtons, _magazineColor));
            Settings.Fsm.Add(new MoveState(_imageWithButtons, _defaultColor));
        }
    }
}