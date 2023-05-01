using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

namespace Result.MyTools.Code.Tools.DragAndDrop
{
    public class Mover : MonoBehaviourExt
    {
        public CPath CurrentPath { get; private set; }

        public void Play(float time, Vector2 target)
        {
            CurrentPath?.StopPath();
            CurrentPath = CreateNewPath();

            Vector2 current = transform.position;
            CurrentPath.EasingLinear(time, 0, 1, (f) =>
                transform.position = Vector2.Lerp(current, target, f));
        }
    }
}