using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using UnityEngine;

namespace Result.Task1.Code.Game.Hero
{
    public class HeroView : MonoBehaviourExtBind
    {
        [SerializeField] private GameObject _current;
        [SerializeField] private Animator _moveAnimator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _houseEntranceSprite;

        private const float Speed = 3f;
        private const float DelayForEnterInHouse = .13f;

        [Bind(Constants.HeroMoveToBindName)]
        private void MoveTo(Vector2 targetPoint)
        {
            Path = new CPath();

            float direction = ((Vector2)transform.position - targetPoint).x;

            ChangeLookDirection(direction);
            Move(targetPoint, direction);
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

        private void ChangeLookDirection(float direction)
        {
            Vector2 currentScale = transform.localScale;
            currentScale.x = Mathf.Sign(direction);
            transform.localScale = currentScale;
        }

        private void Move(Vector2 targetPoint, float direction)
        {
            _moveAnimator.enabled = true;
            _current.SetActive(true);

            Vector2 currentPoint = transform.position;
            float time = Mathf.Abs(direction) / Speed;

            Path
                .EasingLinear(time, 0, 1, (f) =>
                    transform.position = Vector2.Lerp(currentPoint, targetPoint, f))
                .Action(() =>
                {
                    _moveAnimator.enabled = false;
                    _spriteRenderer.sprite = _houseEntranceSprite;
                })
                .Wait(DelayForEnterInHouse)
                .Action(() => Settings.Fsm.Invoke(Constants.HeroStopBindName));
        }
    }
}