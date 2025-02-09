namespace ApartmentBooking.Domain.Shared
{
    public record Currency
    {
        public static readonly Currency None = new("");
        public static readonly Currency Usd = new("USD");
        public static readonly Currency Eur = new("EUR");

        // i make it private for i only can create instance in the Record(class) 
        private Currency(string code) => code = code;
        public string Code { get; init; }
        public static IReadOnlyCollection<Currency> All = new[] { Usd, Eur };

        public static Currency FromCode(string code)
        {
            return All.FirstOrDefault(c => c.Code == code) ??
                    throw new ApplicationException("The Currency Code is Invalid");
        }

    }
}
