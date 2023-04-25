using AxGrid.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Result.Task1.Code.Game.UI
{
    public class PanelWithButtonsView : MonoBehaviourExt
    {
        [SerializeField] private Image _panel;

        [OnAwake]
        private void AwakeThis() =>
            Model.EventManager.AddParameterAction<Color>(Keys.EnterState, ChangeColor);

        private void ChangeColor(Color color) =>
            _panel.color = color;
    }
}