using ReasnAPI.Exceptions;
using ReasnAPI.Mappers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services;

public class AddressService(ReasnContext context)
{
    public AddressDto CreateAddress(AddressDto addressDto)
    {
        ArgumentNullException.ThrowIfNull(addressDto);

        context.Addresses.Add(addressDto.ToEntity());
        context.SaveChanges();

        return addressDto;
    }

    public AddressDto UpdateAddress(int addressId, AddressDto addressDto)
    {
        ArgumentNullException.ThrowIfNull(addressDto);

        var address = context.Addresses.FirstOrDefault(r => r.Id == addressId);

        if (address is null)
        {
            throw new NotFoundException("Address not found");
        }

        address.City = addressDto.City;
        address.Country = addressDto.Country;
        address.Street = addressDto.Street;
        address.State = addressDto.State;
        address.ZipCode = addressDto.ZipCode;

        context.Addresses.Update(address);
        context.SaveChanges();

        return address.ToDto();
    }

    public void DeleteAddress(int addressId)
    {
        var address = context.Addresses.FirstOrDefault(r => r.Id == addressId);

        if (address is null)
        {
            throw new NotFoundException("Address not found");
        }

        context.Addresses.Remove(address);
        context.SaveChanges();
    }

    public AddressDto GetAddressById(int addressId)
    {
        var address = context.Addresses.Find(addressId);

        if (address is null)
        {
            throw new NotFoundException("Address not found");
        }

        return address.ToDto();
    }

    public IEnumerable<AddressDto> GetAddressesByFilter(Expression<Func<Address, bool>> filter)
    {
        return context.Addresses
                        .Where(filter)
                        .ToDtoList()
                        .AsEnumerable();
    }

    public IEnumerable<AddressDto> GetAllAddresses()
    {
        return context.Addresses
                        .ToDtoList()
                        .AsEnumerable();
    }
}