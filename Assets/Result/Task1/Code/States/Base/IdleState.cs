using AxGrid;
using AxGrid.FSM;
using UnityEngine;
using UnityEngine.UI;

namespace Result.Task1.Code.States.Base
{
    public class IdleState : ButtonListener
    {
        private readonly Image _imageWithButtons;
        private readonly Color _targetColor;

        protected IdleState(Image image, Color color)
        {
            _imageWithButtons = image;
            _targetColor = color;
        }

        [Enter]
        protected void EnterThis()
        {
            Settings.Invoke(Keys.HeroStop);
            _imageWithButtons.color = _targetColor;
        }
    }
}