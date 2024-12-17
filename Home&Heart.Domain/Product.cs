using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public byte[] Image { get; set; } = null!;
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; } = null!;
    }
}
