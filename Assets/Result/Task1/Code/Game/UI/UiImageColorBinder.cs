using System;
using AxGrid;
using AxGrid.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Result.Task1.Code.Game.UI
{
    [RequireComponent(typeof(Image))]
    public class UiImageColorBinder : Binder
    {
        [SerializeField] private string _fieldName;

        private Image _image;
        private string _eventName;

        [OnAwake]
        private void AwakeThis()
        {
            try
            {
                _image = GetComponent<Image>();
            }
            catch (Exception e)
            {
                Log.Error($"Error get Component:{e.Message}");
            }

            string fieldName = string.IsNullOrEmpty(_fieldName) ? nameof(UiImageColorBinder) : _fieldName;
            _eventName = $"On{fieldName}Changed";

            Model.EventManager.AddParameterAction<Color>(_eventName, Changed);
        }

        [OnDestroy]
        protected void DestroyThis()
        {
            try
            {
                Model.EventManager.RemoveParameterAction<Color>(_eventName, Changed);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void Changed(Color color) =>
            _image.color = color;
    }
}