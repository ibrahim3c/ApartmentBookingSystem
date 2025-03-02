using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentBooking.Infrastructure.Data.Configurations;

internal sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    //builder pattern :)


    /*
     * public Name Name { get; private set; }
        public Description Description { get; private set; }
        public Address Address { get; private set; }
        public Money Price { get; private set; }
        public Money CleaningFee { get; private set; }
        public DateTime? LastBookedOnUTC { get; internal set; }


        // represent the benefits this apartment offers
        public List<Amenty> Amenties { get; private set; } = new();
     */
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable("apartments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200).
            // from app to db              , from db to app
            HasConversion(name => name.name, value => new Name(value));
        builder.Property(x=>x.Description)
            .HasMaxLength (2000)
            .HasConversion(des=>des.description, value => new Description(value));

        builder.OwnsOne(x => x.Address);
        builder.OwnsOne(x => x.Price, priceBuilder =>
        {
            priceBuilder.Property(pb => pb.currency)
                      .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });
        builder.OwnsOne(x => x.CleaningFee, cleaningFeeBuilder =>
        {
            cleaningFeeBuilder.Property(pb => pb.currency)
                        .HasConversion(currency => currency.Code, code => Currency.FromCode(code));

        });
    }
}
