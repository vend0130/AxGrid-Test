using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using TMPro;
using UnityEngine;

namespace Result.MyTools.Code.Tools
{
    [RequireComponent(typeof(TMP_InputField))]
    public class UIInputFieldDataBind : MonoBehaviourExtBind
    {
        [SerializeField] private string _fieldName;
        [SerializeField] private bool _defaultEnable = true;
        [SerializeField] private bool _printEvent = false;

        private TMP_InputField _inputField;
        private string _enableField;

        [OnAwake]
        private void AwakeThis()
        {
            _inputField = GetComponent<TMP_InputField>();
            
            _fieldName = string.IsNullOrEmpty(_fieldName) ? name : _fieldName;
            _enableField = $"InptFld{_fieldName}Enable";

            if (_printEvent)
                _inputField.onValueChanged.AddListener(OnValueChanged);
            else
                _inputField.onEndEdit.AddListener(OnValueChanged);
        }

        [OnDestroy]
        public void DestroyThis()
        {
            if (_printEvent)
                _inputField.onValueChanged.RemoveAllListeners();
            else
                _inputField.onEndEdit.RemoveAllListeners();
        }

        [Bind("On{_enableField}Changed")]
        private void OnItemEnable()
        {
            if (_inputField.interactable != Model.GetBool(_enableField, _defaultEnable))
                _inputField.interactable = Model.GetBool(_enableField, _defaultEnable);
        }

        [Bind("On{_fieldName}TextChanged")]
        private void Changed()
        {
            if (_inputField.text != Model.GetString($"{_fieldName}Text"))
                _inputField.text = Model.GetString($"{_fieldName}Text");
        }

        private void OnValueChanged(string text)
        {
            Model.Set($"{_fieldName}Text", text);
            Settings.Fsm.Invoke("OnInputField", _fieldName);
        }
    }
}