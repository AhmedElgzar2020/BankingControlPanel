using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BankingControlPanelProj.Infrastructure.Model
{
    public class Client
    {
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(60, ErrorMessage = "FirstName Max Lenght 60")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(60, ErrorMessage = "LastName Max Lenght 60")]
        public string? LastName { get; set; }
        [Phone(ErrorMessage = "PhoneNumber should be in correct format")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "PersonalId is required")]
        [StringLength(11, ErrorMessage = "PersonalId should be 11 characters")]
        public string? PersonalId { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Sex is required")]
        public Sex Sex { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }

}
