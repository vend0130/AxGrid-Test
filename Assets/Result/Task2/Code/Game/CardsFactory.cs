using System.Collections.Generic;
using AxGrid.Base;
using Result.Task2.Code.Extensions;
using UnityEngine;

namespace Result.Task2.Code.Game
{
    public class CardsFactory : MonoBehaviourExt
    {
        [SerializeField] private List<CardView> _cards;

        public CardView Create(string id)
        {
            CardView card = Instantiate(_cards.GetRandomElement(),
                Model.Get<Vector2>(Keys.FirstDefaultPosition), Quaternion.identity, transform);

            card.Init(id);

            return card;
        }
    }
}