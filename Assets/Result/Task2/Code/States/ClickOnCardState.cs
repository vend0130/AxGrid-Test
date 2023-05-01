using System.Collections.Generic;
using System.Linq;
using AxGrid.FSM;
using Result.Task2.Code.Data;
using Result.Task2.Code.Extensions;

namespace Result.Task2.Code.States
{
    [State(nameof(ClickOnCardState))]
    public class ClickOnCardState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            string currentCardId = Model.GetString(Keys.ClickOnCardID);
            CardCollectionType clickCollection = Model.Get<CardCollectionType>(Keys.ClickOnCardCollection);

            if (!TryGetCollection(clickCollection, out CollectionData targetCollection) ||
                !TryGetCard(clickCollection, currentCardId, out CardData card))
            {
                Parent.Change(nameof(DormantState));
                return;
            }

            RemoveCard(clickCollection, currentCardId);
            SwitchCollections(clickCollection, targetCollection, card);

            Parent.Change(nameof(MoveState));
        }

        private bool TryGetCollection(CardCollectionType clickCollection, out CollectionData collection)
        {
            List<CardCollectionType> types = Model
                .Get<List<CardCollectionType>>(Keys.CollectionsNames)
                .Where((type) => IsCorrectType(clickCollection, type))
                .ToList();

            if (types.Count == 0)
            {
                collection = null;
                return false;
            }

            collection = Model.Get<CollectionData>(types.GetRandomElement().ToString());
            return true;
        }

        private bool IsCorrectType(CardCollectionType clickCollection, CardCollectionType type) =>
            type != clickCollection &&
            Model.Get<CollectionData>(type.ToString()).CardDatas.Count < GameData.MaxCardInCollection;

        private bool TryGetCard(CardCollectionType clickCollection, string cardId, out CardData card)
        {
            card = Model
                .Get<CollectionData>(clickCollection.ToString())
                .CardDatas.Find(currentCard => currentCard.ID == cardId);

            return card != null;
        }

        private void RemoveCard(CardCollectionType clickCollection, string cardId) =>
            Model.EventManager.Invoke($"{clickCollection}RemoveCard", cardId);

        private void SwitchCollections(CardCollectionType clickCollection,
            CollectionData targetCollection, CardData card)
        {
            Model.Get<CollectionData>(clickCollection.ToString()).CardDatas.Remove(card);

            targetCollection.CardDatas.Add(card);
            Model.EventManager.Invoke($"{targetCollection.Type}Changed");
        }
    }
}