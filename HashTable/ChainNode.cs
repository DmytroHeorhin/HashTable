namespace HashTable
{
    internal class ChainNode
    {
        public ChainNode(object key, object value)
        {
            Key = key;
            Value = value;
        }

        public ChainNode Next { get; set; }
        public ChainNode Previous { get; set; }
        public object Key { get; set; }
        public object Value { get; set; }
    }
}
