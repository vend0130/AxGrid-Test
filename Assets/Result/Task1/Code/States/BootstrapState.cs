using System;
using AxGrid;
using AxGrid.FSM;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(BootstrapState))]
    public class BootstrapState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            string defaultKey = Constants.DefaultPointType.ToString();

            Vector2 point = Model.Get<Vector2>(defaultKey);
            Settings.Invoke(Constants.HeroResetBindName, point);

            Model.Set(String.Format(Constants.ButtonFormatKey, defaultKey), false);
            Parent.Change(nameof(HomeState));
        }
    }
}