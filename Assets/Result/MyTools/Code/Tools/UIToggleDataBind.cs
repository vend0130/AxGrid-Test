using System;
using AxGrid;
using AxGrid.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Result.MyTools.Code.Tools
{
    [RequireComponent(typeof(Toggle))]
    public class UIToggleDataBind : Binder
    {
        [SerializeField] private string _toggleName;
        [SerializeField] private string _enableField = "";
        [SerializeField] private bool _defaultEnable = true;

        private Toggle _toggle;

        [OnAwake]
        protected void AwakeThis()
        {
            try
            {
                _toggle = GetComponent<Toggle>();
            }
            catch (Exception e)
            {
                Log.Error($"Error get Component:{e.Message}");
            }

            _toggleName = string.IsNullOrEmpty(_toggleName) ? name : _toggleName;
            _enableField = string.IsNullOrEmpty(_enableField) ? $"Toggle{_toggleName}Enable" : _enableField;
        }

        [OnStart]
        public void StartThis()
        {
            _toggle.onValueChanged.AddListener(OnClick);
            Model.EventManager.AddAction($"OnToggle{_toggleName}IsOnChanged", OnItemValue);
            Model.EventManager.AddAction($"On{_enableField}Changed", OnItemEnable);

            OnItemEnable();
            OnItemValue();
        }

        [OnDestroy]
        public void DestroyThis()
        {
            _toggle.onValueChanged.RemoveAllListeners();
            Model.EventManager.RemoveAction($"OnToggle{_toggleName}IsOnChanged", OnItemValue);
            Model.EventManager.RemoveAction($"On{_enableField}Changed", OnItemEnable);
        }

        private void OnItemValue()
        {
            if (_toggle.isOn != Model.GetBool(_toggleName))
                _toggle.isOn = Model.GetBool(_toggleName);

            OnClick(_toggle.isOn);
        }

        private void OnClick(bool value)
        {
            Settings.Fsm?.Invoke("OnToggle", _toggleName, value);
        }

        private void OnItemEnable()
        {
            if (_toggle.interactable != Model.GetBool(_enableField, _defaultEnable))
                _toggle.interactable = Model.GetBool(_enableField, _defaultEnable);
        }
    }
}