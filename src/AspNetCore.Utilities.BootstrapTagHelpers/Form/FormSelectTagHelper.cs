using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Form
{
    /// <summary>
    /// Custom implementation of the select tag helper
    /// </summary>
    [ExcludeFromCodeCoverage]  //Excluded due to HTML Generator usage
    public class FormSelectTagHelper : SelectTagHelper
    {
        private readonly IHtmlGenerator _generator;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="generator">Html Generator for field generation</param>
        public FormSelectTagHelper(IHtmlGenerator generator) : base(generator)
        {
            _generator = generator;
        }

        /// <summary>
        /// Allows the addition of a note to the field
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     Used to actually process the tag helper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Call our base implementation
            base.Process(context, output);

            //Set our tag name
            output.TagName = "select";

            //Add the form-control class
            output.AddClass("form-control", HtmlEncoder.Default);

            //Add before div
            output.PreElement.AppendHtml("<div class=\"form-group\">");

            //Generate our label
            var label = _generator.GenerateLabel(
                ViewContext,
                For.ModelExplorer,
                For.Name, null,
                new { @class = "control-label" });
            output.PreElement.AppendHtml(label);


            //Now, add validation message AFTER the field
            var validationMsg = _generator.GenerateValidationMessage(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                null,
                ViewContext.ValidationMessageElement,
                new { @class = "text-danger" });
            output.PostElement.AppendHtml(validationMsg);

            if (!string.IsNullOrEmpty(Note))
                output.PostElement.AppendHtml($"<small class=\"form-text text-muted\">{Note}</small>");

            //Close wrapping div
            output.PostElement.AppendHtml("</div>");
        }
    }
}
