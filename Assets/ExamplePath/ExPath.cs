using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

namespace ExamplePath
{
    public class ExPath : MonoBehaviourExt
    {
        [SerializeField] private float timeAnim = 1f;
        [SerializeField] private float minValue = 0f;
        [SerializeField] private float maxValue = 100f;
        
        private float actionCount = 0;
        
        [OnDelay(1f)]
        private void StartPath()
        {
            Path = new CPath();

            Path.Action(ActionPrintLog)
                .Action(() => print("START EasingLinear"))
                .EasingLinear(timeAnim, minValue, maxValue, value =>
                {
                    print($"{value:##0.00}");
                })
                .Action(() =>
                {
                    print("END EasingLinear");
                    print("START EasingCircEaseIn");
                })
                .EasingCircEaseIn(timeAnim, minValue, maxValue, value =>
                {
                    print($"{value:##0.00}");
                })
                .Action(() => print("END EasingCircEaseIn"));
        }

        private void ActionPrintLog()
        {
            print($"Action = {actionCount}");
            actionCount++;
        }
    }
}
