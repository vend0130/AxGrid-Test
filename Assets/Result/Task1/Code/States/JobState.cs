using AxGrid.Base;
using AxGrid.FSM;
using Result.Task1.Code.States.Base;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(JobState))]
    public class JobState : IdleState
    {
        private const int Salary = 100;

        protected override void EnterThis()
        {
            base.EnterThis();
            Model.EventManager.Invoke(Keys.EnterState, Model.Get<Color>(nameof(JobState)));
        }

        [OnRefresh(.5f)]
        public void LoopThis()
        {
            int bank = Model.GetInt(Keys.Bank);
            bank += Salary;
            Model.Set(Keys.Bank, bank);
        }
    }
}