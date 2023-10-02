using System.ComponentModel.DataAnnotations;

namespace Critoo.Models;

public class ContactForm
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Message { get; set; } = null!;

    [Required]
    public string? RedirectURL { get; set; } = "/contact";
}
