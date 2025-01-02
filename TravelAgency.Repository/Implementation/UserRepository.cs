using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Identity;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Customer> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Customer>();
        }
        public IEnumerable<Customer> GetAll()
        {
            return entities.AsEnumerable();
        }

        public Customer Get(string id)
        {
            var strGuid = id.ToString();
            return entities
                .Include(z => z.Bookings)
                .ThenInclude(z => z.TravelPackage)
                .First(s => s.Id == strGuid);
        }
        public void Insert(Customer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Customer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Customer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

    }

}
