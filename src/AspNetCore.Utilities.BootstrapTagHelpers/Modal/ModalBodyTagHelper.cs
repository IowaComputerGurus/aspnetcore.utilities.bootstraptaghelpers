using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;

/// <summary>
///     Builds the content for a Modal Body
/// </summary>
public class ModalBodyTagHelper : TagHelper
{
    /// <summary>
    ///     Renders the body element with the wrapping div and class
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClass("modal-body", HtmlEncoder.Default);
        var body = (await output.GetChildContentAsync()).GetContent();
        body = body.Trim();
        output.Content.AppendHtml(body);
    }
}