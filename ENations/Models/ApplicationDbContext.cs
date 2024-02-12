using Microsoft.EntityFrameworkCore;

namespace ENations.Models
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CongressMember> CongressMembers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryFunds> CountryFunds { get; set; }
        public DbSet<ItemOffers> ItemOffers { get; set; }
        public DbSet<LawProposal> LawProposals { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
        public DbSet<PartyMember> PartyMembers { get; set; }
        public DbSet<PoliticalParty> PoliticalParties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGyms> UserGyms { get; set; }
        public DbSet<UserItems> UserItems { get; set; }
        public DbSet<UserMoney> UserMoney { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost; Database=enations; Username=postgres; Password=admin");
            }
        }

    }

}
