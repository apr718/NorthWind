using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace NorthWind.TagHelpers
{
    public class aTagHelper : TagHelper
    {
        private const string NorthwindIdAttributeName = "northwind-id";

        public int NorthwindId { get; set; }
 
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            //output.Attributes.Add("northwind-id", NorthwindId);
            //var content = await output.GetChildContentAsync();
            //var ct = content.GetContent();
            //var target = "/images/" + NorthwindId;

            //output.Attributes.SetAttribute("href", target );
            
            //output.Content.SetContent(target);
            //output.Content.SetHtmlContent(ct);
        }
    }
}
