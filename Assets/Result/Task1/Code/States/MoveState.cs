using System.Collections.Generic;
using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using Result.Task1.Code.Data;
using Result.Task1.Code.States.Base;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(MoveState))]
    public class MoveState : ButtonListener
    {
        private readonly Dictionary<string, string> _states = new Dictionary<string, string>()
        {
            [TargetPointType.Home.ToString()] = nameof(HomeState),
            [TargetPointType.Job.ToString()] = nameof(JobState),
            [TargetPointType.Magazine.ToString()] = nameof(MagazineState),
        };

        private string _targetPointType;


        [Enter]
        private void EnterThis()
        {
            Model.EventManager.Invoke(Keys.EnterState, Model.Get<Color>(nameof(MoveState)));

            _targetPointType = Model.Get<string>(Keys.TargetPoint);
            Vector2 point = Model.Get<Vector2>(_targetPointType);
            Settings.Invoke(Keys.HeroMoveTo, point);
        }

        [Bind(Keys.HeroStop)]
        private void IsStopped() =>
            Parent.Change(_states[_targetPointType]);
    }
}