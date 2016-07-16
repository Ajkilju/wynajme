using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wynajme_AspNetCore_v2.Data;
using Wynajme_AspNetCore_v2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Wynajme_AspNetCore_v2.Repository
{
    public class ManageRepository : IManageRepository
    {
        private ApplicationDbContext _context;

        public ManageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsAnyUserRegistered()
        {
            var user = _context.Users.FirstOrDefault();

            if(user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetRegisterdeUsersCount()
        {
            return _context.Users.Count();
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

        public IQueryable<ApplicationUser> GetUsers()
        {
            return _context.Users;
        }

        public IQueryable<ApplicationUser> GetAdmins()
        {
            //return _context.Users.Where(x => x.Roles.Select(role => role.RoleId).Contains("Admin"));

            return _context.Users;
        }

        public void DeleteUser(ApplicationUser user)
        {
            _context.Remove(user);
        }

        public void UpdateUser(ApplicationUser user)
        {
            _context.Update(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
