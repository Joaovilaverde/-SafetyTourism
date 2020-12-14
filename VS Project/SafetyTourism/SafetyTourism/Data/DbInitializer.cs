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
            new Utilizador{Email="dcmacedo02@upskill.pt",Morada="Porto",Tel=911999111},
            new Utilizador{Email="nuno-maister@upskill.pt",Morada="Vila Verde",Tel=911999112},
            new Utilizador{Email="jpmfranco@upskill.pt",Morada="Custoias",Tel=911999113},
            new Utilizador{Email="ines.machado.oliveira@upskill.pt",Morada="Famalicão",Tel=911999114},
            new Utilizador{Email="raphael.silva.rdcs@upskill.pt",Morada="Paços de Ferreira",Tel=911999115},
            new Utilizador{Email="cgf@upskill.pt",Morada="Foz",Tel=911999116},
            new Utilizador{Email="prp@upskill.pt",Morada="Jacarta",Tel=911999117},
            new Utilizador{Email="pbs@upskill.pt",Morada="Tatooine",Tel=911999118},
            new Utilizador{Email="psm@upskill.pt",Morada="Copertino",Tel=911999119},
            new Utilizador{Email="acm@isep.pt",Morada="Rãs",Tel=911999120},
            new Utilizador{Email="srocha@iscap.ipp.pt",Morada="Dubai",Tel=911999121},
            new Utilizador{Email="ana.godinho@ptmss.com.pt",Morada="Cascais",Tel=962057406}
            };
            foreach (Utilizador u in utilizadores)
            {
                context.Utilizadores.Add(u);
            }
            context.SaveChanges();

            //Preencher Funcionários

            var funcionarios = new Funcionario[]
            {
            new Funcionario{Email="dcmacedo02@upskill.pt"},
            new Funcionario{Email="nuno-maister@upskill.pt"},
            new Funcionario{Email="jpmfranco@upskill.pt"},
            new Funcionario{Email="ines.machado.oliveira@upskill.pt"},
            new Funcionario{Email="raphael.silva.rdcs@upskill.pt"}
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

            //Preencher Recomendações

            var recomendacoes = new Recomendacao[]
            {
            new Recomendacao{Nome="Covid-19.5", Conteudo="Lavar bem as mãos, não cuspir na boca dos outros, usar máscara, rezar aos santinhos todos para não apanhar."},
            new Recomendacao{Nome="Hyperbeam", Conteudo="Conformar-se com a morte certa."},
            new Recomendacao{Nome="Ingleses", Conteudo="Acabar com o pito da guia, chicken with piri-piri, fechar Nando's, isolar o Allgarve."},
            new Recomendacao{Nome="Malária", Conteudo="Tomar o comprimido quando começar a ver turvo e amarelado."},
            };
            foreach (Recomendacao r in recomendacoes)
            {
                context.Recomendacoes.Add(r);
            }
            context.SaveChanges();

            //Preencher Doenças

            var doencas = new Doenca[]
            {
            new Doenca{Nome="Covid-19.5", Descricao="É uma doença respiratória causada pela infeção com o coronavírus da síndrome respiratória aguda grave 2 (SARS-CoV-2).", RecomendacaoId = recomendacoes.Single( s => s.Nome == "Covid-19.5").RecomendacaoId},
            new Doenca{Nome="Hyperbeam", Descricao="Darth Vader's personal toy.", RecomendacaoId = recomendacoes.Single( s => s.Nome == "Hyperbeam").RecomendacaoId},
            new Doenca{Nome="Ingleses", Descricao="Homo Sapiens Sapiens infectados com inglesisse, viciados em chicken with piri-piri.", RecomendacaoId = recomendacoes.Single( s => s.Nome == "Ingleses").RecomendacaoId},
            new Doenca{Nome="Malária", Descricao="Malária é uma doença infecciosa transmitida por mosquitos e causada por protozoários parasitários do género Plasmodium.", RecomendacaoId = recomendacoes.Single( s => s.Nome == "Malária").RecomendacaoId},
            };
            foreach (Doenca d in doencas)
            {
                context.Doencas.Add(d);
            }
            context.SaveChanges();
            
            //Preencher AfectadosPor

            var afectados = new AfectadoPor[]
            {
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-11"), Gravidade=Gravidade.MuitoMau, InfectadosPor100k=100},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-18"), Gravidade=Gravidade.Pior, InfectadosPor100k=500},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Jacarta").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Malária").DoencaId, Data=DateTime.Parse("2020-12-18"), Gravidade=Gravidade.Péssimo, InfectadosPor100k=1999},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Dubai").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Tatooine").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Hyperbeam").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Apocalipse, InfectadosPor100k=100000},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Death Star 1").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Algarve").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Ingleses").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Bom, InfectadosPor100k=3},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Japão").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Minas Tirith").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Shire").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Mordor").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Covid-19.5").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Rivendell").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Malária").DoencaId, Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Pior, InfectadosPor100k=500},
            new AfectadoPor{DestinoId=destinos.Single(s=>s.Nome=="Rivendell").DestinoId, DoencaId=doencas.Single(s=>s.Nome=="Malária").DoencaId, Data=DateTime.Parse("2020-12-11"), Gravidade=Gravidade.Péssimo, InfectadosPor100k=1337},
            };
            foreach (AfectadoPor a in afectados)
            {
                context.Afectados.Add(a);
            }
            context.SaveChanges();
        }
    }
}
