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
    public class CollectionCards : MonoBehaviourExt
    {
        [SerializeField] private string _nameCollection;
        [SerializeField] private Transform _parent;
        [SerializeField] private bool _colliderEnable;

        private const float DistanceBetweenCards = 1.35f;
        private const float OffsetY = .1f;

        private readonly List<CardView> _collection = new List<CardView>();

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
                Model.EventManager.AddAction($"{_nameCollection}Changed", MoveCards);
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
                Model.EventManager.RemoveAction($"{_nameCollection}Changed", MoveCards);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            foreach (CardView card in _collection)
                UnSubscribe(card);
        }

        private void MoveCards()
        {
            string cardId = Model.Get<List<CardData>>(_nameCollection)[^1].ID;

            CardView card = _factory.GetCard(cardId, _nameCollection, _parent);
            _collection.Add(card);
            card.CgangedCollectionHandler += ChangedCollection;

            StartMove(cardId);
        }

        private void ChangedCollection(CardView card)
        {
            UnSubscribe(card);
            _collection.Remove(card);
            StartMove(string.Empty);
        }

        private void StartMove(string cardId)
        {
            for (int i = 0; i < _collection.Count; i++)
            {
                CardView card = _collection[i];

                Vector2 targetPoint = GetTargetPoint(_collection.Count, i, _parent.position);

                card.MoveTo(targetPoint, i, callBack: card.Id == cardId, _colliderEnable);
            }
        }

        private Vector2 GetTargetPoint(int count, int index, Vector2 defaultPosition)
        {
            Vector2 position = Vector2.zero;

            position.y = Random.Range(defaultPosition.y - OffsetY, defaultPosition.y + OffsetY);

            float startPointX = -(float)(count - 1) / 2 * DistanceBetweenCards;
            position.x = startPointX + DistanceBetweenCards * index;

            return position;
        }

        private void UnSubscribe(CardView card) =>
            card.CgangedCollectionHandler -= ChangedCollection;
    }
}