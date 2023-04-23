using System;
using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using Result.Task2.Code.View;

namespace Result.Task2.Code.States
{
    [State(nameof(DormantState))]
    public class DormantState : FSMState
    {
        private const int MaxCardInCollection = 10;

        [Enter]
        private void EnterThis()
        {
            bool buttonInteractable = Model.Get<List<CardView>>(Keys.FirstCollection).Count < MaxCardInCollection;
            Model.Set(Keys.DrawCardButton, buttonInteractable);
        }

        [Exit]
        private void ExitThis() =>
            Model.Set(Keys.DrawCardButton, false);

        [Bind("OnBtn")]
        private void Click()
        {
            if (Model.Get<List<CardView>>(Keys.FirstCollection).Count >= MaxCardInCollection)
            {
                Model.Set(Keys.DrawCardButton, false);
                return;
            }

            Model.Set(Keys.CurrentCollection, Keys.FirstCollection);
            Parent.Change(nameof(FactoryState));
        }

        [Bind(Keys.ClickOnCard)]
        private void ClickOnCard(string id)
        {
            List<CardView> secondCollection = Model.Get<List<CardView>>(Keys.SecondCollection);

            if (secondCollection.Count >= MaxCardInCollection)
                return;

            var card = GetCard(id);

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