using AxGrid.FSM;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(HomeState))]
    public class HomeState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Debug.Log("enter home");
        }
    }
}