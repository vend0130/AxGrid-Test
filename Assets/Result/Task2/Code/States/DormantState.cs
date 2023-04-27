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

        [Bind("OnBtn")]
        private void ClickOnButton()
        {
            Parent.Change(nameof(FactoryState));
        }

        [Bind(Keys.ClickOnCard)]
        private void ClickOnCard(string id)
        {
            Model.Set("card", id);//TODO: temp logic
            Parent.Change(nameof(ClickOnCardState));
        }
    }
}