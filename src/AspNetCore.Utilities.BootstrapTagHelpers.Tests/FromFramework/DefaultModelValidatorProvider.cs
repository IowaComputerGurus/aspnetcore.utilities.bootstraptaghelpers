using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.FromFramework;

internal sealed class DefaultModelValidatorProvider : IMetadataBasedModelValidatorProvider
{
    /// <inheritdoc />
    public void CreateValidators(ModelValidatorProviderContext context)
    {
        //Perf: Avoid allocations here
        for (var i = 0; i < context.Results.Count; i++)
        {
            var validatorItem = context.Results[i];

            // Don't overwrite anything that was done by a previous provider.
            if (validatorItem.Validator != null)
            {
                continue;
            }

            if (validatorItem.ValidatorMetadata is IModelValidator validator)
            {
                validatorItem.Validator = validator;
                validatorItem.IsReusable = true;
            }
        }
    }

    public bool HasValidators(Type modelType, IList<object> validatorMetadata)
    {
        for (var i = 0; i < validatorMetadata.Count; i++)
        {
            if (validatorMetadata[i] is IModelValidator)
            {
                return true;
            }
        }

        return false;
    }
}