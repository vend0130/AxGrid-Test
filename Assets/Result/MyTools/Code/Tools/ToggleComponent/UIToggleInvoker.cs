using AxGrid;
using AxGrid.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Result.MyTools.Code.Tools.ToggleComponent
{
    [RequireComponent(typeof(Toggle))]
    public class UIToggleInvoker : MonoBehaviourExtBind
    {
        [SerializeField] protected string toggleName;

        protected Toggle toggle;

        [OnAwake(priority: 0)]
        private void AwakeThis()
        {
            toggle = GetComponent<Toggle>();
            toggleName = string.IsNullOrEmpty(toggleName) ? name : toggleName;
            Model.EventManager.AddAction($"OnToggle{toggleName}IsOnChanged", OnItemValue);
            Log.Debug(name + "  " + $"OnToggle{toggleName}IsOnChanged");
        }

        [OnStart]
        public void StartThis() =>
            toggle.onValueChanged.AddListener(OnClick);

        [OnDestroy]
        public void DestroyThis()
        {
            toggle.onValueChanged.RemoveAllListeners();
            Model.EventManager.RemoveAction($"OnToggle{toggleName}IsOnChanged", OnItemValue);
        }

        private void OnItemValue()
        {
            if (toggle.isOn != Model.GetBool($"Toggle{toggleName}IsOn"))
                toggle.isOn = Model.GetBool($"Toggle{toggleName}IsOn");
        }

        private void OnClick(bool value)
        {
            Model.Set($"Toggle{toggleName}IsOn", value);
            Settings.Fsm?.Invoke("OnToggle", toggleName, value);
        }
    }
}