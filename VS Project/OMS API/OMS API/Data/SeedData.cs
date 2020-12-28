using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OMS_API.Models;

namespace OMS_API.Data
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Administrador.ToString()));
        }
        public static async Task SeedAdministradorAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Administrador
            var administrador = new ApplicationUser
            {
                UserName = "administrador@upskill.pt",
                Email = "administrador@upskill.pt"
            };
            if (userManager.Users.All(u => u.Id != administrador.Id))
            {
                var user = await userManager.FindByEmailAsync(administrador.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(administrador, "123Pa$$word");
                    await userManager.AddToRoleAsync(administrador, Roles.Administrador.ToString());
                }
            }
            //Seed Funcionario
            var funcionario = new ApplicationUser
            {
                UserName = "cliente@upskill.pt",
                Email = "cliente@upskill.pt"
            };
            if (userManager.Users.All(u => u.Id != funcionario.Id))
            {
                var user = await userManager.FindByEmailAsync(funcionario.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(funcionario, "123Pa$$word");
                }
            }
        }
        public static void Initialize(OMSContext context)
        {
            // Look for any Zonas.
            if (context.Surtos.Any())
            {
                return;   // DB has been seeded
            }

            context.Zonas.AddRange(
                new Zona { Id = "eur", Nome = "Europa" },
                new Zona { Id = "asi", Nome = "Ásia" },
                new Zona { Id = "afr", Nome = "África" },
                new Zona { Id = "amn", Nome = "América do Norte" },
                new Zona { Id = "ams", Nome = "América do Sul" },
                new Zona { Id = "amc", Nome = "América Central" },
                new Zona { Id = "ant", Nome = "Antárctida" },
                new Zona { Id = "oce", Nome = "Oceânia" }
            );
            context.Paises.AddRange(
                new Pais { Id = "pt", Nome = "Portugal", ZonaId = "eur" },
                new Pais { Id = "es", Nome = "Espanha", ZonaId = "eur" },
                new Pais { Id = "ang", Nome = "Angola", ZonaId = "afr" },
                new Pais { Id = "moc", Nome = "Moçambique", ZonaId = "afr" }
            );
            context.Recomendacoes.AddRange(
                new Recomendacao { ZonaId = "eur", Data = DateTime.Parse("2020-2-12"), Validade = DateTime.Parse("2020-2-22"), Informacao = "Lavar as mãos" },
                new Recomendacao { ZonaId = "eur", Data = DateTime.Parse("2020-5-12"), Validade = DateTime.Parse("2021-2-22"), Informacao = "Lavar os pés" },
                new Recomendacao { ZonaId = "eur", Data = DateTime.Parse("2020-7-4"), Validade = DateTime.Parse("2021-7-4"), Informacao = "Festejar" },
                new Recomendacao { ZonaId = "eur", Data = DateTime.Parse("2020-12-27"), Validade = DateTime.Parse("2020-12-31"), Informacao = "Lavar os ouvidos" },
                new Recomendacao { ZonaId = "afr", Data = DateTime.Parse("2018-4-20"), Validade = DateTime.Parse("2018-4-25"), Informacao = "Aceitar a morte certa" },
                new Recomendacao { ZonaId = "ams", Data = DateTime.Parse("2021-6-02"), Validade = DateTime.Parse("2021-7-02"), Informacao = "Usar máscara" },
                new Recomendacao { ZonaId = "oce", Data = DateTime.Parse("1020-1-15"), Validade = DateTime.Parse("1020-4-12"), Informacao = "Deitar tudo a perder" }
            );
            context.Virus.AddRange(
                new Virus { Nome = "Covid" },
                new Virus { Nome = "Pinguins" },
                new Virus { Nome = "Americanos" },
                new Virus { Nome = "Malária" }
            );
            context.Surtos.AddRange(
                new Surto { VirusId = 1, ZonaId = "eur", DataDetecao = DateTime.Parse("2020-12-13"), DataFim = DateTime.Parse("2020-12-20") },
                new Surto { VirusId = 2, ZonaId = "eur", DataDetecao = DateTime.Parse("2020-11-21") },
                new Surto { VirusId = 2, ZonaId = "ant", DataDetecao = DateTime.Parse("2020-10-09"), DataFim = DateTime.Parse("2020-11-09") },
                new Surto { VirusId = 2, ZonaId = "ant", DataDetecao = DateTime.Parse("2020-12-21") },
                new Surto { VirusId = 3, ZonaId = "oce", DataDetecao = DateTime.Parse("2020-12-13"), DataFim = DateTime.Parse("2020-12-20") },
                new Surto { VirusId = 3, ZonaId = "amn", DataDetecao = DateTime.Parse("2020-12-13"), DataFim = DateTime.Parse("2020-12-20") },
                new Surto { VirusId = 3, ZonaId = "ams", DataDetecao = DateTime.Parse("2020-12-20") },
                new Surto { VirusId = 4, ZonaId = "afr", DataDetecao = DateTime.Parse("2020-2-12") }
            );
            context.SaveChanges();
        }
    }
}
