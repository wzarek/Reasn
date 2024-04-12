using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class AddressService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        public AddressDto? CreateAddress(AddressDto addressDto) {
            if (addressDto is null)
                return null;

            var address = new Address {
                City = addressDto.City,
                Country = addressDto.Country,
                State = addressDto.State,
                Street = addressDto.Street,
                ZipCode = addressDto.ZipCode
            };

            _context.Addresses.Add(address);
            _context.SaveChanges();

            return addressDto;
        }

        public AddressDto? UpdateAddress(int addressId, AddressDto addressDto) {
            if (addressDto is null)
                return null;

            var address = _context.Addresses.FirstOrDefault(r => r.Id == addressId);

            if (address is null)
                return null;

            address.City = addressDto.City;
            address.Country = addressDto.Country;
            address.Street = addressDto.Street;
            address.State = addressDto.State;
            address.ZipCode = addressDto.ZipCode;

            _context.SaveChanges();

            return MapToAddressDto(address);
        }

        public void DeleteAddress(int addressId) {
            var address = _context.Addresses.FirstOrDefault(r => r.Id == addressId);

            if (address is not null) {
                _context.Addresses.Remove(address);
                _context.SaveChanges();
            }
        }

        public AddressDto? GetAddressById(int adrressId) {
            return MapToAddressDto(_context.Addresses.FirstOrDefault(r => r.Id == adrressId));
        }

        public IEnumerable<AddressDto?> GetAddressesByFilter(Expression<Func<Address, bool>> filter) {
            return _context.Addresses
                           .Where(filter)
                           .Select(address => MapToAddressDto(address))
                           .ToList(); 
        }

        public IEnumerable<AddressDto?> GetAllAddresses() {
            return _context.Addresses
                           .Select(address => MapToAddressDto(address))
                           .ToList();
        }

        private static AddressDto? MapToAddressDto(Address address) {
            if (address is null)
                return null;

            return new AddressDto {
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                State = address.State,
                ZipCode = address.ZipCode
            };
        }
    }
}
