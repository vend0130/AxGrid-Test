using System.Collections.Generic;
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
            if (Model.Get<List<CardData>>(Keys.FirstCollection).Count >= GameData.MaxCardInCollection)
            {
                Parent.Change(nameof(DormantState));
                return;
            }

            Model.Set(Keys.CurrentCollection, Keys.FirstCollection);

            AddData();

            Parent.Change(nameof(MoveState));
        }

        private void AddData()
        {
            int counter = Model.GetInt(Keys.Counter);

            var data = new CardData($"UniqueID_{counter}");
            Model.Get<List<CardData>>(Keys.FirstCollection).Add(data);

            counter++;
            Model.Set(Keys.Counter, counter);
        }
    }
}