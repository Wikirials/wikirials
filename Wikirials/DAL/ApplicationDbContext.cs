using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wikirials.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Wikirials.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<File> Files { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}