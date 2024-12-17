using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Image { get; set; } = null!;
        public int CompanyId { get; set; }
    }
}
