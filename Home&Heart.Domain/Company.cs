using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? About {  get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public byte[]? Catalogue {  get; set; } = null!;
        public string? Website { get; set; } = string.Empty;
        public bool IsPartner { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

    }
}
