using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Mappers;

public static class AddressMapper
{
    public static AddressDto ToDto(this Address address)
    {
        return new AddressDto
        {
            Street = address.Street,
            Country = address.Country,
            City = address.City,
            State = address.State,
            ZipCode = address.ZipCode
        };
    }

    public static List<AddressDto> ToDtoList(this IEnumerable<Address> addresses)
    {
        return addresses.Select(ToDto).ToList();
    }

    public static Address ToEntity(this AddressDto addressDto)
    {
        return new Address
        {
            City = addressDto.City,
            Country = addressDto.Country,
            State = addressDto.State,
            Street = addressDto.Street,
            ZipCode = addressDto.ZipCode
        };
    }
}