using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services
{
    public class AddressService(ReasnContext context)
    {
        public AddressDto? CreateAddress(AddressDto? addressDto)
        {
            if (addressDto is null)
            {
                return null;
            }

            var address = new Address
            {
                City = addressDto.City,
                Country = addressDto.Country,
                State = addressDto.State,
                Street = addressDto.Street,
                ZipCode = addressDto.ZipCode
            };

            context.Addresses.Add(address);
            context.SaveChanges();

            return addressDto;
        }

        public AddressDto? UpdateAddress(int addressId, AddressDto? addressDto)
        {
            if (addressDto is null)
            {
                return null;
            }

            var address = context.Addresses.FirstOrDefault(r => r.Id == addressId);

            if (address is null)
            {
                return null;
            }

            address.City = addressDto.City;
            address.Country = addressDto.Country;
            address.Street = addressDto.Street;
            address.State = addressDto.State;
            address.ZipCode = addressDto.ZipCode;

            context.Addresses.Update(address);
            context.SaveChanges();

            return MapToAddressDto(address);
        }

        public bool DeleteAddress(int addressId)
        {
            var address = context.Addresses.FirstOrDefault(r => r.Id == addressId);

            if (address is null)
            {
                return false;
            }

            context.Addresses.Remove(address);
            context.SaveChanges();

            return true;
        }

        public AddressDto? GetAddressById(int addressId)
        {
            var address = context.Addresses.Find(addressId);

            if (address is null)
            {
                return null;
            }

            return MapToAddressDto(address);
        }

        public IEnumerable<AddressDto?> GetAddressesByFilter(Expression<Func<Address, bool>> filter)
        {
            return context.Addresses
                           .Where(filter)
                           .Select(address => MapToAddressDto(address))
                           .AsEnumerable();
        }

        public IEnumerable<AddressDto?> GetAllAddresses()
        {
            return context.Addresses
                           .Select(address => MapToAddressDto(address))
                           .AsEnumerable();
        }

        private static AddressDto MapToAddressDto(Address address)
        {
            return new AddressDto
            {
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                State = address.State,
                ZipCode = address.ZipCode
            };
        }
    }
}
