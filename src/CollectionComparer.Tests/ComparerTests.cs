
using System.Collections.Generic;
using Xunit;

using CollectionComparer.Extensions;

namespace CollectionComparer.Tests
{
    public class ComparerTests
    {
        [Fact]
        public void Comparer_OnlyInComparingCollection_ItemIsFound()
        {
            var source = new []{1, 3, 4};
            var collection = new[] {1, 2, 3, 4};

            const int value = 2;
            var results = new List<int>();

            source.CompareTo(collection, (s, d) => s == d)
                .OnlyInComparingCollection(s=> results.Add(s))
                .Process();
            
            Assert.Contains(value, results);
        }
        
        [Fact]
        public void Comparer_InBoth_ItemExists()
        {
            var source = new []{1, 2, 3, 4, 5};
            var collection = new[] {-1, 2, -3, -4};

            const int value = 2;
            var results = new List<int>();

            source.CompareTo(collection, (s, d) => s == d)
                .InBoth(s=> results.Add(s))
                .Process();
            
            Assert.Contains(value, results);
        }
        
        [Fact]
        public void Comparer_OnlyInSourceCollection_ItemIsFound()
        {
            var source = new []{1, 2, 3, 4};
            var collection = new[] {1, 3, 4};

            const int value = 2;
            var results = new List<int>();

            source.CompareTo(collection, (s, d) => s == d)
                .OnlyInSourceCollection(s=> results.Add(s))
                .Process();
            
            Assert.Contains(value, results);
        }
    }
}