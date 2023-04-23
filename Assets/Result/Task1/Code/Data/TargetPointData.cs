using System;
using UnityEngine;

namespace Result.Task1.Code.Data
{
    [Serializable]
    public class TargetPointData
    {
        [field: SerializeField] public TargetPointType Type { get; private set; }
        [field: SerializeField] public Transform Point { get; private set; }
    }
}