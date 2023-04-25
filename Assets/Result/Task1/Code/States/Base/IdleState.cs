using AxGrid;
using AxGrid.FSM;

namespace Result.Task1.Code.States.Base
{
    public class IdleState : ButtonListener
    {

        [Enter]
        protected virtual void EnterThis() => 
            Settings.Invoke(Keys.HeroStop);
    }
}