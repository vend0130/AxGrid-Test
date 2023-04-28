using AxGrid.FSM;
using AxGrid.Model;

namespace Result.MyTools.Code.States
{
    [State(nameof(DragState))]
    public class DragState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
        }

        [Bind("EndDrag")]
        private void EndDrag(string fieldName)
        {
            Parent.Change(nameof(InputChangeState));
        }
    }
}