using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using Result.Task1.Code.Data;
using UnityEngine;

namespace Result.Task1.Code.States.Base
{
    public class ButtonListener : FSMState
    {
        private readonly List<string> _buttonsNames;

        protected ButtonListener()
        {
            _buttonsNames = new List<string>()
            {
                TargetPointType.Home.ToString(),
                TargetPointType.Job.ToString(),
                TargetPointType.Magazine.ToString()
            };
        }

        [Bind("OnBtn")]
        private void Click(string buttonName)
        {
            foreach (string button in _buttonsNames)
            {
                string buttonEnableKey = string.Format(Constants.ButtonFormatKey, button);

                Model.Set(buttonEnableKey, button != buttonName);
            }

            ChangeTargetPoint(buttonName);
            Parent.Change(nameof(MoveState));
        }

        private void ChangeTargetPoint(string name)
        {
            Vector2 point = Model.Get<Vector2>(name);
            Model.Set(Constants.TargetPointKey, point);
        }
    }
}