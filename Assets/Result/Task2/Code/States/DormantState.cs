using System;
using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using Result.Task2.Code.View;
using UnityEngine;

namespace Result.Task2.Code.States
{
    [State(nameof(DormantState))]
    public class DormantState : FSMState
    {
        [Enter]
        private void EnterThis() =>
            Model.Set(Keys.DrawCardButton, true);

        [Exit]
        private void ExitThis() =>
            Model.Set(Keys.DrawCardButton, false);

        [Bind("OnBtn")]
        private void Click()
        {
            Model.Set(Keys.CurrentCollection, Keys.FirstCollection);
            Parent.Change(nameof(FactoryState));
        }

        [Bind(Keys.ClickOnCard)]
        private void ClickOnCard(string id)
        {
            var card = GetCard(id);

            Model.Get<List<CardView>>(Keys.FirstCollection).Remove(card);
            Model.Get<List<CardView>>(Keys.SecondCollection).Add(card);

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