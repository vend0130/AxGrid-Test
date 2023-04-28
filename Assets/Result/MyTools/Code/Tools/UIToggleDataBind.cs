using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Result.MyTools.Code.Tools
{
    [RequireComponent(typeof(Toggle))]
    public class UIToggleDataBind : MonoBehaviourExtBind
    {
        [SerializeField] private string _toggleName;
        [SerializeField] private bool _defaultEnable = true;

        private Toggle _toggle;
        private string _enableField;

        [OnAwake]
        protected void AwakeThis()
        {
            _toggle = GetComponent<Toggle>();
            
            _toggleName = string.IsNullOrEmpty(_toggleName) ? name : _toggleName;
            _enableField = $"Toggle{_toggleName}Enable";
        }

        [OnStart]
        public void StartThis()
        {
            _toggle.onValueChanged.AddListener(OnClick);

            OnItemEnable();
            OnItemValue();
        }

        [OnDestroy]
        public void DestroyThis()
        {
            _toggle.onValueChanged.RemoveAllListeners();
        }

        [Bind("OnToggle{_toggleName}IsOnChanged")]
        private void OnItemValue()
        {
            if (_toggle.isOn != Model.GetBool($"Toggle{_toggleName}IsOn"))
                _toggle.isOn = Model.GetBool($"Toggle{_toggleName}IsOn");
        }

        private void OnClick(bool value)
        {
            Model.Set($"Toggle{_toggleName}IsOn", value);
            Settings.Fsm?.Invoke("OnToggle", _toggleName, value);
        }

        [Bind("On{_enableField}Changed")]
        private void OnItemEnable()
        {
            if (_toggle.interactable != Model.GetBool(_enableField, _defaultEnable))
                _toggle.interactable = Model.GetBool(_enableField, _defaultEnable);
        }
    }
}