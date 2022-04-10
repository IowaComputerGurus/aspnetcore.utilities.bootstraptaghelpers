# AspNetCore Bootstrap Tag Helpers ![](https://img.shields.io/github/license/iowacomputergurus/aspnetcore.utilities.bootstraptaghelpers.svg)

A collection of TagHelpers for ASP.NET Core that make utilizing the Bootstrap 4.x library easier to use for developers.  Designed to reduce code effort substantially

![Build Status](https://github.com/IowaComputerGurus/aspnetcore.utilities.bootstraptaghelpers/actions/workflows/ci-build.yml/badge.svg)

![](https://img.shields.io/nuget/v/icg.aspnetcore.utilities.bootstraptaghelpers.svg) ![](https://img.shields.io/nuget/dt/icg.aspnetcore.utilities.bootstraptaghelpers.svg)

## SonarCloud Analysis

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=IowaComputerGurus_aspnetcore.utilities.bootstraptaghelpers&metric=alert_status)](https://sonarcloud.io/dashboard?id=IowaComputerGurus_aspnetcore.utilities.bootstraptaghelpers)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=IowaComputerGurus_aspnetcore.utilities.bootstraptaghelpers&metric=coverage)](https://sonarcloud.io/dashboard?id=IowaComputerGurus_aspnetcore.utilities.bootstraptaghelpers)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=IowaComputerGurus_aspnetcore.utilities.bootstraptaghelpers&metric=security_rating)](https://sonarcloud.io/dashboard?id=IowaComputerGurus_aspnetcore.utilities.bootstraptaghelpers)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=IowaComputerGurus_aspnetcore.utilities.bootstraptaghelpers&metric=sqale_index)](https://sonarcloud.io/dashboard?id=IowaComputerGurus_aspnetcore.utilities.bootstraptaghelpers)

## Usage Expecations

These tag helpers are only for markup display, your web project must properly include references to Bootstrap and must abide by all license and other requirements of Bootstrap for the functionality to be utilized here.  For more on how to include Bootstrap within your project please reference their documentation.


## Setup - Registering TagHelpers

You must modify your `_viewimports.cshtml` file by adding

``` html+razor
@addTagHelper *, ICG.AspNetCore.Utilities.BootstrapTagHelpers
```

## Usage

The goal of these tag helpers is to reduce the reduntant coding, and compliance with various features of not only the boostrap library but form patterns.  Within the "Samples" folder there are examples of all included tag helpers.  However, the below shows a quick example of the power of these helps.

### Before Usage

The following markup is how you would output a model-bound field for a password field, including a note on complexity and validation.

``` razor
<div class="form-group">
    <label asp-for="Password" class="control-label"></label>
    <input asp-for="Password" class="form-control" />
    <span asp-validation-for="Password" class="text-danger"></span>
    <small class="form-text text-muted">Must be 8 characters with letters & numbers</small>
</div>
```

This is a total of *306* characters with spaces or *268* without.  Granted we get some help with auto-complete etc.

### After Using

You can take the entire above example and simplify it to the following

``` razor
<form-text-input asp-for="Password">
    <form-note>Must be 8 characters with letters & numbers</form-note>
</form-text-input>
```

This is a total of *126* characters with spaces or *112* without.  With intellisense, the actual typing characters are much less.  Your form view is also substantially reduced, making lines of code per form reduced.  For forms without notes the markup improvement is even better.


## Included Tag Helpers

At this time tag helpers have been implemented for the following elements.

| Element | Description of Implementation |
| --- | --- |
| Alerts | Full support for implementation of alerts, including dismissable alerts |
| Badges | Full support for implementation of badges of all Bootstrap color variariations |
| Cards | Support for Card, Card Header, Card Header Actions, and Card body elements |
| Input | Support for Form input controls for anything tied to the `<input>` tag including ASP.NET Code Model Binding & Validation |
| Modals | Support for modal dialogs, including Modal Body, header, footer, dismiss, and toggles |
| TextArea | Support for Form input controls tied to the `<textarea>` tag including ASP.NET Core Model Binding & Validation | 
