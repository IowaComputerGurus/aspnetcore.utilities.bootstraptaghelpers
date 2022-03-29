﻿using System;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal
{
    /// <summary>
    /// Renders a modal header content 
    /// </summary>
    public class ModalHeaderTagHelper : TagHelper
    {
        /// <summary>
        /// The optional title to render for this particular title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Completes the actual rendering of the Tag helper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
            if (!string.IsNullOrEmpty(Title))
            {
                var titleTag = new TagBuilder("h5");
                titleTag.Attributes.Add("class", "modal-title");
                titleTag.Attributes.Add("id", $"{modalContext.Id}Label");
                titleTag.InnerHtml.Append(Title);
                output.Content.AppendHtml(titleTag);
            }

            //Append other items, such as the dismiss button etc
            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();
            output.Content.AppendHtml(body);
        }
    }
}
