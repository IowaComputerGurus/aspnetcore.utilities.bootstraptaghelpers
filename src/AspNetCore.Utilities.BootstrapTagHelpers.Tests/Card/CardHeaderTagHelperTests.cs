using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Card;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Card
{
    public class CardHeaderTagHelperTests : AbstractTagHelperTest
    {
        [Fact]
        public async Task Should_ThrowException_WhenMissingContext()
        {
            //Arrange
            var context = MakeTagHelperContext();
            var output = MakeTagHelperOutput(" ");

            //Act
            var helper = new CardHeaderTagHelper();
            var exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

            Assert.NotNull(exceptionResult);
            Assert.IsType<KeyNotFoundException>(exceptionResult);
        }

        [Fact]
        public async Task Should_Render_As_Div_WhenContextAvailable()
        {
            //Arrange
            var context = MakeTagHelperContext();
            context.Items.Add(typeof(CardContext), new CardContext());
            var output = MakeTagHelperOutput(" ");

            //Act
            var helper = new CardHeaderTagHelper();
            await helper.ProcessAsync(context, output);

            //Assert
            Assert.Equal("div", output.TagName);
        }

        [Fact]
        public async Task Should_Render_With_ClassAdded()
        {
            //Arrange
            var context = MakeTagHelperContext();
            context.Items.Add(typeof(CardContext), new CardContext());
            var output = MakeTagHelperOutput(" ");

            //Act
            var helper = new CardHeaderTagHelper();
            await helper.ProcessAsync(context, output);

            //Assert
            Assert.Equal("card-header", output.Attributes["class"].Value);
        }

        [Fact]
        public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
        {
            //Arrange
            var customClass = "testing-out";
            var expectedClass = $"{customClass} card-header";
            var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
                {new("class", customClass)});
            var context = MakeTagHelperContext();
            context.Items.Add(typeof(CardContext), new CardContext());
            var output = MakeTagHelperOutput(" ", existingAttributes);

            //Act
            var helper = new CardHeaderTagHelper();
            await helper.ProcessAsync(context, output);
            
            //Assert
            Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
        }
    }
}
