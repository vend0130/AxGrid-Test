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
        [SerializeField] private CardCollectionType _collectionType;
        [SerializeField] private Transform _parent;
        [SerializeField] private bool _colliderEnableBeforeCardMove = true;

        private const float DistanceBetweenCards = 1.35f;
        private const float OffsetY = .1f;

        private readonly List<CardView> _collection = new List<CardView>();

        private CardsFactory _factory;

        [OnAwake]
        private void AwakeThis() =>
            _factory = GetComponent<CardsFactory>();

        [OnStart]
        private void StartThis()
        {
            Model.EventManager.AddAction($"{_collectionType}Changed", MoveCards);
            Model.EventManager.AddParameterAction<string>($"{_collectionType}RemoveCard", RemoveCard);
        }

        [OnDestroy]
        private void DestroyThis()
        {
            Model.EventManager.RemoveAction($"{_collectionType}Changed", MoveCards);
            Model.EventManager.RemoveParameterAction<string>($"{_collectionType}RemoveCard", RemoveCard);

            foreach (CardView card in _collection)
                card.OnClickHandler -= ClickOnCard;
        }

        private void MoveCards()
        {
            string cardId = Model.Get<CollectionData>(_collectionType.ToString()).CardDatas[^1].ID;

            CardView card = _factory.GetCard(cardId, _parent);
            _collection.Add(card);
            card.OnClickHandler += ClickOnCard;

            StartMove(cardId);
        }

        private void RemoveCard(string cardId)
        {
            CardView card = _collection.Find(card => card.Id == cardId);
            card.OnClickHandler -= ClickOnCard;
            
            _collection.Remove(card);
            StartMove(cardId);
        }

        private void ClickOnCard(CardView card) => 
            Settings.Fsm.Invoke(Keys.ClickOnCard, card.Id, _collectionType);

        private void StartMove(string cardId)
        {
            for (int i = 0; i < _collection.Count; i++)
            {
                CardView card = _collection[i];

                Vector2 targetPoint = GetTargetPoint(_collection.Count, i, _parent.position);

                card.MoveTo(targetPoint, i, callBack: card.Id == cardId, _colliderEnableBeforeCardMove);
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
    }
}