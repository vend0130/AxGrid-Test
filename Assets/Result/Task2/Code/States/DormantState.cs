using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using Result.Task2.Code.Data;
using UnityEngine;

namespace Result.Task2.Code.States
{
    [State(nameof(DormantState))]
    public class DormantState : FSMState
    {
        private const int MaxCardInCollection = 10;

        [Enter]
        private void EnterThis()
        {
            Model.EventManager.Invoke("Lol", Vector2.one * 11, 1, false, false);
            bool buttonInteractable = Model.Get<List<CardData>>(Keys.FirstCollection).Count < MaxCardInCollection;
            Model.Set(Keys.DrawCardButton, buttonInteractable);
        }

        [Bind("OnBtn")]
        private void ClickOnButton() =>
            Parent.Change(nameof(ClickOnButtonState));

        [Bind(Keys.ClickOnCard)]
        private void ClickOnCard() =>
            Parent.Change(nameof(ClickOnCardState));
    }
}