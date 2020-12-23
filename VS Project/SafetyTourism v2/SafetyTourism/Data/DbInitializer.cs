using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafetyTourism.Models;
using Microsoft.AspNetCore.Identity;

namespace SafetyTourism.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAsync(UserManager<Utilizador> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Administrador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Funcionario.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Visitante.ToString()));
        }
        public static async Task SeedAdministradorAsync(UserManager<Utilizador> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Administrador
            var administrador = new Utilizador
            {
                UserName = "administrador",
                Email = "administrador@upskill.pt",
                FirstName = "Jakim",
                LastName = "das Coives"
            };
            if (userManager.Users.All(u => u.Id != administrador.Id))
            {
                var user = await userManager.FindByEmailAsync(administrador.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(administrador, "123Pa$$word");
                    await userManager.AddToRoleAsync(administrador, Enums.Roles.Administrador.ToString());
                }
            }
            //Seed Funcionario
            var funcionario = new Utilizador
            {
                UserName = "funcionario",
                Email = "funcionario@upskill.pt",
                FirstName = "Manel",
                LastName = "dos PCs"
            };
            if (userManager.Users.All(u => u.Id != funcionario.Id))
            {
                var user = await userManager.FindByEmailAsync(funcionario.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(funcionario, "123Pa$$word");
                    await userManager.AddToRoleAsync(funcionario, Enums.Roles.Funcionario.ToString());
                }
            }
        }
    }    
}
