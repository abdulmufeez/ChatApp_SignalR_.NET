using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ChatApp.Models;
using System.Threading.Tasks;
using System;

namespace ChatApp.Data {
    public class AppDbContext : IdentityDbContext<User> {           //Getting help from identityclass with user
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ChatApp.Models.Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChatUser>()
            .HasKey(entity => new {entity.ChatId, entity.UserId});
        }
    }
}