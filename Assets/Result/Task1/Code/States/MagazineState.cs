using AxGrid.FSM;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(MagazineState))]
    public class MagazineState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Debug.Log("enter Magazine");
        }
    }
}