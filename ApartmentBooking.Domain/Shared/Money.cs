namespace ApartmentBooking.Domain.Shared
{
    public record Money(decimal amount, Currency currency)
    {
        public static Money operator +(Money left, Money right)
        {
            if (left.currency != right.currency)
                throw new InvalidOperationException("Currency have to be equal");
            return new Money(left.amount + right.amount, left.currency);

        }

        public static Money Zero() => new Money(0, Currency.None);
        public static Money Zero(Currency currency) => new Money(0, currency);
        public  bool IsZero() => this==Zero(currency);
    }
}
