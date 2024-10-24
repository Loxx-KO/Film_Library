using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configurations
{
    public class DirectorConfiguration : EntityTypeConfiguration<Director>
    {
        public DirectorConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(50);
            Property(x => x.Surname).HasMaxLength(50);
            HasMany(x => x.Movies).WithRequired(x => x.Director).HasForeignKey(x => x.DirectorId);
        
            Property(x => x.Name).IsRequired();
            Property(x => x.Surname).IsRequired();
        }
    }
}
