using System;
using System.Collections.Generic;
using AxGrid;
using AxGrid.Base;
using Result.Task2.Code.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Result.Task2.Code.Game
{
    [RequireComponent(typeof(CardsFactory))]
    public class CollectionsCards : MonoBehaviourExt
    {
        [SerializeField] private float _distanceBetweenCards = 1.7f;
        [SerializeField] private float _offsetY = .1f;

        private readonly List<CardView> _firstCollection = new List<CardView>();
        private readonly List<CardView> _secondCollection = new List<CardView>();

        private CardsFactory _factory;

        [OnAwake]
        private void AwakeThis()
        {
            try
            {
                _factory = GetComponent<CardsFactory>();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        [OnStart]
        private void StartThis()
        {
            try
            {
                Model.EventManager.AddAction(Keys.CollectionChanged, MoveCards);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        [OnDestroy]
        private void DestroyThis()
        {
            try
            {
                Model.EventManager.RemoveAction(Keys.CollectionChanged, MoveCards);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void MoveCards()
        {
            string targetCollection = Model.GetString(Keys.CurrentCollection);
            bool toFirstCollection = targetCollection == Keys.FirstCollection;

            List<CardData> firstCollection = Model.Get<List<CardData>>(Keys.FirstCollection);
            List<CardData> secondCollection = Model.Get<List<CardData>>(Keys.SecondCollection);

            string cardId = toFirstCollection ? firstCollection[^1].ID : secondCollection[^1].ID;

            ChangeCollections(toFirstCollection, cardId);

            StartMove(_firstCollection, Model.Get<Vector2>(Keys.FirstDefaultPosition), cardId);

            if (toFirstCollection)
                return;

            StartMove(_secondCollection, Model.Get<Vector2>(Keys.SecondDefaultPosition), cardId, false);
        }

        private void ChangeCollections(bool toFirstCollection, string cardId)
        {
            if (toFirstCollection)
            {
                var card = _factory.Create(cardId);
                _firstCollection.Add(card);
                return;
            }

            foreach (CardView card in _firstCollection)
            {
                if (card.Id != cardId)
                    continue;

                _secondCollection.Add(card);
                _firstCollection.Remove(card);
                return;
            }
        }

        private void StartMove(List<CardView> collection, Vector2 defaultPosition,
            string cardId, bool colliderEnable = true)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                CardView card = collection[i];

                Vector2 targetPoint = GetTargetPoint(collection.Count, i, defaultPosition);

                card.MoveTo(targetPoint, i, callBack: card.Id == cardId, colliderEnable);
            }
        }

        private Vector2 GetTargetPoint(int count, int index, Vector2 defaultPosition)
        {
            Vector2 position = Vector2.zero;

            position.y = Random.Range(defaultPosition.y - _offsetY, defaultPosition.y + _offsetY);

            float startPointX = -(float)(count - 1) / 2 * _distanceBetweenCards;
            position.x = startPointX + _distanceBetweenCards * index;

            return position;
        }
    }
}