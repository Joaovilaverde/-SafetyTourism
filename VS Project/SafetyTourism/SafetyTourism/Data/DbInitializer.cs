using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafetyTourism.Models;

namespace SafetyTourism.Data
{
    public class DbInitializer
    {
        public static void Initialize(SafetyContext context)
        {
            context.Database.EnsureCreated();

            //Ver se existem destinos na base de dados

            if (context.Destinos.Any())
            {
                return;   //Se existir DB has been seeded
            }

            //Preencher Utilizadores

            var utilizadores = new Utilizador[]
            {
            new Utilizador{Nome="Daniel",Password="123",Email="dcmacedo02@upskill.pt",Morada="Porto",Tel=911999111},
            new Utilizador{Nome="João",Password="123",Email="nuno-maister@upskill.pt",Morada="Vila Verde",Tel=911999112},
            new Utilizador{Nome="Zé",Password="123",Email="jpmfranco@upskill.pt",Morada="Custoias",Tel=911999113},
            new Utilizador{Nome="Inês",Password="123",Email="ines.machado.oliveira@upskill.pt",Morada="Famalicão",Tel=911999114},
            new Utilizador{Nome="Raphael",Password="123",Email="raphael.silva.rdcs@upskill.pt",Morada="Paços de Ferreira",Tel=911999115},
            new Utilizador{Nome="Carlos",Password="123",Email="cgf@upskill.pt",Morada="Foz",Tel=911999116},
            new Utilizador{Nome="Paulo",Password="123",Email="prp@upskill.pt",Morada="Jacarta",Tel=911999117},
            new Utilizador{Nome="Paulo",Password="123",Email="pbs@upskill.pt",Morada="Tatooine",Tel=911999118},
            new Utilizador{Nome="Paulo",Password="123",Email="psm@upskill.pt",Morada="Copertino",Tel=911999119},
            new Utilizador{Nome="Constantino",Password="123",Email="acm@isep.pt",Morada="Rãs",Tel=911999120},
            new Utilizador{Nome="Susana",Password="123",Email="srocha@iscap.ipp.pt",Morada="Dubai",Tel=911999121},
            new Utilizador{Nome="Ana",Password="123",Email="ana.godinho@ptmss.com.pt",Morada="Cascais",Tel=962057406}
            };
            foreach (Utilizador u in utilizadores)
            {
                context.Utilizadores.Add(u);
            }
            context.SaveChanges();

            //Preencher Funcionários

            var funcionarios = new Funcionario[]
            {
            new Funcionario{Nome="Daniel Admin",Password="123",Email="dcmacedo02@upskill.pt"},
            new Funcionario{Nome="João Admin",Password="123",Email="nuno-maister@upskill.pt"},
            new Funcionario{Nome="Zé Admin",Password="123",Email="jpmfranco@upskill.pt"},
            new Funcionario{Nome="Inês Admin",Password="123",Email="ines.machado.oliveira@upskill.pt"},
            new Funcionario{Nome="Raphael Admin",Password="123",Email="raphael.silva.rdcs@upskill.pt"}
            };
            foreach (Funcionario f in funcionarios)
            {
                context.Funcionarios.Add(f);
            }
            context.SaveChanges();

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
            
            //Preencher AfectadosPor

            var afectados = new AfectadoPor[]
            {
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-11"), InfectadosPor100k=100, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-18"), InfectadosPor100k=500, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Malária").DoencaId, Data=DateTime.Parse("2020-12-18"), InfectadosPor100k=1999, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Dubai").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Tatooine").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Hyperbeam").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=100000, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Death Star 1").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Algarve").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Ingleses").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=3, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Japão").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Minas Tirith").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Shire").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Mordor").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=50, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Rivendell").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Malária").DoencaId, Data=DateTime.Parse("2020-12-04"), InfectadosPor100k=500, Gravidade=""},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Rivendell").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Malária").DoencaId, Data=DateTime.Parse("2020-12-11"), InfectadosPor100k=1337, Gravidade=""},
            };
            foreach (AfectadoPor a in afectados)
            {
                context.Afectados.Add(a);
            }
            context.SaveChanges();
        }
    }
}
