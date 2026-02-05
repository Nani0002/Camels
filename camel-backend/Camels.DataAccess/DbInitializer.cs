using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camels.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Camels.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(CamelsDbContext context, string dbPath, bool seed)
        {
            var directory = Path.GetDirectoryName(dbPath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            context.Database.Migrate();

            if (context.Camels.Any() || !seed)
                return;

            Camel[] camels = [
                new Camel
                {
                    Name = "Sahara",
                    Color = "Brown",
                    HumpCount = 2,
                    LastFed = DateTime.Now.AddSeconds(-1)
                },
                new Camel
                {
                    Name = "Camelot",
                    Color = "Yellow",
                    HumpCount = 1,
                    LastFed = DateTime.Now.AddHours(-2)
                }
                ];

            context.Camels.AddRange(camels);

            context.SaveChanges();
        }
    }
}
