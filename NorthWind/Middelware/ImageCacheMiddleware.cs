using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace NorthWind.Middelware
{
    public class ImageCacheMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public ImageCacheMiddleware(RequestDelegate next, IOptions<ImageCache> options)
        {
            _nextDelegate = next;
            ImageCache = options.Value;
        }

        public ImageCache ImageCache { get; }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {text}";
        }

        public async Task Invoke(HttpContext context)
        {
            var route = context.Request.Path;
            var uri = route.ToUriComponent();
            

            var isImage = route.ToUriComponent().Contains("images");
            
            
            if (isImage)
            {
                var destPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + ImageCache?.CacheDirectoryPath + ImageCache?.ImageFileName;
                var categoryId = uri.Substring(8);
                if (!File.Exists(destPath + $"{categoryId}.bmp"))
                {
                    var originalBodyStream = context.Response.Body;

                    using (var responseBody = new MemoryStream())
                    {

                        context.Response.Body = responseBody;

                        await _nextDelegate.Invoke(context);

                        await responseBody.CopyToAsync(originalBodyStream);

                        await FormatResponse(context.Response);

                        await responseBody.CopyToAsync(originalBodyStream);

                        var img = Image.FromStream(context.Response.Body);

                        DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + ImageCache?.CacheDirectoryPath);
                        var files = dir.GetFiles();
                        

                        foreach (FileInfo fi in files)
                        {
                            var creationTime = fi.CreationTime;

                            if (ImageCache?.CacheTimeMinute != null && creationTime < (DateTime.Now - new TimeSpan(0, 0, (int) ImageCache?.CacheTimeMinute, 0)))
                            {
                                fi.Delete();
                            }
                        }

                        files = dir.GetFiles();

                        int imageCount = files.Length;
                        if (imageCount > ImageCache?.MaxCountOfCachingImages)
                        {
                            var delCount = imageCount - ImageCache?.MaxCountOfCachingImages;
                            for (int i = 0; i < delCount; i++)
                            {
                               files[i].Delete();
                            }
                        }
                        img.Save(destPath + $"{categoryId}.bmp", ImageFormat.Bmp);
                    }
                }
                else
                {
                        byte[] imgdata = File.ReadAllBytes(destPath + $"{categoryId}.bmp");

                        await context.Response.Body.WriteAsync(imgdata, 0, imgdata.Length);
                }
            }
            else
            {
                await _nextDelegate.Invoke(context);
            }
            
        }
    }
}