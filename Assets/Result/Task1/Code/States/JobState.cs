using AxGrid.Base;
using AxGrid.FSM;
using Result.Task1.Code.States.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Result.Task1.Code.States
{
    [State(nameof(JobState))]
    public class JobState : IdleState
    {
        private const int Salary = 100;

        public JobState(Image image, Color color) : base(image, color)
        {
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