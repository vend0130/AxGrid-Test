using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

namespace Result.MyTools.Code.Tools.DragAndDrop
{
    public class Scaler : MonoBehaviourExt
    {
        private CPath _path;

        public void Play(float time, Vector2 target)
        {
            _path?.StopPath();
            _path = CreateNewPath();

            Vector2 current = transform.localScale;
            _path.EasingLinear(time, 0, 1, (f) =>
                transform.localScale = Vector2.Lerp(current, target, f));
        }

        public void Stop() =>
            _path?.StopPath();
    }
}