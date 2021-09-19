using HealhCheck.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HealhCheck.Api.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
            => _companyRepository = companyRepository;

        [HttpGet("companies")]
        public IActionResult GetCompanies()
            => Ok(_companyRepository.Get());

        [HttpGet("companies/{guid:id}")]
        public IActionResult GetCompany(Guid id)
            => Ok(_companyRepository.GetById(id));
    }
}