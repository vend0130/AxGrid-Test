using AxGrid.FSM;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(MoveState))]
    public class MoveState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Debug.Log("enter move");
        }
    }
}