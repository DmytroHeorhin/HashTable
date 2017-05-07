using System;
using System.Collections.Generic;

namespace HashTable
{
    public class HashTable : IHashTable
    {
        private int _hashLoad;
        private int _hashSize;
        private ChainNode[] _buckets;
        private const float MaxLoadFactor = 0.72f;
        private const int InitialCapacity = 3;

        private bool IsResizeNeeded => (float)_hashLoad / _hashSize > MaxLoadFactor;

        public HashTable() : this(InitialCapacity) {}

        public HashTable(int capacity)
        {
            _hashSize = Util.GetPrime(capacity);
            _buckets = new ChainNode[_hashSize];
        }

        public bool Contains(object key)
        {
            var node = GetNodeByKey(key);
            return node != null;
        }

        public void Add(object key, object value)
        {
            if (Contains(key))
            {
                throw new InvalidOperationException("An entry with provided key is already present in this hash table.");
            }
            var index = GetHash(key);
            var firstNode = _buckets[index];
            if (firstNode == null)
            {
                _buckets[index] = CreateNode(key, value);
                _hashLoad++;

                if (IsResizeNeeded)
                {
                    DoubleSize();
                }
            }
            else
            {
                var currentNode = firstNode;
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                var newNode = CreateNode(key, value);
                currentNode.Next = newNode;
                newNode.Previous = currentNode;
            }
        }

        public object this[object key]
        {
            get
            {
                var node = GetNodeByKey(key);
                if (node == null)
                {
                    throw new KeyNotFoundException();
                }
                return node.Value;
            }

            set
            {
                var node = GetNodeByKey(key);
                if (node != null && value != null)
                {
                    node.Value = value;
                }
                if (node == null)
                {
                    Add(key, value);
                }
                if (value == null)
                {
                    Remove(node);
                }        
            }
        }

        public bool TryGet(object key, out object value)
        {
            var node = GetNodeByKey(key);
            if (node != null)
            {
                value = node.Value;
                return true;
            }
            value = null;
            return false;
        }

        private int GetHash(object key)
        {
            var hashCode = key.GetHashCode();
            return Math.Abs(
                hashCode + 1 + ((hashCode >> 5) + 1) % (_hashSize - 1)
                ) % _hashSize;
        }

        private ChainNode GetNodeByKey(object key)
        {
            var index = GetHash(key);
            var node = _buckets[index];
            while (node != null)
            {
                if (IsKeyOfNode(node, key))
                {
                    return node;
                }
                node = node.Next;
            }
            return null;
        }

        private bool IsKeyOfNode(ChainNode node, object key)
        {
            return key.Equals(node.Key);
        }

        private ChainNode CreateNode(object key, object value)
        {
            return new ChainNode(key, value);
        }

        private void Remove(ChainNode node)
        {
            var nextNode = node.Next;
            if (node.Previous == null)
            {
                _buckets[GetHash(node.Key)] = nextNode;
                if (nextNode == null)
                {
                    _hashLoad--;
                }
            }
            else
            {
                node.Previous.Next = nextNode;
            }
        }

        private void DoubleSize()
        {
            var oldBuckets = _buckets;

            _hashSize = Util.GetPrime(_hashSize * 2);
            _buckets = new ChainNode[_hashSize];
            foreach (var bucket in oldBuckets)
            {
                var node = bucket;
                while (node != null)
                {
                    Add(node.Key, node.Value);
                    node = node.Next;
                }
            }
        }
    }
}
