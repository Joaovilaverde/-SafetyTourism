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
                LastName = "das Coives",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
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
                LastName = "dos PCs",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
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
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            //Ver se existem destinos na base de dados

            if (context.Destinos.Any())
            {
                return;   //Se existir DB has been seeded
            }

            //Preencher Destinos

            var destinos = new Destino[]
            {
            new Destino{Nome="Jacarta"},
            new Destino{Nome="Dubai"},
            new Destino{Nome="Tatooine"},
            new Destino{Nome="Death Star 1"},
            new Destino{Nome="Algarve"},
            new Destino{Nome="Japão"},
            new Destino{Nome="Minas Tirith"},
            new Destino{Nome="Shire"},
            new Destino{Nome="Mordor"},
            new Destino{Nome="Rivendell"}
            };
            foreach (Destino d in destinos)
            {
                context.Destinos.Add(d);
            }
            context.SaveChanges();

            //Preencher Doenças

            var doencas = new Doenca[]
            {
            new Doenca{Nome="Covid-19.5", Descricao="É uma doença respiratória causada pela infeção com o coronavírus da síndrome respiratória aguda grave 2 (SARS-CoV-2).", Recomendacao = "Lavar bem as mãos, não cuspir na boca dos outros, usar máscara, rezar aos santinhos todos para não apanhar."},
            new Doenca{Nome="Hyperbeam", Descricao="Darth Vader's personal toy.", Recomendacao = "Conformar-se com a morte certa."},
            new Doenca{Nome="Ingleses", Descricao="Homo Sapiens Sapiens infectados com inglesisse, viciados em chicken with piri-piri.", Recomendacao = "Acabar com o pito da guia, chicken with piri-piri, fechar Nando's, isolar o Allgarve."},
            new Doenca{Nome="Malária", Descricao="Malária é uma doença infecciosa transmitida por mosquitos e causada por protozoários parasitários do género Plasmodium.", Recomendacao = "Tomar o comprimido quando começar a ver turvo e amarelado."},
            };
            foreach (Doenca d in doencas)
            {
                context.Doencas.Add(d);
            }
            context.SaveChanges();
            
            //Preencher Surtos

            var surtos = new Surto[]
            {
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-11"), InfectadosPor100k=100, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-18"), InfectadosPor100k=500, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Malária").DoencaId, Data=DateTime.Parse("2020-12-18"), InfectadosPor100k=1999, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Dubai").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Tatooine").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Hyperbeam").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=100000, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Death Star 1").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Algarve").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Ingleses").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=3, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Japão").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Minas Tirith").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Shire").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Mordor").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Rivendell").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Malária").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=500, Gravidade=""},
            new Surto{DestinoId=destinos.Single(s=>s.Nome=="Rivendell").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Malária").DoencaId, Data=DateTime.Parse("2020-12-11"), InfectadosPor100k=1337, Gravidade=""},
            };
            foreach (Surto s in surtos)
            {
                context.Surtos.Add(s);
            }
            context.SaveChanges();
        }
    }
}
