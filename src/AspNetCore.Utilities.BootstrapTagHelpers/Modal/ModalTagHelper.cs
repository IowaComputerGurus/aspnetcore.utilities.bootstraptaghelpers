using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal
{
    /// <summary>
    /// A high-level wrapper Tag Helper for rendering a bootstrap Modal
    /// </summary>
    [RestrictChildren("modal-body", "modal-header", "modal-footer")]
    public class ModalTagHelper : TagHelper
    {
        /// <summary>
        /// If set to true the background will not be clickable to dismiss the dialog
        /// </summary>
        public bool StaticBackdrop { get; set; } = false;

        /// <summary>
        /// Renders the tag helper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var id = output.Attributes["id"].Value.ToString();
            output.TagName = "div";
            output.Attributes.Add("class", "modal fade");
            output.Attributes.Add("aria-labelledby", $"{id}Label");
            if (StaticBackdrop)
                output.Attributes.Add("data-backdrop", "static");

            var dialogWrapper = new TagBuilder("div");
            dialogWrapper.Attributes.Add("class", "modal-dialog");
            var dialogContent = new TagBuilder("div");
            dialogContent.Attributes.Add("class", "modal-content");
            dialogWrapper.InnerHtml.AppendHtml(dialogContent);

            //Setup context
            var modalContext = new ModalContext{Id = id};
            context.Items[typeof(ModalContext)] = modalContext;

            //Render children now
            var body = (await output.GetChildContentAsync()).GetContent();
            dialogContent.InnerHtml.AppendHtml(body);

            output.Content.AppendHtml(dialogWrapper);
        }
    }
    
}
