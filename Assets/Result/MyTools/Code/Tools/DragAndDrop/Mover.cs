using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

namespace Result.MyTools.Code.Tools.DragAndDrop
{
    public class Mover : MonoBehaviourExt
    {
        private CPath _path;

        public void Play(float time, Vector2 target)
        {
            _path?.StopPath();
            _path = CreateNewPath();

            Vector2 current = transform.position;
            _path.EasingLinear(time, 0, 1, (f) =>
                transform.position = Vector2.Lerp(current, target, f));
        }
    }
}