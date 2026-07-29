using OrderFlow.Domain.Common;
using OrderFlow.Domain.Exceptions;

namespace OrderFlow.Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Street { get; }
        public string Number { get; }
        public string Neighborhood { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }
        public string Country { get; }

        private Address()
        {
            Street = string.Empty;
            Number = string.Empty;
            Neighborhood = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty;
            Country = string.Empty;
        }

        public Address(string street, string number, string neighborhood, string city, string state, string zipCode, string country)
        {
            Validate(street, nameof(street));
            Validate(number, nameof(number));
            Validate(neighborhood, nameof(neighborhood));
            Validate(city, nameof(city));
            Validate(state, nameof(state));
            Validate(zipCode, nameof(zipCode));
            Validate(country, nameof(country));

            Street = street.Trim();
            Number = number.Trim();
            Neighborhood = neighborhood.Trim();
            City = city.Trim();
            State = state.Trim().ToUpperInvariant();
            ZipCode = zipCode.Trim();
            Country = country.Trim().ToUpperInvariant();
        }
        private static void Validate(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException( $"O campo {fieldName} é obrigatório.");
            }
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return Neighborhood;
            yield return City;
            yield return State;
            yield return ZipCode;
            yield return Country;
        }

        public override string ToString()
        {
            return $"{Street}, {Number} - {Neighborhood}, {City}/{State}, {ZipCode}, {Country}";
        }
    }
}
