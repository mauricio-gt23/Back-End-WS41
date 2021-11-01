using System;
using Microsoft.EntityFrameworkCore;
using Roomies.API.Domain.Models;
using Roomies.API.Extensions;

namespace Roomies.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<FavouritePost> FavouritePosts { get; set; }
        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<Leaseholder> Leaseholders { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfilePaymentMethod> UserPaymentMethods { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Conversations Entity
            builder.Entity<Conversation>().ToTable("Conversations");

            //Constraints
            builder.Entity<Conversation>().HasKey(c => c.Id);
            builder.Entity<Conversation>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Conversation>().Property(c => c.DateCreation).IsRequired();//


            // Relationships 
            builder.Entity<Conversation>()
                .HasMany(p => p.Messages)
                .WithOne(p => p.Conversation)
                .HasForeignKey(p => p.ConversationId);

            //FavouritePost Entity Intermediate Table
            builder.Entity<FavouritePost>().ToTable("FavouritePosts");

            builder.Entity<FavouritePost>().HasKey
                (p => new { p.PostId, p.LeaseholderId });

            builder.Entity<FavouritePost>()
                 .HasOne(pt => pt.Post)
                 .WithMany(p => p.FavouritePosts)
                 .HasForeignKey(pt => pt.PostId);

            builder.Entity<FavouritePost>()
                .HasOne(pt => pt.Leaseholder)
                .WithMany(t => t.FavouritePosts)
                .HasForeignKey(pt => pt.LeaseholderId);


            //Profile Entity
            builder.Entity<Profile>().ToTable("Profiles");
                           
            builder.Entity<Profile>().HasKey(p => p.Id);
            builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Profile>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Profile>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Profile>().Property(p => p.CellPhone).IsRequired().HasMaxLength(9);
            builder.Entity<Profile>().Property(p => p.IdCard).IsRequired().HasMaxLength(8);
            builder.Entity<Profile>().Property(p => p.Description).IsRequired().HasMaxLength(240);
            builder.Entity<Profile>().Property(p => p.Birthday).IsRequired();
            builder.Entity<Profile>().Property(p => p.Department).IsRequired().HasMaxLength(25);
            builder.Entity<Profile>().Property(p => p.Province).IsRequired().HasMaxLength(25);
            builder.Entity<Profile>().Property(p => p.District).IsRequired().HasMaxLength(25);
            builder.Entity<Profile>().Property(p => p.Address).IsRequired().HasMaxLength(100);
            builder.Entity<Profile>().Property(p => p.StartSubscription).IsRequired();
            builder.Entity<Profile>().Property(p => p.EndSubsciption).IsRequired();

            // Relationships 
            builder.Entity<Profile>()
                .HasMany(p => p.Conversation1)
                .WithOne(p => p.User1)
                .HasForeignKey(p => p.User1Id);

            builder.Entity<Profile>()
               .HasMany(p => p.Conversation2)
               .WithOne(p => p.User2)
               .HasForeignKey(p => p.User2Id);


            //User Entity
            builder.Entity<User>().ToTable("Users");
                           
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.FirstName).IsRequired();
            builder.Entity<User>().Property(p => p.LastName).IsRequired();
            builder.Entity<User>().Property(p => p.Username).IsRequired();
            builder.Entity<User>().Property(p => p.PasswordHash).IsRequired();

            // Relationships 
            builder.Entity<Profile>()
                .HasOne(owp => owp.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(owp => owp.UserId);


            //Landlord Entity
            builder.Entity<Landlord>().ToTable("Landlords");


            // Relationships 


            builder.Entity<Landlord>()
                .HasMany(p => p.Posts)
                .WithOne(p => p.Landlord) ///
                .HasForeignKey(p => p.LandlordId);


            //leaseholder Entity
            builder.Entity<Leaseholder>().ToTable("Leaseholders");

           
            builder.Entity<Leaseholder>()
                .HasMany(p => p.Reviews)
                .WithOne(p => p.Leaseholder)
                .HasForeignKey(p => p.LeaseholderId);


            //Message Entity
            builder.Entity<Message>().ToTable("Messages");
            builder.Entity<Message>().HasKey(p => p.Id);
            builder.Entity<Message>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Message>().Property(p => p.Content).IsRequired().HasMaxLength(300);
            builder.Entity<Message>().Property(p => p.SentDate).IsRequired();
            builder.Entity<Message>().Property(p => p.Seen).IsRequired();
            //-----------------

            // PaymentMethod Entity
            builder.Entity<PaymentMethod>().ToTable("PaymentMethods");

            builder.Entity<PaymentMethod>().HasKey(e => e.Id);
            builder.Entity<PaymentMethod>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PaymentMethod>().Property(e => e.CVV).IsRequired().HasMaxLength(3);
            builder.Entity<PaymentMethod>().Property(e => e.ExpiryDate).IsRequired();
            //--------------------

            // Plan Entity
            builder.Entity<Plan>().ToTable("Plans");

            // Constraints

            builder.Entity<Plan>().HasKey(p => p.Id);
            builder.Entity<Plan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Plan>().Property(p => p.Price).IsRequired();
            builder.Entity<Plan>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Plan>().Property(p => p.Description).IsRequired().HasMaxLength(200);

            //builder.Entity<Plan>()
            //    .HasMany(p => p.Users)
            //    .WithOne(p => p.Plan)
            //    .HasForeignKey(p => p.PlanId);    
            
            // Posts Entity

            builder.Entity<Post>().ToTable("Posts");

            builder.Entity<Post>().HasKey(p => p.Id);
            builder.Entity<Post>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Post>().Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Entity<Post>().Property(p => p.Description).IsRequired().HasMaxLength(500);

            builder.Entity<Post>().Property(p => p.Address).IsRequired().HasMaxLength(50);
            builder.Entity<Post>().Property(p => p.Province).IsRequired().HasMaxLength(25);
            builder.Entity<Post>().Property(p => p.District).IsRequired().HasMaxLength(25);
            builder.Entity<Post>().Property(p => p.Department).IsRequired().HasMaxLength(25);
            builder.Entity<Post>().Property(p => p.Price).IsRequired();
            builder.Entity<Post>().Property(p => p.RoomQuantity).IsRequired();
            builder.Entity<Post>().Property(p => p.PostDate).IsRequired();

            builder.Entity<Post>()
               .HasMany(p => p.Reviews)
               .WithOne(p => p.Post)
               .HasForeignKey(p => p.PostId);
            //-------------------------------------

            // Review Entity

            builder.Entity<Review>().ToTable("Reviews");

            builder.Entity<Review>().HasKey(e => e.Id);
            builder.Entity<Review>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Review>().Property(e => e.Content).IsRequired().HasMaxLength(300);
            builder.Entity<Review>().Property(e => e.Date).IsRequired();

            //UserPaymentMethod Entity Intermediate Table
            builder.Entity<ProfilePaymentMethod>().ToTable("ProfilePaymentMethods");

            builder.Entity<ProfilePaymentMethod>().HasKey(p => new { p.ProfileId, p.PaymentMethodId });

            builder.Entity<ProfilePaymentMethod>()
                 .HasOne(pt => pt.Profile)
                 .WithMany(p => p.ProfilePaymentMethods)
                 .HasForeignKey(pt => pt.ProfileId);

            builder.Entity<ProfilePaymentMethod>()
                .HasOne(pt => pt.PaymentMethod)
                .WithMany(t => t.UserPaymentMethods)
                .HasForeignKey(pt => pt.PaymentMethodId);

            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
