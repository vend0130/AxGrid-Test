using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

namespace Result.Task1.Code.Game.Hero
{
    public class HeroView : MonoBehaviourExtBind
    {
        [SerializeField] public GameObject _current;

        [Bind(Constants.HeroMoveToBindName)]
        private void MoveTo(Vector2 at)
        {
            ChangeDirection(at);
            _current.SetActive(true);
        }

        [Bind(Constants.HeroResetBindName)]
        private void ResetHero(Vector2 at)
        {
            transform.position = at;
            Stop();
        }

        [Bind(Constants.HeroStopBindName)]
        private void Stop() =>
            _current.SetActive(false);

        private void ChangeDirection(Vector2 at)
        {
            Vector2 direction = (Vector2)transform.position - at;
            Vector2 currentScale = transform.localScale;
            currentScale.x = Mathf.Sign(direction.x);
            transform.localScale = currentScale;
        }
    }
}