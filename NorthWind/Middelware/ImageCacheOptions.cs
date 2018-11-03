using System;

namespace NorthWind.Middelware
{
    public interface IImageCacheOptions
    {
        string DirPath { get; set; }
        int MaxCount { get; set; }
        DateTime CacheTime { get; set; }
    }

    public class ImageCacheOptions : IImageCacheOptions
    {
        public string DirPath  { get; set; }
        public int MaxCount { get; set; }
        public DateTime CacheTime { get; set; }
    }
}
