# AspNetCore Bootstrap Tag Helpers ![](https://img.shields.io/github/license/iowacomputergurus/aspnetcore.utilities.bootstraptaghelpers.svg)

A collection of TagHelpers for ASP.NET Core that make utilizing the Bootstrap 4.x library easier to use for developers

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

## Included Tag Helpers

The following is a short example of the included tag helpers, for full information on the helpers included, please run the "Samples" app, contained within this repository

* Alert
* Modal
