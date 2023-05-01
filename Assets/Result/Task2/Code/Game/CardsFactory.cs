using System.Collections.Generic;
using AxGrid.Base;
using Result.Task2.Code.Extensions;
using UnityEngine;

namespace Result.Task2.Code.Game
{
    public class CardsFactory : MonoBehaviourExt
    {
        [SerializeField] private List<CardView> _cards;

        private readonly List<CardView> _cardsOnScene = new List<CardView>();

        public CardView GetCard(string id, Transform parent)
        {
            if (TryGetCardOnScene(id, out CardView card))
                card.transform.SetParent(parent);
            else
                card = Create(id, parent);

            return card;
        }

        private CardView Create(string id, Transform parent)
        {
            CardView card = Instantiate(_cards.GetRandomElement(),
                parent.position, Quaternion.identity, parent);

            card.name += $"-{id}";

            card.Init(id);
            _cardsOnScene.Add(card);

            return card;
        }

        private bool TryGetCardOnScene(string id, out CardView card)
        {
            card = _cardsOnScene.Find(cardOnScene => cardOnScene.Id == id);
            return card != null;
        }
    }
}