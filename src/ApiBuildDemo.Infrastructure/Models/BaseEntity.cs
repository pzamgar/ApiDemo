using System;

namespace ApiBuildDemo.Infrastructure.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated {get; set;}
        public DateTime DateModified { get; set; }
    }
}