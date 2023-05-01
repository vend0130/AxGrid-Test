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

            List<CardCollectionType> types = AddCollectionTypes();

            foreach (CardCollectionType type in types)
                Model.Set(type.ToString(), new CollectionData(type, new List<CardData>()));

            Parent.Change(nameof(DormantState));
        }

        private List<CardCollectionType> AddCollectionTypes()
        {
            List<CardCollectionType> types = new List<CardCollectionType>()
            {
                CardCollectionType.First,
                CardCollectionType.Second,
                CardCollectionType.Third,
            };

            Model.Set(Keys.CollectionsNames, types);

            return types;
        }
    }
}