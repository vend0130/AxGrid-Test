using AxGrid.FSM;
using AxGrid.Model;

namespace Result.MyTools.Code.States
{
    [State(nameof(DragState))]
    public class DragState : FSMState
    {

        [Bind("EndDrag")]
        private void EndDrag(string fieldName)
        {
            if (fieldName == "PinkFin")
                Model.EventManager.Invoke($"On{fieldName}FailDrop");

            Parent.Change(nameof(InputChangeState));
        }
    }
}