using System.Collections.Generic;
using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using Result.Task2.Code.States;
using UnityEngine;

namespace Result.Task2.Code
{
    public class Startup : MonoBehaviourExt
    {
        [SerializeField] private List<GameObject> _cards;

        [OnAwake]
        private void AwakeThis()
        {
            CreateFsm();
        }
        
        [OnStart]
        private void StartThis()
        {
            Settings.Fsm.Start(nameof(DormantState));
        }

        private void CreateFsm()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new DormantState());
            Settings.Fsm.Add(new FactoryState(_cards));
            Settings.Fsm.Add(new MoveState());
        }
    }
}