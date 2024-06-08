import { AddressDtoMapper, AddressDto } from "@reasn/common/models/AddressDto";
import ModelMappingError from "@reasn/common/errors/ModelMappingError";

describe("AddressDto", () => {
  const country = "Test Country";
  const city = "Test City";
  const street = "Test Street";
  const state = "Test State";
  const zipCode = "12345";

  describe("fromJson", () => {
    it("should create an instance of AddressDto from JSON string", () => {
      const json = `{
                "Country": "${country}",
                "City": "${city}",
                "Street": "${street}",
                "State": "${state}",
                "ZipCode": "${zipCode}"
            }`;

      let address = AddressDtoMapper.fromJSON(json);
      address = address as AddressDto;

      expect(address.Country).toBe(country);
      expect(address.City).toBe(city);
      expect(address.Street).toBe(street);
      expect(address.State).toBe(state);
      expect(address.ZipCode).toBe(zipCode);
    });

    it("should throw error if the JSON string is empty", () => {
      expect(() => AddressDtoMapper.fromJSON("")).toThrow(ModelMappingError);
    });

    it("should throw an error when providing JSON without each property individually", () => {
      const jsonWithoutCountry = `{
                "City": "${city}",
                "Street": "${street}",
                "State": "${state}",
                "ZipCode": "${zipCode}"
            }`;

      const jsonWithoutCity = `{
                "Country": "${country}",
                "Street": "${street}",
                "State": "${state}",
                "ZipCode": "${zipCode}"
            }`;

      const jsonWithoutStreet = `{
                "Country": "${country}",
                "City": "${city}",
                "State": "${state}",
                "ZipCode": "${zipCode}"
            }`;

      const jsonWithoutState = `{
                "Country": "${country}",
                "City": "${city}",
                "Street": "${street}",
                "ZipCode": "${zipCode}"
            }`;

      const jsonWithoutZipCode = `{
                "Country": "${country}",
                "City": "${city}",
                "Street": "${street}",
                "State": "${state}"
            }`;

      expect(() => AddressDtoMapper.fromJSON(jsonWithoutCountry)).toThrow(
        ModelMappingError,
      );
      expect(() => AddressDtoMapper.fromJSON(jsonWithoutCity)).toThrow(
        ModelMappingError,
      );
      expect(() => AddressDtoMapper.fromJSON(jsonWithoutStreet)).toThrow(
        ModelMappingError,
      );
      expect(() => AddressDtoMapper.fromJSON(jsonWithoutState)).toThrow(
        ModelMappingError,
      );
      expect(() => AddressDtoMapper.fromJSON(jsonWithoutZipCode)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of AddressDto from an object", () => {
      const object = {
        Country: country,
        City: city,
        Street: street,
        State: state,
        ZipCode: zipCode,
      };

      let address = AddressDtoMapper.fromObject(object);
      address = address as AddressDto;

      expect(address.Country).toBe(country);
      expect(address.City).toBe(city);
      expect(address.Street).toBe(street);
      expect(address.State).toBe(state);
      expect(address.ZipCode).toBe(zipCode);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        Country: country,
        City: true,
        Street: street,
        State: state,
        ZipCode: null,
      };

      const objectWithoutCountry = {
        City: city,
        Street: street,
        State: state,
        ZipCode: zipCode,
      };

      const objectWithoutCity = {
        Country: country,
        Street: street,
        State: state,
        ZipCode: zipCode,
      };

      const objectWithoutStreet = {
        Country: country,
        City: city,
        State: state,
        ZipCode: zipCode,
      };

      const objectWithoutState = {
        Country: country,
        City: city,
        Street: street,
        ZipCode: zipCode,
      };

      const objectWithoutZipCode = {
        Country: country,
        City: city,
        Street: street,
        State: state,
      };

      expect(() => AddressDtoMapper.fromObject(object)).toThrow(
        ModelMappingError,
      );
      expect(() => AddressDtoMapper.fromObject(objectWithoutCountry)).toThrow(
        ModelMappingError,
      );
      expect(() => AddressDtoMapper.fromObject(objectWithoutCity)).toThrow(
        ModelMappingError,
      );
      expect(() => AddressDtoMapper.fromObject(objectWithoutStreet)).toThrow(
        ModelMappingError,
      );
      expect(() => AddressDtoMapper.fromObject(objectWithoutState)).toThrow(
        ModelMappingError,
      );
      expect(() => AddressDtoMapper.fromObject(objectWithoutZipCode)).toThrow(
        ModelMappingError,
      );
    });
  });
});
