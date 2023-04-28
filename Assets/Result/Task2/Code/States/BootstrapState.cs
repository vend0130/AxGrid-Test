using System.Collections.Generic;
using AxGrid.FSM;
using Result.Task2.Code.Data;

namespace Result.Task2.Code.States
{
    [State(nameof(BootstrapState))]
    public class BootstrapState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            Model.Set(Keys.Counter, 0);

            List<string> names = AddCollectionNames();

            foreach (string name in names)
                Model.Set(name, new List<CardData>());

            Parent.Change(nameof(DormantState));
        }

        private List<string> AddCollectionNames()
        {
            List<string> names = new List<string>()
            {
                Keys.FirstCollection,
                Keys.SecondCollection,
                Keys.ThirdCollection,
            };

            Model.Set(Keys.CollectionsNames, names);

            return names;
        }
    }
}