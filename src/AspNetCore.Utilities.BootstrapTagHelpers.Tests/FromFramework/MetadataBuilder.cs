using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.FromFramework;

class MetadataBuilder : IMetadataBuilder
{
    readonly List<Action<BindingMetadata>> _bindingActions = new List<Action<BindingMetadata>>();
    readonly List<Action<DisplayMetadata>> _displayActions = new List<Action<DisplayMetadata>>();

    readonly ModelMetadataIdentity _key;
    readonly List<Action<ValidationMetadata>> _valiationActions = new List<Action<ValidationMetadata>>();

    public MetadataBuilder(ModelMetadataIdentity key) { _key = key; }

    public IMetadataBuilder BindingDetails(Action<BindingMetadata> action)
    {
        _bindingActions.Add(action);
        return this;
    }

    public IMetadataBuilder DisplayDetails(Action<DisplayMetadata> action)
    {
        _displayActions.Add(action);
        return this;
    }

    public IMetadataBuilder ValidationDetails(Action<ValidationMetadata> action)
    {
        _valiationActions.Add(action);
        return this;
    }

    public void Apply(BindingMetadataProviderContext context)
    {
        if (_key.Equals(context.Key))
            foreach (var action in _bindingActions)
                action(context.BindingMetadata);
    }

    public void Apply(DisplayMetadataProviderContext context)
    {
        if (_key.Equals(context.Key))
            foreach (var action in _displayActions)
                action(context.DisplayMetadata);
    }

    public void Apply(ValidationMetadataProviderContext context)
    {
        if (_key.Equals(context.Key))
            foreach (var action in _valiationActions)
                action(context.ValidationMetadata);
    }
}