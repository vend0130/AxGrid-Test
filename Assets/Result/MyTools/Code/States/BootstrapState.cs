using AxGrid.FSM;

namespace Result.MyTools.Code.States
{
    [State(nameof(BootstrapState))]
    public class BootstrapState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            ChangeFrogData(isDrag: false, name: "froooog");

            Parent.Change(nameof(InputChangeState));
        }

        private void ChangeFrogData(bool isDrag, string name)
        {
            Model.Set(Keys.ToggleDragFrogIsOn, isDrag);
            Model.Set(Keys.FrogDragState, isDrag);
            
            Model.Set(Keys.FrogNameInputField, name);
            Model.Set(Keys.FrogNameText, name);
        }
    }
}