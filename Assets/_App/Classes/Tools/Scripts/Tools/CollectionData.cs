namespace ClassesTools
{
    public class CollectionData
    {
        public readonly int info;
        
        public string CurrentCollectionName { get; set; }

        public CollectionData(int info, string currentCollectionName)
        {
            this.info = info;
            CurrentCollectionName = currentCollectionName;
        }
    }
}