using AxGrid.FSM;

namespace Result.MyTools.Code.States
{
    [State(nameof(BootstrapState))]
    public class BootstrapState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            ChangeFrogData(isDrag: false, name: string.Empty);
            ChangePinkFinData(isDrag: true, name: "Pink Fin");

            Parent.Change(nameof(InputChangeState));
        }

        private void ChangeFrogData(bool isDrag, string name)
        {
            Model.Set(Keys.ToggleDragFrogIsOn, isDrag);
            Model.Set(Keys.FrogDragState, isDrag);

            Model.Set(Keys.FrogNameInputField, name);
            Model.Set(Keys.FrogNameText, name);
        }

        private void ChangePinkFinData(bool isDrag, string name)
        {
            Model.Set(Keys.ToggleDragPinkFinIsOn, isDrag);
            Model.Set(Keys.PinkFinDragState, isDrag);

            Model.Set(Keys.PinkFinInputField, name);
            Model.Set(Keys.PinkFinNameText, name);
        }
    }
}