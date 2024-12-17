using AutoMapper;
using Home_Heart.Application.Contracts;
using Home_Heart.Application.Dtos;
using Home_Heart.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Services
{
    public class CompanyAppService : ICompanyAppService
    {
        private readonly IAsyncRepository<Company> repository;
        private readonly IMapper mapper;

        public CompanyAppService(IAsyncRepository<Company> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CompanyDto> AddCompanyAsync(CompanyDto company)
        {
            // Set catalogue with allow null 
            byte[] catalogueData = null;
            if (!string.IsNullOrEmpty(company.Catalogue) && File.Exists(company.Catalogue))
            {
                catalogueData = File.ReadAllBytes(company.Catalogue);
            }
            //Set catalogue required  
            //if (string.IsNullOrEmpty(company.Catalogue) || !File.Exists(company.Catalogue))
            //{
            //    throw new FileNotFoundException("The provided pdf path is invalid or the file does not exist.");
            //}

            //byte[] catalogueData = File.ReadAllBytes(company.Catalogue);
            var obj = new Company
            {
                Id = company.Id,
                Name = company.Name,
                Catalogue = catalogueData,
                About = company.About,
                Description = company.Description,
                Website = company.Website,
                IsPartner = company.IsPartner,
                CategoryId = company.CategoryId,
            };
            var addCompany = await repository.AddAsync(obj);
            return mapper.Map<CompanyDto>(addCompany);
        }

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var allData = await repository.GetAllAsync();
            return mapper.Map<List<CompanyDto>>(allData);   
        }

        public async Task<List<CompanyDto>> GetCompaniesByCategoryIdAsync(int categoryId)
        {
            var companies = await repository.GetByForeignKeyAsync(p => p.CategoryId == categoryId);
            var companyDtos = mapper.Map<List<CompanyDto>>(companies);
            foreach (var companyDto in companyDtos)
            {
                var company = companies.FirstOrDefault(p => p.Id == companyDto.Id);
                if (company != null && company.Catalogue != null)
                {
                    companyDto.Catalogue = Convert.ToBase64String(company.Catalogue);
                }
            }
            return companyDtos;
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(int id)
        {
            var compId = await repository.GetByIDAsync(id);
            var companyDto = mapper.Map<CompanyDto>(compId);
            if (compId.Catalogue != null)
            {
                companyDto.Catalogue = Convert.ToBase64String(compId.Catalogue);
            }
            return companyDto;
        }
    }
}
