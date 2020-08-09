using System;
using System.Collections.Generic;

namespace CollectionComparer.Extensions
{
    public static class CollectionExtensions
    {
        public static Comparer<TType> CompareTo<TType>(this IEnumerable<TType> source, IEnumerable<TType> collection, Func<TType, TType, bool> comparer)
        {
            var merger =  new Comparer<TType>(new EqualityComparer<TType>(comparer));
            return merger.SetCollections(source, collection);
        }
    }
}