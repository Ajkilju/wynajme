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
    public enum TrackingManage
    {
        AsNoTracking,
        Tracking
    }

    public enum DaneUzytkownika
    {
        Wszystko,
        Podstawowe
    }

    public class ManageRepository : IManageRepository
    {
        private ApplicationDbContext _context;

        public ManageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> PobierzUzytkownikaAsync(string Id, DaneUzytkownika dane, TrackingManage tracking)
        {
            if(Id == null)
            {
                return null;
            }

            if (dane == DaneUzytkownika.Podstawowe)
            {
                if (tracking == TrackingManage.AsNoTracking)
                {
                    return await _context.Users.AsNoTracking().SingleAsync(m => m.Id == Id);
                }
                return await _context.Users.SingleAsync(m => m.Id == Id);
            }
            else
            {
                if (tracking == TrackingManage.AsNoTracking)
                {
                    return await _context.Users
                        .Include(m => m.Ogloszenia).ThenInclude(k => k.Kategoria)
                        .Include(m => m.Ogloszenia).ThenInclude(k => k.Miasto)
                        .Include(m => m.Obserwowane).ThenInclude(k => k.Ogloszenie)
                        .AsNoTracking().SingleAsync(m => m.Id == Id);
                }
                return await _context.Users
                        .Include(m => m.Ogloszenia).ThenInclude(k => k.Kategoria)
                        .Include(m => m.Ogloszenia).ThenInclude(k => k.Miasto)
                        .Include(m => m.Obserwowane).ThenInclude(k => k.Ogloszenie)
                        .SingleAsync(m => m.Id == Id);
            }
        }

        public void AktualizujUzytkownika(ApplicationUser user)
        {
            _context.Update(user);
        }

        public int LiczbaZarejestrowanychUzytkownikow()
        {
            return _context.Users.Count();
        }

        public IQueryable<ApplicationUser> PobierzUzytkownikow()
        {
            return _context.Users;
        }

        public IQueryable<ApplicationUser> PobierzAdministratorow()
        {
            //return _context.Users.Where(x => x.Roles.Select(role => role.RoleId).Contains("Admin"));

            return _context.Users;
        }

        public void UsunUzytkownika(ApplicationUser user)
        {
            _context.Remove(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
