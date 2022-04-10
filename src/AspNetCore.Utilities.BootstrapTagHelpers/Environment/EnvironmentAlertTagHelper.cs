using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Environment;

/// <summary>
///     Tag helper allowing the addition of an alert to designate/warn about a specific environment
/// </summary>
public class EnvironmentAlertTagHelper : EnvironmentTagHelper
{
    /// <summary>
    ///     Default constructor
    /// </summary>
    /// <param name="hostingEnvironment"></param>
    public EnvironmentAlertTagHelper(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
    {
    }

    /// <summary>
    ///     If the element is rendered, what color should the alert be
    /// </summary>
    public BootstrapColor AlertColor { get; set; } = BootstrapColor.Warning;

    /// <summary>
    ///     Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);

        //If nothing rendered don't wrap
        if (output.IsContentModified)
            return;

        output.PreContent.AppendHtml($"<div class=\"alert alert-{AlertColor.ToString().ToLower()}\">");
        output.PostContent.AppendHtml("</div");
    }
}