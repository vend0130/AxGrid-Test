using AxGrid.FSM;
using AxGrid.Model;
using Result.Task2.Code.Data;

namespace Result.Task2.Code.States
{
    [State(nameof(DormantState))]
    public class DormantState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            bool buttonInteractable = IsNotFull();
            Model.Set(Keys.DrawCardButton, buttonInteractable);
        }

        [Bind("OnBtn")]
        private void ClickOnButton(string name)
        {
            if (name != "DrawCardButton")
                return;

            Parent.Change(nameof(ClickOnButtonState));
        }

        [Bind(Keys.ClickOnCard)]
        private void ClickOnCard(string cardId, CardCollectionType collectionType)
        {
            if (collectionType != CardCollectionType.First)
                return;

            Model.Set(Keys.ClickOnCardID, cardId);
            Model.Set(Keys.ClickOnCardCollection, collectionType);

            Parent.Change(nameof(ClickOnCardState));
        }

        private bool IsNotFull() =>
            Model.Get<CollectionData>(CardCollectionType.First.ToString())
                .CardDatas.Count < GameData.MaxCardInCollection;
    }
}