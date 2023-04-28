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
        }

        [Exit]
        private void ExitThis()
        {
            Model.Set(Keys.ToggleDragFrogEnable, false);
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
                Model.Set(Keys.FrogDragState, value);
        }
    }
}