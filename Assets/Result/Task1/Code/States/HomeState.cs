using AxGrid.FSM;
using Result.Task1.Code.States.Base;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(HomeState))]
    public class HomeState : IdleState
    {
        protected override void EnterThis()
        {
            base.EnterThis();
            Model.Set(Keys.Image, Model.Get<Color>(nameof(HomeState)));
        }
    }
}