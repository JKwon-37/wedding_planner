#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models;

[NotMapped]
public class LogUser
{
    [Required(ErrorMessage = " is required!")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string LogEmail {get;set;}

    [Required(ErrorMessage = " is required!")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string LogPw {get;set;}
}