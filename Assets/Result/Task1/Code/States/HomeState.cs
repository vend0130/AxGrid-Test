using AxGrid.FSM;
using Result.Task1.Code.States.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Result.Task1.Code.States
{
    [State(nameof(HomeState))]
    public class HomeState : IdleState
    {
        public HomeState(Image image, Color color) : base(image, color)
        {
        }
    }
}