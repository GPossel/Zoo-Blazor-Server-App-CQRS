using Infrastructure;
using Infrastructure.Entities.Followers;
using Infrastructure.Entities.Users;
using Infrastructure.Users;
using Microsoft.EntityFrameworkCore;

namespace Zoo_Blazor_Server_App_CQRS.SeedData
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyContext(serviceProvider.GetRequiredService<DbContextOptions>()))
            {
                var emailHarry = EmailDbo.Create("harry@hotmail.com").Value;
                var nameHarry = NameDbo.Create("Harry", "Potter");
                UserDbo harry = UserDbo.Create(emailHarry, nameHarry, true);

                var emailHermelien = EmailDbo.Create("hermelien@hotmail.com").Value;
                var nameHermelien = NameDbo.Create("Hermelien", "Granger");
                UserDbo hermelien = UserDbo.Create(emailHermelien, nameHermelien, true);

                if (!context.Users.Any())
                {
                    context.Users.AddRange(harry, hermelien);
                }

                FollowerDbo harryFollowesHermelien = FollowerDbo.Create(harry, hermelien, DateTime.Now);

                if (!context.Followers.Any())
                {
                    context.Followers.AddRange(harryFollowesHermelien);
                }

                context.SaveChanges();
            }
        
        }
    }
}
