namespace NorthWind.Middelware
{
    public class ImageCache
    {
        public string CacheDirectoryPath { get; set; }
        public string ImageFileName { get; set; }
        public int MaxCountOfCachingImages { get; set; }
        public int CacheTimeMinute { get; set; }

    }
}
