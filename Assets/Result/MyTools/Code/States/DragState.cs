using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

namespace Result.MyTools.Code.States
{
    [State(nameof(DragState))]
    public class DragState : FSMState
    {
        [Bind("EndDrag")]
        private void EndDrag(string fieldName)
        {
            Log.Debug($"Drop position {fieldName}: " + Model.Get<Vector2>($"{fieldName}DragPosition"));

            if (fieldName == "PinkFin")
                Model.EventManager.Invoke($"{fieldName}FailDrop");

            Parent.Change(nameof(InputChangeState));
        }
    }
}