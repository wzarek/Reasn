import {
  AddressDtoMapper,
  AddressDto,
} from "@reasn/common/src/schemas/AddressDto";
import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";

describe("AddressDto", () => {
  const country = "Test Country";
  const city = "Test City";
  const street = "Test Street";
  const state = "Test State";
  const zipCode = "12345";

  describe("fromJson", () => {
    it("should create an instance of AddressDto from JSON string", () => {
      const json = `{
                "country": "${country}",
                "city": "${city}",
                "street": "${street}",
                "state": "${state}",
                "zipCode": "${zipCode}"
            }`;

      let address = AddressDtoMapper.fromJSON(json);
      address = address as AddressDto;

      expect(address.country).toBe(country);
      expect(address.city).toBe(city);
      expect(address.street).toBe(street);
      expect(address.state).toBe(state);
      expect(address.zipCode).toBe(zipCode);
    });

    it("should throw error if the JSON string is empty", () => {
      expect(() => AddressDtoMapper.fromJSON("")).toThrow(ModelMappingError);
    });

    it("should throw an error when providing JSON without each property individually", () => {
      const jsonWithoutCountry = `{
                "city": "${city}",
                "street": "${street}",
                "state": "${state}",
                "zipCode": "${zipCode}"
            }`;

      const jsonWithoutCity = `{
                "country": "${country}",
                "street": "${street}",
                "state": "${state}",
                "zipCode": "${zipCode}"
            }`;

      const jsonWithoutStreet = `{
                "country": "${country}",
                "city": "${city}",
                "state": "${state}",
                "zipCode": "${zipCode}"
            }`;

      const jsonWithoutState = `{
                "country": "${country}",
                "city": "${city}",
                "street": "${street}",
                "zipCode": "${zipCode}"
            }`;

      const jsonWithoutZipCode = `{
                "country": "${country}",
                "city": "${city}",
                "street": "${street}",
                "state": "${state}"
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
        country: country,
        city: city,
        street: street,
        state: state,
        zipCode: zipCode,
      };

      let address = AddressDtoMapper.fromObject(object);
      address = address as AddressDto;

      expect(address.country).toBe(country);
      expect(address.city).toBe(city);
      expect(address.street).toBe(street);
      expect(address.state).toBe(state);
      expect(address.zipCode).toBe(zipCode);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        country: country,
        city: true,
        street: street,
        state: state,
        zipCode: null,
      };

      const objectWithoutCountry = {
        city: city,
        street: street,
        state: state,
        zipCode: zipCode,
      };

      const objectWithoutCity = {
        country: country,
        street: street,
        state: state,
        zipCode: zipCode,
      };

      const objectWithoutStreet = {
        country: country,
        city: city,
        state: state,
        zipCode: zipCode,
      };

      const objectWithoutState = {
        country: country,
        city: city,
        street: street,
        zipCode: zipCode,
      };

      const objectWithoutZipCode = {
        country: country,
        city: city,
        street: street,
        state: state,
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
