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

        public CardView GetCard(string id, string collectionName, Transform parent)
        {
            CardView card;
            if (TryGetCardOnScene(id, out card))
            {
                card.ChangeCollection(collectionName);
                card.transform.SetParent(parent);
                return card;
            }

            card = Create(id, parent);
            card.ChangeCollection(collectionName);

            return card;
        }

        private CardView Create(string id, Transform parent)
        {
            CardView card = Instantiate(_cards.GetRandomElement(),
                parent.position, Quaternion.identity, parent);

            card.Init(id);

            _cardsOnScene.Add(card);

            return card;
        }

        private bool TryGetCardOnScene(string id, out CardView card)
        {
            foreach (CardView cardOnScene in _cardsOnScene)
            {
                if (cardOnScene.Id == id)
                {
                    card = cardOnScene;
                    return true;
                }
            }

            card = null;
            return false;
        }
    }
}