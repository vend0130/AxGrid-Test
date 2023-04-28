using System.Collections.Generic;
using AxGrid;
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
            if (!NextCollection(out string nameCollection, out List<CardData> collection))
            {
                Parent.Change(nameof(DormantState));
                return;
            }

            string id = Model.GetString(Keys.CardID);
            if (!TryGetCard(id, out CardData card))
            {
                Log.Error($"not fount card with id: {id} in {Keys.FirstCollection}");
                Parent.Change(nameof(DormantState));
                return;
            }

            Model.Get<List<CardData>>(Keys.FirstCollection).Remove(card);
            collection.Add(card);

            Model.EventManager.Invoke($"{nameCollection}Changed");
            Parent.Change(nameof(MoveState));
        }

        private bool NextCollection(out string name, out List<CardData> collection)
        {
            List<string> names = Model.Get<List<string>>(Keys.CollectionsNames);
            List<(string, List<CardData>)> pair = new List<(string, List<CardData>)>();

            foreach (string currentName in names)
            {
                if (currentName == Keys.FirstCollection)
                    continue;

                var currentCollection = Model.Get<List<CardData>>(currentName);
                if (currentCollection.Count < GameData.MaxCardInCollection)
                    pair.Add((currentName, currentCollection));
            }

            if (pair.Count == 0)
            {
                name = string.Empty;
                collection = null;
                return false;
            }

            (name, collection) = pair.GetRandomElement();
            return true;
        }

        private bool TryGetCard(string id, out CardData card)
        {
            foreach (CardData currentCard in Model.Get<List<CardData>>(Keys.FirstCollection))
            {
                if (currentCard.ID == id)
                {
                    card = currentCard;
                    return true;
                }
            }

            card = null;
            return false;
        }
    }
}