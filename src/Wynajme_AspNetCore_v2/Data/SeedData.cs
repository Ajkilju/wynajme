using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wynajme_AspNetCore_v2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Wynajme_AspNetCore_v2.Data
{
    public class SeedData
    {
        public static void MiastaInit(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Miasto.Any())
                {
                    return;   // DB has been seeded
                }

                context.Miasto.AddRange(
                 new Miasto
                 {
                     Nazwa = "Toruń"
                 },
                 new Miasto
                 {
                     Nazwa = "Warszawa"
                 },
                 new Miasto
                 {
                     Nazwa = "Gdańsk"
                 },
                 new Miasto
                 {
                     Nazwa = "Wrocław"
                 },
                 new Miasto
                 {
                     Nazwa = "Kraków"
                 },
                 new Miasto
                 {
                     Nazwa = "Łeba"
                 },
                 new Miasto
                 {
                     Nazwa = "Poznań"
                 },
                 new Miasto
                 {
                     Nazwa = "Katowice"
                 },
                 new Miasto
                 {
                     Nazwa = "Łódź"
                 },
                 new Miasto
                 {
                     Nazwa = "Zakopane"
                 }

            );
                context.SaveChanges();
            }
        }

        public static void KategorieInit(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Kategoria.Any())
                {
                    return;   // DB has been seeded
                }

                
                context.Kategoria.AddRange(
                     new Kategoria
                     {
                         Nazwa = "Dom"
                     },
                     new Kategoria
                     {
                         Nazwa = "Mieszkanie"
                     },
                     new Kategoria
                     {
                         Nazwa = "Pokój"
                     },
                     new Kategoria
                     {
                         Nazwa = "Na weekend"
                     },
                     new Kategoria
                     {
                         Nazwa = "Na wakacje"
                     }
                );
                

                context.SaveChanges();
            }
        }

        public static void AddDatabaseInitialData(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                 serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Ogloszenie.Any())
                {
                    return;   // DB has been seeded
                }
                var kategoriaId = context.Kategoria.Select(k => k.KategoriaId).ToList();
                var miastoId = context.Miasto.Select(m => m.MiastoId).ToList();
                var oglList = OgloszenieGenerator(1000, kategoriaId, miastoId);
                context.Ogloszenie.AddRange(oglList);
                context.SaveChanges();
            }
        }

        public static void AddDatabaseData(IServiceProvider serviceProvider, int howMany)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var kategoriaId = context.Kategoria.Select(k => k.KategoriaId).ToList();
                var miastoId = context.Miasto.Select(m => m.MiastoId).ToList();
                var oglList = OgloszenieGenerator(howMany, kategoriaId, miastoId);
                context.Ogloszenie.AddRange(oglList);
                context.SaveChanges();
            }
        }



        public static List<Ogloszenie> OgloszenieGenerator(int howMany, List<int> kategoriaId, List<int> miastoId )
        {
            string[,] tytulPart = new string[,] {
                { "domek", "dom", "mieszkanie", "pokój", "willa", "chata", "chałupa", "buda dla psa", "rudera", "nora" },
                { "tanio", "od zaraz", "wynajmę", "w centrum", "za miastem", "od ręki", "do remontu", "po remoncie", "po powodzi","nie drogo" },
                { "dla studenta", "dla pary", "dla rodziny", "dobry dojazd", "piękna okolica", "przy lesie", "lubie placki", "kup browary", "na impreze", "bez prądu" },
                { "duża lazienka", "duża kuchnia", "plac zabaw", "przy parku", "bierz pan!", "okazja!", "extra!", "dobry wybór!", "nie bierz!", "oszalałem!" }
            };

            string[] DodatkoweWyposazenie = new string[] { "telewizor", "sprzęt grający", "siłowna", "Bilard", "ping-pong" };

            Random r = new Random();
            List<Ogloszenie> oglList = new List<Ogloszenie>();

            string tytulTmp = "";
            string trescTmp = "";
            string DodatkoweWyp = "";

            for (int i = 0; i < howMany; i++)
            {
                tytulTmp = tytulPart[0, r.Next(0, 10)];
                tytulTmp += " " + tytulPart[r.Next(1, 4), r.Next(0, 10)];
                tytulTmp += " " + tytulPart[r.Next(1, 4), r.Next(0, 10)];

                trescTmp = "";
                for (int j = 0; j < r.Next(40, 300); j++)
                {
                    trescTmp += tytulPart[r.Next(0, 4), r.Next(0, 10)] + " ";
                }

                DodatkoweWyp = "";
                for (int j = 0; j < r.Next(1,6); j++)
                {
                    DodatkoweWyp += DodatkoweWyposazenie[r.Next(1, 5)] + " ";
                }

                oglList.Add(
                    new Ogloszenie
                    {
                        DataDodania = new DateTime(r.Next(2014, 2016), r.Next(1, 13), r.Next(1, 27)),
                        KategoriaId = kategoriaId[r.Next(0, 5)],
                        MiastoId = miastoId[r.Next(0, 10)],
                        Tytul = tytulTmp,
                        Cena = r.Next(1, 200) * 10,
                        Tresc = trescTmp,
                        
                        Powierzchnia = r.Next(30, 200),
                        KuchniaEl = r.Next(1, 3) == 1 ? true : false,
                        KuchniaGaz = r.Next(1, 3) == 1 ? true : false,
                        Lodowka = r.Next(1, 3) == 1 ? true : false,
                        Mikrofala = r.Next(1, 3) == 1 ? true : false,
                        Pralka = r.Next(1, 3) == 1 ? true : false,
                        Prysznic = r.Next(1, 3) == 1 ? true : false,
                        Wanna = r.Next(1, 3) == 1 ? true : false,
                        Zmywarka = r.Next(1, 3) == 1 ? true : false,
                        DodatkoweWyposazenie = DodatkoweWyp,
                        Email = tytulPart[0, r.Next(1, 10)] + "@" + tytulPart[0, r.Next(0, 10)] + ".pl",
                        
                    }
                    );
            }
            return oglList;
        }



        public static void DelateOlgloszenia(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                 serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var ogloszenia = context.Ogloszenie.ToList();
                context.Ogloszenie.RemoveRange(ogloszenia);
                context.SaveChanges();
            }
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            ILogger logger = loggerFactory.CreateLogger<Startup>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User" };

            foreach (string roleName in roleNames)
            {
                bool roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    logger.LogInformation(String.Format("!roleExists for roleName {0}", roleName));
                    IdentityRole identityRole = new IdentityRole(roleName);
                    IdentityResult identityResult = await roleManager.CreateAsync(identityRole);
                    if (!identityResult.Succeeded)
                    {
                        logger.LogCritical(
                            String.Format(
                                "!identityResult.Succeeded after roleManager.CreateAsync(identityRole) for identityRole with roleName { 0}",
                                roleName));
                        foreach (var error in identityResult.Errors)
                        {
                            logger.LogCritical(
                                String.Format(
                                    "identityResult.Error.Description: {0}",
                                    error.Description));
                            logger.LogCritical(
                                String.Format(
                                    "identityResult.Error.Code: {0}",
                                 error.Code));
                        }
                    }
                }
            }
        }

    }
}
