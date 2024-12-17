using Home_Heart.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Contracts
{
    public interface ICompanyAppService
    {
        Task<CompanyDto> AddCompanyAsync(CompanyDto company);
        Task<CompanyDto> GetCompanyByIdAsync(int id);
        Task<List<CompanyDto>> GetAllCompaniesAsync();
        Task<List<CompanyDto>> GetCompaniesByCategoryIdAsync(int categoryId);
    }
}
