using System.ComponentModel.DataAnnotations;

namespace Cms.Application.Admin.Models.Product;

public class CreateProductDto : CreateOrUpdateProductDto
{
    [Required]
    public string No { get; set; }
}