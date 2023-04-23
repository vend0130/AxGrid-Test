using AxGrid;
using AxGrid.FSM;
using Result.Task1.Code.States.Base;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(MoveState))]
    public class MoveState : ButtonListener
    {
        [Enter]
        private void EnterThis()
        {
            Log.Debug("enter move state");
            Vector2 point = Model.Get<Vector2>(Constants.TargetPointKey);
            Settings.Invoke(Constants.HeroMoveToBindName, point);
        }
    }
}