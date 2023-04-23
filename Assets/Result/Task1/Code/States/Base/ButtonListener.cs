using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using Result.Task1.Code.Data;

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

            Model.Set(Constants.TargetPointKey, buttonName);
            Parent.Change(nameof(MoveState));
        }
    }
}