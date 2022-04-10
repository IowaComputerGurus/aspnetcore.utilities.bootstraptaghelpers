using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Card
{
    /// <summary>
    /// Tag Helper for rendering the header for a card
    /// </summary>
    [RestrictChildren("card-header-actions")]
    public class CardHeaderTagHelper : TagHelper
    {
        /// <summary>
        /// The title of the header
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Renders the header for a bootstrap card
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //Get the context information
            if (context.Items[typeof(CardContext)] is not CardContext cardContext)
                throw new ArgumentException("CardContext is not specified in context parameter");

            return ProcessAsyncInternal(output, cardContext);
        }

        private async Task ProcessAsyncInternal(TagHelperOutput output, CardContext cardContext)
        {
            //Setup basic tag information
            output.TagName = "div";
            output.AddClass("card-header", HtmlEncoder.Default);

            //Get sub controls if we need them
            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();

            var headerContent = new TagBuilder("div");
            headerContent.AddCssClass("d-md-flex");
            headerContent.AddCssClass("align-items-center");
            headerContent.AddCssClass("w-100");

            // Render title if given
            if (!string.IsNullOrEmpty(Title))
            {
                var titleTag = new TagBuilder("h5");

                //Add custom id value if we can, otherwise it will not be unique
                if (!string.IsNullOrEmpty(cardContext.Id))
                    titleTag.Attributes.Add("id", $"{cardContext.Id}Label");
                titleTag.InnerHtml.Append(Title);

                headerContent.InnerHtml.AppendHtml(titleTag);
            }

            //Add sub-content after our title
            if (!string.IsNullOrEmpty(body))
                headerContent.InnerHtml.AppendHtml(body);

            //Add to the display
            output.Content.AppendHtml(headerContent);
        }
    }
}
