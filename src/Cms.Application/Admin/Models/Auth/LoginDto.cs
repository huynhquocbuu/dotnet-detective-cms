using System.ComponentModel.DataAnnotations;

namespace Cms.Application.Admin.Models.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "Vui lòng nhập username!!!")]
    [StringLength(50, ErrorMessage = "username < 100 ký tự!!!")]
    [Display(Name = "Username:")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập password!!!")]
    [StringLength(50, ErrorMessage = "username < 100 ký tự!!!")]
    [Display(Name = "Password:")]
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
    public bool RememberMe { get; set; }
    
}