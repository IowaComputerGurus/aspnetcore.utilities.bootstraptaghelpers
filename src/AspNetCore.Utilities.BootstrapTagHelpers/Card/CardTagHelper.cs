using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Card;

/// <summary>
/// A tag helper for rendering a bootstrap card to a view
/// </summary>
[RestrictChildren("card-header", "card-body")]
public class CardTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var id = output.Attributes["id"]?.Value?.ToString();

        output.TagName = "div";

        output.AddClass("card", HtmlEncoder.Default);

        // setup content
        var cardContext = new CardContext {Id = id};
        context.Items[typeof(CardContext)] = cardContext;

        var content = (await output.GetChildContentAsync()).GetContent();

        output.Content.AppendHtml(content);
    }
}