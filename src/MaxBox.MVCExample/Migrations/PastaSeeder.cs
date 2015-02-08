using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using MaxBox.MVCExample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MaxBox.MVCExample.Migrations
{
    public static class PastaSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            //Generic lambdas voor rolecreate en usercreatre
            Action<string> createRole = roleName =>
            {
                if (!context.Roles.Any(x => x.Name == roleName))
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    roleManager.Create(new IdentityRole(roleName));
                }
            };
            Action<string, string, string[]> createUserWithRoles = (userName, password, userRoles) =>
            {
                if (!context.Users.Any(x => x.UserName == userName))
                {
                    var userManager = new UserManager<User>(new UserStore<User>(context));
                    var user = new User {UserName = userName};
                    userManager.Create(user, password);
                    userRoles.ToList().ForEach(x => userManager.AddToRole(user.Id, x));
                }
            };
            //roles gedeelte
            List<string> roles = new[] {"Admin", "Klant"}.ToList();
            roles.ForEach(createRole);

            //user gedeelte
            createUserWithRoles("Maxim", "password123", new[] {"Admin"});
            createUserWithRoles("Admin", "password123", new[] {"Admin"});
            createUserWithRoles("TestKlant1", "password123", new[] {"Klant"});
            createUserWithRoles("TestKlant2", "password123", new[] {"Klant"});
            createUserWithRoles("TestKlant3", "password123", new[] {"Klant"});

            //Categorieen
            context.ProductsCategories.AddOrUpdate(x => x.Naam, new[]
            {
                new ProductCategory {Id = 1, Naam = "Dranken", Gewicht = 0},
                new ProductCategory {Id = 2, Naam = "Voorgerechten", Gewicht = 10},
                new ProductCategory {Id = 3, Naam = "Pasta", Gewicht = 15},
                new ProductCategory {Id = 4, Naam = "Pasta al forno", Gewicht = 20},
                new ProductCategory {Id = 5, Naam = "Salades", Gewicht = 25},
                new ProductCategory {Id = 6, Naam = "KinderMenu's", Gewicht = 30},
                new ProductCategory {Id = 7, Naam = "Desserten", Gewicht = 35}
            });
            context.SaveChanges();
            List<ProductCategory> productcategorien = context.ProductsCategories.ToList();

            //Products
            context.Products.AddOrUpdate(x => x.Naam, new[]
            {
                //Dranken
                new Product
                {
                    Afkorting = "COC",
                    Naam = "Coca-Cola",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id,
                    IsBeschikbaar = false,
                    Status = Status.Discount
                },
                new Product
                {
                    Afkorting = "COL",
                    Naam = "Coca-Cola light",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id,
                    Status = Status.Discount
                },
                new Product
                {
                    Afkorting = "COZ",
                    Naam = "Coca-Cola zero",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id,
                    IsBeschikbaar = false,
                    Status = Status.Discount
                },
                new Product
                {
                    Afkorting = "FAN",
                    Naam = "Fanta",
                    Prijs = 1.6,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },
                new Product
                {
                    Afkorting = "SPR",
                    Naam = "Sprite",
                    Prijs = 1.6,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },
                new Product
                {
                    Afkorting = "ITE",
                    Naam = "Ice Tea",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },
                new Product
                {
                    Afkorting = "SAG",
                    Naam = "Schweppes Agrum",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },
                new Product
                {
                    Afkorting = "MMA",
                    Naam = "Minute Maid",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },
                new Product
                {
                    Afkorting = "PWA",
                    Naam = "Plat Water",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },
                new Product
                {
                    Afkorting = "BWA",
                    Naam = "Bruiswater",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id,
                    Status = Status.SuperDiscount
                },
                new Product
                {
                    Afkorting = "FRI",
                    Naam = "Fristi",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },
                new Product
                {
                    Afkorting = "CEC",
                    Naam = "Cecemel",
                    Prijs = 1.6,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },
                new Product
                {
                    Afkorting = "FFA",
                    Naam = "Fles Fanta",
                    Prijs = 3.5,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },
                new Product
                {
                    Afkorting = "FCC",
                    Naam = "Fles Coca-Cola",
                    Prijs = 3.5,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "Dranken").Id
                },

                //kindermenu's

                new Product
                {
                    Afkorting = "KPO",
                    Naam = "Pollo met verrassingspasta",
                    Prijs = 5.99,
                    CategorieId = productcategorien.First(x => x.Naam == "KinderMenu's").Id,
                    Status = Status.Discount
                },
                new Product
                {
                    Afkorting = "KSP",
                    Naam = "Spagghetti bolognese",
                    Prijs = 5.99,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "KinderMenu's").Id
                },

                //Voorgerechten

                new Product
                {
                    Afkorting = "LON",
                    Naam = "Lookbrood natuur 3 stuks",
                    Prijs = 2.70,
                    CategorieId = productcategorien.First(x => x.Naam == "Voorgerechten").Id,
                    IsBeschikbaar = false,
                    Status = Status.SuperDiscount
                },
                new Product
                {
                    Afkorting = "LOK",
                    Naam = "Lookbrood met kaas 3 stuks",
                    Prijs = 3.30,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "Voorgerechten").Id
                },
                new Product
                {
                    Afkorting = "BRU",
                    Naam = "Bruschette 2 stuks",
                    Prijs = 4.90,
                    CategorieId = productcategorien.First(x => x.Naam == "Voorgerechten").Id
                },
                new Product
                {
                    Afkorting = "CAR",
                    Naam = "Carpaccio di salmone",
                    Prijs = 5.35,
                    CategorieId = productcategorien.First(x => x.Naam == "Voorgerechten").Id
                },
                new Product
                {
                    Afkorting = "SCA",
                    Naam = "Scampi all' alglio",
                    Prijs = 5.35,
                    CategorieId = productcategorien.First(x => x.Naam == "Voorgerechten").Id
                }
                ,
                new Product
                {
                    Afkorting = "ZUP",
                    Naam = "Zuppa di pomodoro",
                    Prijs = 3.50,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "Voorgerechten").Id
                },
                new Product
                {
                    Afkorting = "TIR",
                    Naam = "Tiramisu",
                    Prijs = 3.50,
                    CategorieId = productcategorien.First(x => x.Naam == "Desserten").Id
                },
                new Product
                {
                    Afkorting = "PCO",
                    Naam = "Panna Cotta",
                    Prijs = 3.50,
                    CategorieId = productcategorien.First(x => x.Naam == "Desserten").Id
                },
                new Product
                {
                    Afkorting = "MAC",
                    Naam = "Mousse Al Chocolate",
                    Prijs = 3.50,
                    CategorieId = productcategorien.First(x => x.Naam == "Desserten").Id
                },
                new Product
                {
                    Afkorting = "CAP",
                    Naam = "Insalate caprese",
                    Prijs = 4.25,
                    CategorieId = productcategorien.First(x => x.Naam == "Salades").Id
                }
                ,
                new Product
                {
                    Afkorting = "SMS",
                    Naam = "Insalate mista mixed salada",
                    Prijs = 4.25,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "Salades").Id
                }
                ,
                new Product
                {
                    Afkorting = "KIP",
                    Naam = "Insalate al pollo 'kipsalade'",
                    Prijs = 4.90,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "Salades").Id
                }
                ,
                new Product
                {
                    Afkorting = "CHE",
                    Naam = "Insalate dello chef 'salade van de chef'",
                    Prijs = 4.90,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "Salades").Id
                }
                ,
                new Product
                {
                    Afkorting = "CAC",
                    Naam = "Cannelloni Carne",
                    Prijs = 10.25,
                    CategorieId = productcategorien.First(x => x.Naam == "Pasta al forno").Id
                }
                ,
                new Product
                {
                    Afkorting = "CAR",
                    Naam = "Cannelloni Ricotta Spinaci",
                    Prijs = 10.25,
                    CategorieId = productcategorien.First(x => x.Naam == "Pasta al forno").Id
                }
                ,
                new Product
                {
                    Afkorting = "LAS",
                    Naam = "Lasagne Bolognese",
                    Prijs = 11.60,
                    IsBeschikbaar = false,
                    CategorieId = productcategorien.First(x => x.Naam == "Pasta al forno").Id
                }
            });
            context.SaveChanges();
        }
    }
}