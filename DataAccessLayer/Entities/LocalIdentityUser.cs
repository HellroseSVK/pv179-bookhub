using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class LocalIdentityUser : IdentityUser
{
    [ForeignKey("User")]
    public int UserId { get; set; }

    public User? User { get; set; }
}