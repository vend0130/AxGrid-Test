using System;
using System.Collections.Generic;
using AxGrid.FSM;
using Result.Task2.Code.Data;
using Result.Task2.Code.View;

namespace Result.Task2.Code.States
{
    [State(nameof(ClickOnCardState))]
    public class ClickOnCardState : FSMState
    {
        [Enter]
        private void EnterThis()
        {
            List<CardView> secondCollection = Model.Get<List<CardView>>(Keys.SecondCollection);

            if (secondCollection.Count >= GameData.MaxCardInCollection)
            {
                Parent.Change(nameof(DormantState));
                return;
            }
            
            Model.Set(Keys.DrawCardButton, false);

            var card = GetCard(Model.GetString("card")); //TODO: temp logic

            Model.Get<List<CardView>>(Keys.FirstCollection).Remove(card);
            secondCollection.Add(card);

            Model.Set(Keys.CurrentCollection, Keys.SecondCollection);
            Parent.Change(nameof(MoveState));
        }

        private CardView GetCard(string id)
        {
            foreach (CardView card in Model.Get<List<CardView>>(Keys.FirstCollection))
            {
                if (card.Id == id)
                    return card;
            }

            throw new Exception($"not found card with id [{id}] in first collection");
        }
    }
}