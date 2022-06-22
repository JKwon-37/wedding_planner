#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace WeddingPlanner.Models;

public class UserWeddingAttend
{
    [Key]
    public int UserWeddingAttendId {get;set;}

    public int UserId {get;set;}
    public User? Attender {get;set;}

    public int WeddingId {get;set;}
    public Wedding? Rsvp {get;}
}