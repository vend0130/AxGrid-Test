using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace Result.Task2.Code.States
{
    [State(nameof(DormantState))]
    public class DormantState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Model.Set(Keys.DrawCardButton, true);
        }
        
        [Bind("OnBtn")]
        private void Click(string buttonName)
        {
            Parent.Change(nameof(FactoryState));
        }
    }
}