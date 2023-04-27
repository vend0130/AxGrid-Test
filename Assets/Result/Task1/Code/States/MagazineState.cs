using AxGrid.FSM;
using Result.Task1.Code.States.Base;
using UnityEngine;

namespace Result.Task1.Code.States
{
    [State(nameof(MagazineState))]
    public class MagazineState : IdleState
    {
        private readonly Vector2Int _buyPriceRange = new Vector2Int(50, 150);

        private float _nextTimeBuy;

        protected override void EnterThis()
        {
            base.EnterThis();
            Model.Set(Keys.Image, Model.Get<Color>(nameof(MagazineState)));
        }

        [Loop(.4f)]
        public void LoopThis()
        {
            int bank = Model.GetInt(Keys.Bank);

            if (bank <= 0)
                return;

            bank -= Random.Range(_buyPriceRange.x, _buyPriceRange.y + 1);
            bank = bank < 0 ? 0 : bank;
            Model.Set(Keys.Bank, bank);
        }
    }
}