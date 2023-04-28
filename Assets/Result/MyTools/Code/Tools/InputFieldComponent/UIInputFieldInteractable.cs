using AxGrid.Base;
using UnityEngine;

namespace Result.MyTools.Code.Tools.InputFieldComponent
{
    public class UIInputFieldInteractable : UIInputFieldInvoker
    {
        [SerializeField] private bool defaultEnable = true;

        private string EnableField => $"InptFld{fieldName}Enable";

        [OnAwake]
        private void AwakeThis() =>
            Model.EventManager.AddAction($"On{EnableField}Changed", OnItemEnable);

        [OnDestroy]
        private void DestroyTis() =>
            Model.EventManager.RemoveAction($"On{EnableField}Changed", OnItemEnable);

        private void OnItemEnable()
        {
            if (inputField.interactable != Model.GetBool(EnableField, defaultEnable))
                inputField.interactable = Model.GetBool(EnableField, defaultEnable);
        }
    }
}