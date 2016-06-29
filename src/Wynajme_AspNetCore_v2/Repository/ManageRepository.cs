using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wynajme_AspNetCore_v2.Data;
using Wynajme_AspNetCore_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Wynajme_AspNetCore_v2.Repository
{
    public class ManageRepository : IManageRepository
    {
        private ApplicationDbContext _context;

        public ManageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string Id)
        {
            return  _context.Users
                .Include(m => m.Ogloszenia).ThenInclude(k => k.Kategoria)
                .Include(m => m.Ogloszenia).ThenInclude(k => k.Miasto)
                .Single(m => m.Id == Id);
        }

        public ApplicationUser GetUserAllData(string Id)
        {
            return _context.Users
                .Include(m => m.Ogloszenia).ThenInclude(k => k.Kategoria)
                .Include(m => m.Ogloszenia).ThenInclude(k => k.Miasto)
                .Include(m => m.Obserwowane).ThenInclude(k => k.Ogloszenie)
                .Single(m => m.Id == Id);
        }

        public void UpdateUser(ApplicationUser user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

    }
}
