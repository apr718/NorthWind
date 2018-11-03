using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace NorthWind.Middelware
{
    public class ImageCacheMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly IImageCacheOptions<ImageCacheOptions> _cashe;
        private const string CacheDirectoryPath = @"\AspNetCore\categoryId";

        public ImageCacheMiddleware(RequestDelegate next)
        {
            _nextDelegate = next;
            //_cashe = cache;
        }

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
                var categoryId = uri.Substring(8);
                if (!File.Exists(Path.Combine(CacheDirectoryPath, categoryId + ".bmp")))
                {
                    var destPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + CacheDirectoryPath;

                    var originalBodyStream = context.Response.Body;

                    using (var responseBody = new MemoryStream())
                    {

                        context.Response.Body = responseBody;

                        await _nextDelegate.Invoke(context);

                        await responseBody.CopyToAsync(originalBodyStream);

                        await FormatResponse(context.Response);

                        await responseBody.CopyToAsync(originalBodyStream);

                        var img = Image.FromStream(context.Response.Body);

                        img.Save(destPath + $"{categoryId}.bmp", ImageFormat.Bmp);
                    }
                }
            }
            else
            {
                await _nextDelegate.Invoke(context);
            }
            
        }
    }
}