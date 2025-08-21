using System.IO.Pipelines;
using BlackJackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlackJackAPI.DbContext
{
    public class DBInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            BlackJackAPIDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<BlackJackAPIDbContext>();

            if (!context.Players.Any())
            {
                context.AddRange
                (
                    new Player
                    {
                        UserName = "BlockA",
                        Password = "Password",
                        Email = "blakec7689@gmail.com",
                        FirstName = "Blake",
                        LastName = "Clark",
                        Credits = 1000.00m,
                        InActive = null
                    }
                );
            }

            context.SaveChanges();
            context.Dispose();
            context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<BlackJackAPIDbContext>();
        }
    }
}
