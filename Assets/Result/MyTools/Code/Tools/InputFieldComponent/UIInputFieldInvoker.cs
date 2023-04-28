using AxGrid;
using AxGrid.Base;
using TMPro;
using UnityEngine;

namespace Result.MyTools.Code.Tools.InputFieldComponent
{
    [RequireComponent(typeof(TMP_InputField))]
    public class UIInputFieldInvoker : MonoBehaviourExtBind
    {
        [SerializeField] protected string fieldName;
        [SerializeField] private bool printEvent = false;

        protected TMP_InputField inputField;

        [OnAwake]
        private void AwakeThis()
        {
            inputField = GetComponent<TMP_InputField>();

            fieldName = string.IsNullOrEmpty(fieldName) ? name : fieldName;

            if (printEvent)
                inputField.onValueChanged.AddListener(OnValueChanged);
            else
                inputField.onEndEdit.AddListener(OnValueChanged);

            Model.Set($"{fieldName}Text", "safasf");
            Model.EventManager.AddAction($"On{fieldName}TextChanged", Changed);
        }

        [OnDestroy]
        public void DestroyThis()
        {
            if (printEvent)
                inputField.onValueChanged.RemoveAllListeners();
            else
                inputField.onEndEdit.RemoveAllListeners();

            Model.EventManager.RemoveAction($"On{fieldName}TextChanged", Changed);
        }

        private void Changed()
        {
            if (inputField.text != Model.GetString($"{fieldName}Text"))
                inputField.text = Model.GetString($"{fieldName}Text");
        }

        private void OnValueChanged(string text)
        {
            Model.Set($"{fieldName}Text", text);
            Settings.Fsm.Invoke("OnInputField", fieldName);
        }
    }
}