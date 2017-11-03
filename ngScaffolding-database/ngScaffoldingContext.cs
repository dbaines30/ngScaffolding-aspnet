using ngScaffolding.database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ngScaffolding.models.Models;

namespace ngScaffolding.database
{
    public class ngScaffoldingContext : DbContext
    {
        public ngScaffoldingContext(DbContextOptions<ngScaffoldingContext> options) : base(options)
        {
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<ErrorModel> Errors { get; set; }

        public DbSet<ReferenceValue> ReferenceValues { get; set; }
        public DbSet<ReferenceValueItem> ReferenceValueItems { get; set; }

        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<UserPreferenceValue> UserPreferenceValues { get; set; }
    }

}