using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Domain.Reviews;
using ApartmentBooking.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentBooking.Infrastructure.Data.Configurations
{
    internal sealed class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        /*
        public Guid UserId { get; private set; }
        public Guid ApartmentId { get; private set; }
        public Rating Rating { get; private set; }
        public Comment Comment {  get; private set; }
        public DateTime CreatedAt { get; private set; }
         */
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.HasKey(x => x.Id);
            builder.Property(r => r.Comment).IsRequired()
                .HasConversion(c => c.comment, c => new Comment(c));
           
            builder.Property(r => r.Rating).IsRequired()
                .HasConversion(c => c.Value, c =>  Rating.Create(c));

            builder.HasOne<Apartment>()
                .WithMany()
                .HasForeignKey(a => a.ApartmentId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId);
           
        }
    }
}
