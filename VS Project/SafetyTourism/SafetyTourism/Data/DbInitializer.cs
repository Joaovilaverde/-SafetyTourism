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

            // Look for any students.
            if (context.Utilizadores.Any())
            {
                return;   // DB has been seeded
            }

            var utilizadores = new Utilizador[]
            {
            new Utilizador{UtilizadorId=1, Email="dcmacedo02@upskill.pt",Morada="Porto",Tel=911999111},
            new Utilizador{UtilizadorId=2, Email="nuno-maister@upskill.pt",Morada="Vilaverde",Tel=911999112},
            new Utilizador{UtilizadorId=3, Email="jpmfranco@upskill.pt",Morada="Custoias",Tel=911999113},
            new Utilizador{UtilizadorId=4, Email="ines.machado.oliveira@upskill.pt",Morada="Famalicão",Tel=911999114},
            new Utilizador{UtilizadorId=5, Email="raphael.silva.rdcs@upskill.pt",Morada="Paços de Ferreira",Tel=911999115},
            new Utilizador{UtilizadorId=6, Email="cgf@upskill.pt",Morada="Foz",Tel=911999116},
            new Utilizador{UtilizadorId=7, Email="prp@upskill.pt",Morada="Jacarta",Tel=911999117},
            new Utilizador{UtilizadorId=8, Email="pbs@upskill.pt",Morada="Tatooine",Tel=911999118},
            new Utilizador{UtilizadorId=9, Email="psm@upskill.pt",Morada="Copertino",Tel=911999119},
            new Utilizador{UtilizadorId=10, Email="acm@isep.pt",Morada="Rãs",Tel=911999120},
            new Utilizador{UtilizadorId=11, Email="srocha@iscap.ipp.pt",Morada="Dubai",Tel=911999121},
            new Utilizador{UtilizadorId=12, Email="ana.godinho@ptmss.com.pt",Morada="Cascais",Tel=962057406}
            };
            foreach (Utilizador u in utilizadores)
            {
                context.Utilizadores.Add(u);
            }
            context.SaveChanges();

            var funcionarios = new Funcionario[]
            {
            new Funcionario{FuncionarioId=1, Email="dcmacedo02@upskill.pt"},
            new Funcionario{FuncionarioId=2, Email="nuno-maister@upskill.pt"},
            new Funcionario{FuncionarioId=3, Email="jpmfranco@upskill.pt"},
            new Funcionario{FuncionarioId=4, Email="ines.machado.oliveira@upskill.pt"},
            new Funcionario{FuncionarioId=5, Email="raphael.silva.rdcs@upskill.pt"}
            };
            foreach (Funcionario f in funcionarios)
            {
                context.Funcionarios.Add(f);
            }
            context.SaveChanges();

            var destinos = new Destino[]
            {
            new Destino{DestinoId=1,Nome="Jacarta"},
            new Destino{DestinoId=2,Nome="Dubai"},
            new Destino{DestinoId=3,Nome="Tatooine"},
            new Destino{DestinoId=4,Nome="Death Star 1"},
            new Destino{DestinoId=5,Nome="Algarve"},
            new Destino{DestinoId=6,Nome="Japão"},
            new Destino{DestinoId=7,Nome="Minas Tirith"},
            new Destino{DestinoId=8,Nome="Shire"},
            new Destino{DestinoId=9,Nome="Mordor"},
            new Destino{DestinoId=10,Nome="Rivendell"}
            };
            foreach (Destino d in destinos)
            {
                context.Destinos.Add(d);
            }
            context.SaveChanges();

            var relatorios = new Relatorio[]
            {
            new Relatorio{RelatorioId=1, DestinoId=1, Doenca="Covid-19.5", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new Relatorio{RelatorioId=2, DestinoId=1, Doenca="Covid-19.5", Data=DateTime.Parse("2020-12-11"), Gravidade=Gravidade.MuitoMau, InfectadosPor100k=100},
            new Relatorio{RelatorioId=3, DestinoId=1, Doenca="Covid-19.5", Data=DateTime.Parse("2020-12-18"), Gravidade=Gravidade.Pior, InfectadosPor100k=500},
            new Relatorio{RelatorioId=4, DestinoId=2, Doenca="Covid-19.5", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new Relatorio{RelatorioId=5, DestinoId=3, Doenca="Hyperbeam", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Apocalipse, InfectadosPor100k=100000},
            new Relatorio{RelatorioId=6, DestinoId=4, Doenca="Covid-19.5", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new Relatorio{RelatorioId=7, DestinoId=5, Doenca="Ingleses", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Bom, InfectadosPor100k=3},
            new Relatorio{RelatorioId=8, DestinoId=6, Doenca="Covid-19.5", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new Relatorio{RelatorioId=9, DestinoId=7, Doenca="Covid-19.5", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new Relatorio{RelatorioId=10, DestinoId=8, Doenca="Covid-19.5", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new Relatorio{RelatorioId=11, DestinoId=9, Doenca="Covid-19.5", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Mau, InfectadosPor100k=50},
            new Relatorio{RelatorioId=12, DestinoId=10, Doenca="Malária", Data=DateTime.Parse("2020-12-04"), Gravidade=Gravidade.Pior, InfectadosPor100k=500},
            new Relatorio{RelatorioId=13, DestinoId=10, Doenca="Malária", Data=DateTime.Parse("2020-12-11"), Gravidade=Gravidade.Péssimo, InfectadosPor100k=1337},

            };
            foreach (Relatorio r in relatorios)
            {
                context.Relatorios.Add(r);
            }
            context.SaveChanges();
        }
    }
}
