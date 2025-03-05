namespace ApartmentBooking.Application.Apartments.GetApartments
{
    public class ApartmentResponse
    {
        public  Guid id { get; init; }
        public string Name {  get; init; }
        public string Description { get; init; }
        public decimal Price {  get; init; }
        public string Currency { get; init; }
        public AddressResponse Address { get; set; }
    }
}
