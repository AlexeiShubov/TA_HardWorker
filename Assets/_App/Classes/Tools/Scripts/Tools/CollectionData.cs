namespace ClassesTools
{
    public class CollectionData
    {
        public readonly int info;
        
        public int Priority { get; set; }
        public string CurrentCollectionName { get; set; }

        public CollectionData(int info, string currentCollectionName, int priority)
        {
            this.info = info;
            CurrentCollectionName = currentCollectionName;
            Priority = priority;
        }
    }
}