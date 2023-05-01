using AxGrid.Base;
using UnityEngine;

namespace Result.MyTools.Code.Tools.ToggleComponent
{
    public class UIToggleInteractable : UIToggleInvoker
    {
        [SerializeField] private bool defaultEnable = true;

        private string EnableField => $"Toggle{toggleName}Enable";

        [OnAwake(priority: 1)]
        private void AwakeThis() =>
            Model.EventManager.AddAction($"On{EnableField}Changed", OnItemEnable);

        [OnDestroy]
        private void DestroyTis() =>
            Model.EventManager.RemoveAction($"On{EnableField}Changed", OnItemEnable);

        private void OnItemEnable()
        {
            if (toggle.interactable != Model.GetBool(EnableField, defaultEnable))
                toggle.interactable = Model.GetBool(EnableField, defaultEnable);
        }
    }
}