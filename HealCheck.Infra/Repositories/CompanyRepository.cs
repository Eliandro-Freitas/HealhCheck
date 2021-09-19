using HealhCheck.Domain.Entities;
using HealhCheck.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealCheck.Infra.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;
        public CompanyRepository()
        {
            _context = new DataContext();
            DeleteAndCreateDatabase();
        }

        public IEnumerable<Company> Get()
            => _context.Companies.ToList();

        public Company GetById(Guid id)
            => _context.Companies
                .Where(x => id.Equals(x.Id))
                .FirstOrDefault();

        private void DeleteAndCreateDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}