using System.Collections.Generic;
using System.Linq;
using Acais.API.Data.Interfaces;
using Acais.API.Models;

namespace Acais.API.Data
{
    public class Seed
    {
        public static void SeedData(IDataContext context)
        {
            if (!context.Tamanhos.Any())
            {
                var tamanhos = new List<Tamanho>
                {
                    new Tamanho
                    {
                        Nome = "pequeno",
                        TempoPreparo = 5,
                        Valor = 10m
                    },
                    new Tamanho
                    {
                        Nome = "medio",
                        TempoPreparo = 7,
                        Valor = 13m
                    },
                    new Tamanho
                    {
                        Nome = "grande",
                        TempoPreparo = 10,
                        Valor = 15m
                    }
                };

                context.Tamanhos.AddRange(tamanhos);

                context.SaveChanges();
            }

            if (!context.Sabores.Any())
            {
                var sabores = new List<Sabor>
                {
                    new Sabor
                    {
                        Nome = "morango",
                        TempoPreparo = 0
                    },
                    new Sabor
                    {
                        Nome = "banana",
                        TempoPreparo = 0
                    },
                    new Sabor
                    {
                        Nome = "kiwi",
                        TempoPreparo = 5
                    }
                };

                context.Sabores.AddRange(sabores);

                context.SaveChanges();
            }
        }
    }
}