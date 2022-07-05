using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.FromFramework;

public interface IMetadataBuilder
{
    IMetadataBuilder BindingDetails(Action<BindingMetadata> action);

    IMetadataBuilder DisplayDetails(Action<DisplayMetadata> action);

    IMetadataBuilder ValidationDetails(Action<ValidationMetadata> action);
}