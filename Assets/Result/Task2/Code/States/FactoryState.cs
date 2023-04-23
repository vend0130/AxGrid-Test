using System.Collections.Generic;
using AxGrid;
using AxGrid.FSM;
using UnityEngine;

namespace Result.Task2.Code.States
{
    [State(nameof(FactoryState))]
    public class FactoryState : FSMState
    {
        private readonly List<GameObject> _cards;

        public FactoryState(List<GameObject> cards) =>
            _cards = cards;
        
        [Enter]
        private void EnterThis()
        {
            Log.Debug("enter factory");
        }
    }
}