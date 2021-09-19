using System;

namespace HealhCheck.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
    }
}