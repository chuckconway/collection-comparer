# Conway Collection Comparer

The Collection Comparer compares two collection and exposes on opprotunity to execute code depending if the item is in the source (left) collection, the comparing (right) collection or both collections.



**For example**, I have two collections. One collection is from the database, a second from the UI.  In the UI the user removes some items and adds some new items. When the user saves the changes we need to reconcile the differences. 



One possiblity is removing all the items and add them anew. The problem is if you have foregin keys, it quickly becomes challenging. An alternative is discovering the differences and update the database accordingly. This is where the Collection Comparer comes in.



An implementation might look like this: 

```c#
private static void MergeAlbums(IEnumerable<int> existing, IEnumerable<int> newAlbums, PhotoSchema photo)
{
    existing.CompareTo(newAlbums, (s, d) => s == d)
        .OnlyInSourceCollection(s =>
        {
           // if only in the database collection, then it's been removed. 
            var album = photo.Albums.SingleOrDefault(x => x.AlbumId == s);
            photo.Albums.Remove(album);
        })
        .OnlyInComparingCollection(s =>
        {
            // if it's only in the comparing collection, then it's new
            // and needs to be added to the database.
            photo.Albums.Add(new PhotoAlbumSchema
            {
                AlbumId = s,
                PhotoId = photo.PhotoId
            });
        })
        .Process();
}
```







Another example:

```c#
var source = new []{1, 3, 4, 6};
var collection = new[] {1, 2, 3, 4, 5};

source.CompareTo(collection, (s, d) => s == d)
    .OnlyInSourceCollection(s=> {/* do something */})
    .OnlyInComparingCollection(s=>{/* do something */})
    .InBoth(s=> {/*do something*/})
    .Process();
```

