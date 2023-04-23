using AxGrid.Base;
using Result.Task1.Code.Data;
using UnityEngine;

namespace Result.Task1.Code.Game
{
    public class TargetPointsList : MonoBehaviourExtBind
    {
        [SerializeField] public TargetPointData[] _points;

        [OnAwake]
        private void AwakeThis()
        {
            foreach (TargetPointData point in _points)
                Model.Set(point.Type.ToString(), (Vector2)point.Point.position);
        }
    }
}