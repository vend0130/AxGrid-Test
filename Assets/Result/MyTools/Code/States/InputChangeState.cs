using AxGrid.FSM;
using AxGrid.Model;

namespace Result.MyTools.Code.States
{
    [State(nameof(InputChangeState))]
    public class InputChangeState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            SetDataWhenChangedState("Frog", toggle: true, field: !Model.GetBool(Keys.ToggleDragFrogIsOn));
            SetDataWhenChangedState("PinkFin", toggle: true, field: !Model.GetBool(Keys.ToggleDragPinkFinIsOn));
        }

        [Exit]
        private void ExitThis()
        {
            SetDataWhenChangedState("Frog", toggle: false, field: false);
            SetDataWhenChangedState("PinkFin", toggle: false, field: false);
        }

        private void SetDataWhenChangedState(string name, bool toggle, bool field)
        {
            Model.Set($"ToggleDrag{name}Enable", toggle);
            Model.Set($"InptFld{name}NameFieldEnable", field);
        }

        [Bind("BeginDrag")]
        private void BeginDrag(string fieldName)
        {
            Model.Set(Keys.CurrentDragObject, fieldName);
            Parent.Change(nameof(DragState));
        }

        [Bind("OnToggle")]
        private void ToggleClick(string name, bool value)
        {
            if (name == Keys.ToggleDragFrog)
                SetDataWhenClickOnToggle("Frog", value);
            else if (name == Keys.ToggleDragPinkFin)
                SetDataWhenClickOnToggle("PinkFin", value);
        }

        private void SetDataWhenClickOnToggle(string name, bool value)
        {
            Model.Set($"{name}DragState", value);
            Model.Set($"InptFld{name}NameFieldEnable", !value);
        }

        [Bind("OnInputField")]
        private void InputField(string name)
        {
            if (name == "FrogNameField")
                Model.Set(Keys.FrogNameText, Model.GetString(Keys.FrogNameInputField));
            else if (name == "PinkFinNameField")
                Model.Set(Keys.PinkFinNameText, Model.GetString(Keys.PinkFinInputField));
        }
    }
}