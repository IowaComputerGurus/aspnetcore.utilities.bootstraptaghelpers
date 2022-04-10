using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Utilities.BootstrapTagHelpers.Sample.Models;

public enum SampleEnum
{
    [Display(Name = "My First item")]
    Item1 = 1,

    [Display(Name = "My Second Item")]
    Item2 = 3
}