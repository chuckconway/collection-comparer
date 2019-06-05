using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionComparer
{
    public class Comparer<TType>
    {
        private readonly IEqualityComparer<TType> _comparer;

        public Comparer(IEqualityComparer<TType> comparer)
        {
            _comparer = comparer;
        }

        private IEnumerable<TType> _source;
        private IEnumerable<TType> _collection;
        private Action<TType> _onlyInComparingCollection = s => { };
        private Action<TType> _inBoth = s => { };
        private Action<TType> _onlyInSourceCollection = s => { };

        /// <summary>
        /// Item is only found in the comparing collection.
        ///
        /// A use case could be adding this item to the source collection
        /// </summary>
        /// <param name="onlyInComparingCollection"></param>
        /// <returns></returns>
        public Comparer<TType> OnlyInComparingCollection(Action<TType> onlyInComparingCollection)
        {
            _onlyInComparingCollection = onlyInComparingCollection;
            return this;
        }

        /// <summary>
        /// Item found in both collections.
        ///
        /// A use case could be updating an existing item with new values.
        /// </summary>
        /// <param name="inBoth"></param>
        /// <returns></returns>
        public Comparer<TType> InBoth(Action<TType> inBoth)
        {
            _inBoth = inBoth;
            return this;
        }

        /// <summary>
        /// The item is only in the source collection.
        /// 
        /// A use case could be removing this item from the source collection.
        /// </summary>
        /// <param name="onlyInSourceCollection"></param>
        /// <returns></returns>
        public Comparer<TType> OnlyInSourceCollection(Action<TType> onlyInSourceCollection)
        {
            _onlyInSourceCollection = onlyInSourceCollection;
            return this;
        }

        public Comparer<TType> SetCollections(IEnumerable<TType> source, IEnumerable<TType> collection)
        {
            _source = source;
            _collection = collection;
            return this;
        }

        /// <summary>
        /// Compare the collections
        /// </summary>
        public void Process()
        {
            foreach (var item in _collection)
            {
                var contains = _source.Contains<TType>(item, _comparer);

                if (contains) // InBoth
                {
                    _inBoth(item);
                }
                else //Add
                {
                    _onlyInComparingCollection(item);
                }
            }

            foreach (var item in _source)
            {
                var contains = _collection.Contains(item, _comparer);

                if (!contains)
                {
                    _onlyInSourceCollection(item);
                }
            }
        }
    }
}