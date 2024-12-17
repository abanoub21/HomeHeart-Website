using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Catalogue { get; set; } = null!;
        public string Website { get; set; } = string.Empty;
        public bool IsPartner { get; set; }
        public int CategoryId { get; set; }
    }
}
