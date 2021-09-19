using HealhCheck.Domain.Entities;
using System;
using System.Collections.Generic;

namespace HealhCheck.Domain.Repositories
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> Get();
        Company GetById(Guid id);
    }
}