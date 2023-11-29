namespace ClassesTools
{
    public class CollectionData
    {
        public readonly string info;
        
        public string CurrentCollectionName { get; set; }

        public CollectionData(string info, string currentCollectionName)
        {
            this.info = info;
            CurrentCollectionName = currentCollectionName;
        }
    }
}