using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers
{
    public class ModalToggleTagHelper : TagHelper
    {
        public string Target { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.Add("data-toggle", "modal");
            output.Attributes.Add("data-target", $"#{Target}");
            output.Attributes.Add("class", "btn btn-primary");
            output.Attributes.Add("type", "button");
        }
    }
}
