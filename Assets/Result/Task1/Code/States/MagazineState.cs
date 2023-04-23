using AxGrid.FSM;
using Result.Task1.Code.States.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Result.Task1.Code.States
{
    [State(nameof(MagazineState))]
    public class MagazineState : IdleState
    {
        private readonly Vector2Int _buyPriceRange = new Vector2Int(50, 150);

        private float _nextTimeBuy;

        public MagazineState(Image image, Color color) : base(image, color)
        {
        }

        [Loop(.4f)]
        public void LoopThis()
        {
            int bank = Model.GetInt(Constants.BankKey);

            if (bank <= 0)
                return;

            bank -= Random.Range(_buyPriceRange.x, _buyPriceRange.y + 1);
            bank = bank < 0 ? 0 : bank;
            Model.Set(Constants.BankKey, bank);
        }
    }
}