#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models;

public class Wedding
{
    [Key]
    public int WeddingId {get;set;}

    [Required(ErrorMessage = " is required")]
    public string WedderOne {get;set;}

    [Required(ErrorMessage = " is required")]
    public string WedderTwo {get;set;}

    [Required(ErrorMessage = " is required")]
    [DateValidation]
    public DateTime Date {get;set;}

    [Required(ErrorMessage = " is required")]
    public string WeddingDress {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    List<UserWeddingAttend> AttendsWedding {get;set;} = new List<UserWeddingAttend>();
}