using System.ComponentModel.DataAnnotations;

namespace Hotel.DAL.Entities
{
    public class User
    {
        [Key, MaxLength(30)]
        public string Login { get; set; }
        [Required, MaxLength(30)]
        public string PasswordHash { get; set; }
        [Required]
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin,
        User
    }
}
