using CRUD_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Test.Data
{
    public class Initializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Login = "Petro22",
                        Password = "qwerty",
                        FirstName = "Petro",
                        LastName = "Krehel",
                        DateOfBirth = new DateTime(2001, 7, 12),
                        Gender = "M"
                    },
                    new User
                    {
                        Login = "MaxYmmm",
                        Password = "123456",
                        FirstName = "Maxym",
                        LastName = "Ivanov",
                        DateOfBirth = new DateTime(2002, 11, 9),
                        Gender = "M"
                    },
                    new User
                    {
                        Login = "Irynka",
                        Password = "555555",
                        FirstName = "Iryna",
                        LastName = "Sydorenko",
                        DateOfBirth = new DateTime(2000, 2, 21),
                        Gender = "F"
                    }
                );
                context.SaveChanges();
            }
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order
                    {
                        User = context.Users.ToList()[0],
                        OrderDate = DateTime.Now,
                        OrderCost = (decimal?)344.5,
                        ItemsDescription = "Gaming keyboard",
                        ShippingAddress = "Lviv, Doroshenka 12, 9"
                    },
                    new Order
                    {
                        User = context.Users.ToList()[1],
                        OrderDate = DateTime.Now,
                        OrderCost = (decimal?)8000.0,
                        ItemsDescription = "Tools kit",
                        ShippingAddress = "Lviv, Gorodotska 140, 33"
                    },
                    new Order
                    {
                        User = context.Users.ToList()[2],
                        OrderDate = DateTime.Now,
                        OrderCost = (decimal?)20135.35,
                        ItemsDescription = "PC",
                        ShippingAddress = "Lviv, Vyhovskoho 29, 14"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
