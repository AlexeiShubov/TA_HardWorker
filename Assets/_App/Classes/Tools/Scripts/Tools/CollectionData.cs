namespace ClassesTools
{
    public class CollectionData
    {
        public readonly int info;

        public int ID;
        public int Priority { get; set; }
        public string CurrentCollectionName { get; set; }

        public CollectionData(int ID, int info, string currentCollectionName, int priority)
        {
            this.ID = ID;
            this.info = info;
            CurrentCollectionName = currentCollectionName;
            Priority = priority;
        }
    }
}