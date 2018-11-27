using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthWind.TagHelpers
{
    public static class ImageHtmlHelper
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper htmlHelper, int imageId, string linkText)
            => new HtmlString($"<a href='/images/{imageId}'>{linkText}</a>");
    }
}
