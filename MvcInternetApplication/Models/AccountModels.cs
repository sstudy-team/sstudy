using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace MvcInternetApplication.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("MyConnection")
        {
        }

        public DbSet<UserProfile> UserProfile { get; set; }
		public DbSet<UserCard> UserCards { get; set; }
		public DbSet<Card> Cards { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subsection> Subsections { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }

	[Table("UserCards")]
	public class UserCard
    {
        [Key]
		public int UserCardId { get; set; }
        public int UserId { get; set; }
        public int CardId { get; set; }
    }

	[Table("Cards")]
	public class Card
	{
		public int CardId { get; set; }
		public string Title { get; set; }
		public string Prev { get; set; }
		public string Information { get; set; }
        public Topic TopicId { get; set; }
        public string Subject { get; set; }
        public string Section { get; set; }
        public string Subsection { get; set; }
        public string Topic { get; set; }
    }

    [Table("Subjects")]
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
    }

    [Table("Sections")]
    public class Section
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
    }

    [Table("Subsections")]
    public class Subsection
    {
        public int SubsectionId { get; set; }
        public string Name { get; set; }
        public Section SectionId { get; set; }
    }

    [Table("Topics")]
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public Subsection SubsectionId { get; set; }
    }
    
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
