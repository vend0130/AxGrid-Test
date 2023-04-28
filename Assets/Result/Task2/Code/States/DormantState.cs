using System.Collections.Generic;
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
            bool buttonInteractable =
                Model.Get<List<CardData>>(Keys.FirstCollection).Count < GameData.MaxCardInCollection;
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
        private void ClickOnCard() =>
            Parent.Change(nameof(ClickOnCardState));
    }
}