using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.FontAwesomeTagHelpers.Tests
{
    public class BoolFontAwesomeIconTagHelperTests
    {
        [Theory]
        [InlineData(false, "fas fa-times text-danger")]
        [InlineData(true, "fas fa-check text-success")]
        public void ShouldRenderProperIconClass_WithDefaultConfiguration(bool value, string expectedClass)
        {
            //Arrange
            var context = MakeTagHelperContext();
            var output = MakeTagHelperOutput(" ");

            //Act
            var helper = new BoolFontAwesomeIconTagHelper{Value = value};
            helper.Process(context, output);

            //Assert
            Assert.Equal("span", output.TagName);
            Assert.Equal(expectedClass, output.Attributes["class"].Value);
        }

        [Theory]
        [InlineData(false, "fas fa-circle text-success", "fas fa-circle text-warning", "fas fa-circle text-warning")]
        [InlineData(true, "fas fa-circle text-success", "fas fa-circle text-warning", "fas fa-circle text-success")]
        public void ShouldRenderProperIconClass_WithCustomConfiguration(bool value, string yesClass, string noClass,
            string expectedClass)
        {
            //Arrange
            var context = MakeTagHelperContext();
            var output = MakeTagHelperOutput(" ");

            //Act
            var helper = new BoolFontAwesomeIconTagHelper{ Value = value, FalseIconClass = noClass, TrueIconClass = yesClass};
            helper.Process(context, output);

            //Assert
            Assert.Equal("span", output.TagName);
            Assert.Equal(expectedClass, output.Attributes["class"].Value);
        }

        [Theory]
        [InlineData(false, "i")]
        public void ShouldRenderProperTagName_WithCustomTagName(bool value, string tagName)
        {
            //Arrange
            var context = MakeTagHelperContext();
            var output = MakeTagHelperOutput(" ");

            //Act
            var helper = new BoolFontAwesomeIconTagHelper { Value = value, Tag = tagName };
            helper.Process(context, output);

            //Assert
            Assert.Equal(tagName, output.TagName);
        }

        private TagHelperContext MakeTagHelperContext(TagHelperAttributeList attributes = null)
        {
            attributes = attributes ?? new TagHelperAttributeList();

            return new TagHelperContext(
                tagName: "span",
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
