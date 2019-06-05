# Collection Comparer

Usage:

    var source = new []{1, 3, 4, 6};
    var collection = new[] {1, 2, 3, 4, 5};

    var source = new []{1, 3, 4, 6};
    var collection = new[] {1, 2, 3, 4, 5};

    source.CompareTo(collection, (s, d) => s == d)
        .OnlyInSourceCollection(s=> {/* do something */})
        .OnlyInComparingCollection(s=>{/* do something */})
        .InBoth(s=> {/*do something*/})
        .Process();

