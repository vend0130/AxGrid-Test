using System;
using System.Collections.Generic;
using AxGrid.FSM;
using Result.Task2.Code.Data;

namespace Result.Task2.Code.States
{
    [State(nameof(ClickOnCardState))]
    public class ClickOnCardState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            List<CardData> secondCollection = Model.Get<List<CardData>>(Keys.SecondCollection);

            if (secondCollection.Count >= GameData.MaxCardInCollection)
            {
                Parent.Change(nameof(DormantState));
                return;
            }

            var card = GetCard(Model.GetString(Keys.CardID));

            Model.Get<List<CardData>>(Keys.FirstCollection).Remove(card);
            secondCollection.Add(card);

            Model.Set(Keys.CurrentCollection, Keys.SecondCollection);

            Parent.Change(nameof(MoveState));
        }

        private CardData GetCard(string id)
        {
            foreach (CardData card in Model.Get<List<CardData>>(Keys.FirstCollection))
            {
                if (card.ID == id)
                    return card;
            }

            throw new Exception($"not found card with id [{id}] in first collection");
        }
    }
}