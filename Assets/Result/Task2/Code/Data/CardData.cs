namespace Result.Task2.Code.Data
{
    public class CardData
    {
        public string ID { get; private set; }

        public CardData(string id) =>
            ID = id;
    }
}