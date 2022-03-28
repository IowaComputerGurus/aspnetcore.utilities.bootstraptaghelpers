using System;
using System.Linq;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers
{
    public class ModalHeaderTagHelper : TagHelper
    {
        public string Title { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //Get the context information
            var modalContext = context.Items[typeof(ModalContext)] as ModalContext;
            if (modalContext == null)
                throw new ArgumentException();

            //Setup basic tag information
            output.TagName = "div";
            output.Attributes.Add("class", "modal-header");

            //Add the title
            var titleTag = new TagBuilder("h5");
            titleTag.Attributes.Add("class", "modal-title");
            titleTag.Attributes.Add("id", $"{modalContext.Id}Label");
            titleTag.InnerHtml.Append(Title);
            output.Content.AppendHtml(titleTag);

            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();
            output.Content.AppendHtml(body);
        }
    }
}
