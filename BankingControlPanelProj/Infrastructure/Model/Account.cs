using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingControlPanelProj.Infrastructure.Model
{
    public class Account
    {
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string? Currency { get; set; }
        [ForeignKey("Client")]
        [SwaggerSchema(ReadOnly = true)]
        public Guid ClientId { get; set; }
    }
}
