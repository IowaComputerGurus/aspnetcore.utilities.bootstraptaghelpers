using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests
{
    public class AlertTagHelperTests
    {
        [Theory]
        [InlineData(BootstrapColor.Primary, "alert alert-primary")]
        [InlineData(BootstrapColor.Secondary, "alert alert-secondary")]
        [InlineData(BootstrapColor.Success, "alert alert-success")]
        [InlineData(BootstrapColor.Warning, "alert alert-warning")]
        [InlineData(BootstrapColor.Info, "alert alert-info")]
        [InlineData(BootstrapColor.Danger, "alert alert-danger")]
        public void Should_Render_ProperClass(BootstrapColor color, string expectedClass)
        {
            //Arrange
            var context = MakeTagHelperContext();
            var output = MakeTagHelperOutput(" ");

            //Act
            var helper = new AlertTagHelper() { AlertColor = color};
            helper.Process(context, output);

            //Assert
            Assert.Equal("div", output.TagName);
            Assert.Equal(expectedClass, output.Attributes["class"].Value);
        }

        [Fact]
        public void Should_Render_With_RoleAdded()
        {
            //Arrange
            var context = MakeTagHelperContext();
            var output = MakeTagHelperOutput(" ");

            //Act
            var helper = new AlertTagHelper() { AlertColor = BootstrapColor.Info };
            helper.Process(context, output);

            //Assert
            Assert.Equal("div", output.TagName);
            Assert.Equal("alert", output.Attributes["role"].Value);
        }

        private TagHelperContext MakeTagHelperContext(TagHelperAttributeList attributes = null)
        {
            attributes = attributes ?? new TagHelperAttributeList();

            return new TagHelperContext(
                tagName: "div",
                allAttributes: attributes,
                items: new Dictionary<object, object>(),
                uniqueId: Guid.NewGuid().ToString("N"));
        }

        private TagHelperOutput MakeTagHelperOutput(
            string tagName,
            TagHelperAttributeList attributes = null,
            string childContent = null)
        {
            attributes = attributes ?? new TagHelperAttributeList();

            return new TagHelperOutput(
                tagName,
                attributes,
                getChildContentAsync: (useCachedResult, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetContent(childContent);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
        }
    }
}
