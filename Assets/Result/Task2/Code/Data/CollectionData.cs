using System.Collections.Generic;

namespace Result.Task2.Code.Data
{
    public class CollectionData
    {
        public CardCollectionType Type { get; private set; }
        public List<CardData> CardDatas { get; private set; }

        public CollectionData(CardCollectionType type, List<CardData> data)
        {
            Type = type;
            CardDatas = data;
        }
    }
}