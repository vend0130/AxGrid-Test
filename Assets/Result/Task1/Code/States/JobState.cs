using AxGrid.FSM;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(JobState))]
    public class JobState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Debug.Log("enter job");
        }
    }
}