using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace Card_Creator.Data
{
    public class CardCreatorContext : DbContext
    {
        public DbSet<Card> Card{ get; set;}
        public DbSet<Type> Type{ get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server= (localdb)\MSSQLLocalDB; " + 
                @"Database = Card Creator; " + 
                @"Trusted_Connection =True; ");
        }

         
        
    }
}
