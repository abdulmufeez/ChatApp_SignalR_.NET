using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ChatApp.Models;

namespace ChatApp.Data {
    public class AppDbContext : IdentityDbContext<User> {           //Getting help from identityclass with user
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ChatApp.Models.Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}