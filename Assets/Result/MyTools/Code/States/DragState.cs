using AxGrid;
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
            Log.Debug("enter drag");
        }
        
        [Bind("EndDrag")]
        private void BeginDrag(string fieldName)
        {
            Parent.Change(nameof(InputChangeState));
        }
    }
}