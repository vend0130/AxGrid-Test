using AxGrid.Base;
using UnityEngine;

namespace Result.Task2.Code.View
{
    public class CardView : MonoBehaviourExt
    {
        public string Id { get; private set; }

        public void InitId(string id) =>
            Id = id;

        public void MoveTo(Vector2 targetPosition)
        {
            transform.position = targetPosition;
        }
    }
}