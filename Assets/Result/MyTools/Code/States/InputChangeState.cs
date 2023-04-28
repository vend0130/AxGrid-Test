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
            Model.Set(Keys.ToggleDragFrogEnable, true);
            Model.Set(Keys.InptFldFrogNameEnable, !Model.GetBool(Keys.ToggleDragFrogIsOn));
        }

        [Exit]
        private void ExitThis()
        {
            Model.Set(Keys.ToggleDragFrogEnable, false);
            Model.Set(Keys.InptFldFrogNameEnable, false);
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
            {
                Model.Set(Keys.FrogDragState, value);
                Model.Set(Keys.InptFldFrogNameEnable, !value);
            }
        }
        
        [Bind("OnInputField")]
        private void InputField(string name)
        {
            if (name == "FrogNameField")
            {
                Model.Set(Keys.FrogNameText, Model.GetString(Keys.FrogNameInputField));
            }
        }
    }
}