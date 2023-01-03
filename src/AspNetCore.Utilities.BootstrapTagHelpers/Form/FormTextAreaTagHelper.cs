using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Form
{
    /// <summary>
    ///     TagHelper for rending Bootstrap form compliant textarea controls with support for ASP.NET Core model Binding.  Will
    ///     include Label, Field, and validation.
    /// </summary>
    [ExcludeFromCodeCoverage] //Excluding from code coverage due to complexity 
    [RestrictChildren("form-note")]
    public class FormTextAreaTagHelper : TextAreaTagHelper, IFormElementMixin
    {
        /// <inheritdoc />
        public IHtmlGenerator HtmlGenerator { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="generator">Html Generator for field generation</param>
        public FormTextAreaTagHelper(IHtmlGenerator generator) : base(generator)
        {
            HtmlGenerator = generator;
        }

        /// <summary>
        ///     Used to actually process the tag helper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //The Microsoft implementation of the TextArea tag helper doesn't actually load sub-controls so we need to
            var childContent = await output.GetChildContentAsync();

            //Call our base implementation
            base.Process(context, output);

            //Set our tag name
            output.TagName = "textarea";

            //Add the form-control class
            output.AddClass("form-control", HtmlEncoder.Default);

            //Add before div
            this.StartFormGroup(output);
            
            //Generate our label
            this.AddLabel(output);

            //Now, add validation message AFTER the field
            this.AddValidationMessage(output);

            //Add child content if we have it
            if(!childContent.IsEmptyOrWhiteSpace)
                output.PostElement.AppendHtml(childContent);

            //Close wrapping div
            this.EndFormGroup(output);
        }
    }
}
