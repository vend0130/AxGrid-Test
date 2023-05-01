using AxGrid.FSM;
using Result.Task2.Code.Data;

namespace Result.Task2.Code.States
{
    [State(nameof(ClickOnButtonState))]
    public class ClickOnButtonState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            if (IsFull())
            {
                Parent.Change(nameof(DormantState));
                return;
            }

            AddData();

            Model.EventManager.Invoke($"{CardCollectionType.First}Changed");
            Parent.Change(nameof(MoveState));
        }

        private void AddData()
        {
            int counter = Model.GetInt(Keys.Counter);

            var data = new CardData($"UniqueID_{counter}");
            Model.Get<CollectionData>(CardCollectionType.First.ToString()).CardDatas.Add(data);

            counter++;
            Model.Set(Keys.Counter, counter);
        }

        private bool IsFull() =>
            Model.Get<CollectionData>(CardCollectionType.First.ToString())
                .CardDatas.Count >= GameData.MaxCardInCollection;
    }
}