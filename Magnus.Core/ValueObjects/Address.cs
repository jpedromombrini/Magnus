using System.Text.RegularExpressions;

namespace Magnus.Core.ValueObjects;

public class Address
{
    public Address(string zipCode, string publicPlace, int number, string neighborhood, string city, string state,
        string complement)
    {
        SetZipCode(zipCode);
        SetPublicPlace(publicPlace);
        SetNumber(number);
        SetNeighborhood(neighborhood);
        SetCity(city);
        SetState(state);
        SetComplement(complement);
    }

    public string ZipCode { get; private set; }
    public string PublicPlace { get; private set; }
    public int Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Complement { get; private set; }

    private void SetZipCode(string zipCode)
    {
        if (string.IsNullOrWhiteSpace(zipCode))
            throw new ArgumentException("O CEP não pode ser nulo ou vazio.");

        if (!Regex.IsMatch(zipCode, @"^\d{5}-\d{3}$"))
            throw new ArgumentException("O CEP deve estar no formato 'XXXXX-XXX'.");

        ZipCode = zipCode;
    }

    private void SetPublicPlace(string publicPlace)
    {
        if (string.IsNullOrWhiteSpace(publicPlace))
            throw new ArgumentException("O logradouro não pode ser nulo ou vazio.");

        PublicPlace = publicPlace;
    }

    private void SetNumber(int number)
    {
        Number = number;
    }

    private void SetNeighborhood(string neighborhood)
    {
        if (string.IsNullOrWhiteSpace(neighborhood))
            throw new ArgumentException("O bairro não pode ser nulo ou vazio.");

        Neighborhood = neighborhood;
    }

    private void SetCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("A cidade não pode ser nula ou vazia.");

        City = city;
    }

    private void SetState(string state)
    {
        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("O estado não pode ser nulo ou vazio.");

        State = state;
    }

    private void SetComplement(string complement)
    {
        Complement = complement ?? string.Empty;
    }

    public override string ToString()
    {
        return $"{PublicPlace}, {Number}, {Neighborhood}, {City} - {State}, {ZipCode}. Complemento: {Complement}.";
    }
}