using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers
{
    public class ModalBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "modal-body");
            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();
            output.Content.AppendHtml(body);
        }
    }
}
