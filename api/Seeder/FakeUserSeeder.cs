using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace api.Seeder
{
    public static class FakeUserSeeder
    {
        public static async Task GenerateFakeUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            var fakeUsers = new List<(string PartnerNo, string UserName, string Password)>
            {
                ("FG-00001", "FAKEGOOGLE", "FAKEPASSWORD1234"),
                ("FG-00002", "FAKEPEOPLE", "FAKEPASSWORD4578")
            };

            foreach (var (partnerNo, userName, password) in fakeUsers)
            {
                var existingUser = await userManager.FindByNameAsync(userName);
                if (existingUser == null)
                {
                    var user = new User
                    {
                        UserName = userName, //this is partnerkey FAKEGOOGLE lets say
                        PartnerNo = partnerNo,
                        Email = $"{partnerNo.ToLower()}@example.com",
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(user, password);
                }
            }
        }
    }
}
