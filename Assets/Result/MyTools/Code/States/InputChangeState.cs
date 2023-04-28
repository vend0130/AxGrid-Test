using AxGrid;
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
            Log.Debug("enter input");
            Model.Set($"FrogState", true);
        }

        [Bind("BeginDrag")]
        private void BeginDrag(string fieldName)
        {
            Parent.Change(nameof(DragState));
        }
    }
}