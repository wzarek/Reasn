import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";
import { UserDto, UserDtoMapper } from "@reasn/common/src/schemas/UserDto";
import { UserInterestDto } from "@reasn/common/src/schemas/UserInterestDto";
import { UserRole } from "@reasn/common/src/enums/schemasEnums";
import { AddressDto } from "@reasn/common/src/schemas/AddressDto";

describe("UserDto", () => {
  const username = "john_doe";
  const name = "John";
  const surname = "Doe";
  const email = "john.doe@example.com";
  const phone = "+48 1234567890";
  const role = UserRole.USER;
  const addressId = 2;
  const address: AddressDto = {
    Country: "Test Country",
    City: "Test City",
    Street: "Test Street",
    State: "Test State",
    ZipCode: "12345",
  };
  const interests: UserInterestDto[] = [
    { Interest: { Name: "Programming" }, Level: 5 },
    { Interest: { Name: "Music" }, Level: 3 },
  ];

  describe("fromJson", () => {
    it("should create an instance of UserDto from JSON string", () => {
      const json = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "Role": "${role}",
                "AddressId": ${addressId},
                "Address": ${JSON.stringify(address)},
                "Intrests": ${JSON.stringify(interests)}
            }`;

      let user = UserDtoMapper.fromJSON(json);
      user = user as UserDto;

      expect(user.Username).toBe(username);
      expect(user.Name).toBe(name);
      expect(user.Surname).toBe(surname);
      expect(user.Email).toBe(email);
      expect(user.Phone).toBe(phone);
      expect(user.Role).toBe(role);
      expect(user.AddressId).toBe(addressId);
      expect(user.Address).toEqual(address);
      expect(user.Intrests).toEqual(interests);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => UserDtoMapper.fromJSON("")).toThrow(ModelMappingError);
    });

    it("should throw an error when providing JSON without each property individually", () => {
      const jsonWithoutUsername = `{
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "Role": "${role}",
                "AddressId": ${addressId},
                "Address": ${JSON.stringify(address)},
                "Intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutName = `{
                "Username": "${username}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "Role": "${role}",
                "AddressId": ${addressId},
                "Address": ${JSON.stringify(address)},
                "Intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutSurname = `{
                "Username": "${username}",
                "Name": "${name}",
                "Email": "${email}",
                "Phone": "${phone}",
                "Role": "${role}",
                "AddressId": ${addressId},
                "Address": ${JSON.stringify(address)},
                "Intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutEmail = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Phone": "${phone}",
                "Role": "${role}",
                "AddressId": ${addressId},
                "Address": ${JSON.stringify(address)},
                "Intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutPhone = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Role": "${role}",
                "AddressId": ${addressId},
                "Intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutRole = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "AddressId": ${addressId},
                "Address": ${JSON.stringify(address)},
                "Intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutAddressId = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "Role": "${role}",
                "Address": ${JSON.stringify(address)},
                "Intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutAddress = `{
              "Username": "${username}",
              "Name": "${name}",
              "Surname": "${surname}",
              "Email": "${email}",
              "Phone": "${phone}",
              "AddressId": ${addressId},
              "Role": "${role}",
              "Intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutInterests = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "Role": "${role}",
                "AddressId": ${addressId}
            }`;

      expect(() => UserDtoMapper.fromJSON(jsonWithoutUsername)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutName)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutSurname)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutEmail)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutPhone)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutRole)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutAddressId)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutAddress)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutInterests)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of UserDto from an object", () => {
      const object = {
        Username: username,
        Name: name,
        Surname: surname,
        Email: email,
        Phone: phone,
        Role: role,
        AddressId: addressId,
        Address: address,
        Intrests: interests,
      };

      let user = UserDtoMapper.fromObject(object);
      user = user as UserDto;

      expect(user.Username).toBe(username);
      expect(user.Name).toBe(name);
      expect(user.Surname).toBe(surname);
      expect(user.Email).toBe(email);
      expect(user.Phone).toBe(phone);
      expect(user.Role).toBe(role);
      expect(user.AddressId).toBe(addressId);
      expect(user.Address).toEqual(address);
      expect(user.Intrests).toEqual(interests);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        Username: true,
        Name: null,
        Surname: "Doe",
        Email: "john.doe@example.com",
        Phone: "+1234567890",
        Role: UserRole.USER,
        AddressId: 2,
        Intrests: interests,
      };

      const objectWithoutUsername = {
        Name: name,
        Surname: surname,
        Email: email,
        Phone: phone,
        Role: role,
        AddressId: addressId,
        Address: address,
        Intrests: interests,
      };

      const objectWithoutName = {
        Username: username,
        Surname: surname,
        Email: email,
        Phone: phone,
        Role: role,
        AddressId: addressId,
        Address: address,
        Intrests: interests,
      };

      const objectWithoutSurname = {
        Username: username,
        Name: name,
        Email: email,
        Phone: phone,
        Role: role,
        AddressId: addressId,
        Address: address,
        Intrests: interests,
      };

      const objectWithoutEmail = {
        Username: username,
        Name: name,
        Surname: surname,
        Phone: phone,
        Role: role,
        AddressId: addressId,
        Address: address,
        Intrests: interests,
      };

      const objectWithoutPhone = {
        Username: username,
        Name: name,
        Surname: surname,
        Email: email,
        Role: role,
        AddressId: addressId,
        Address: address,
        Intrests: interests,
      };

      const objectWithoutRole = {
        Username: username,
        Name: name,
        Surname: surname,
        Email: email,
        Phone: phone,
        AddressId: addressId,
        Address: address,
        Intrests: interests,
      };

      const objectWithoutAddressId = {
        Username: username,
        Name: name,
        Surname: surname,
        Email: email,
        Phone: phone,
        Role: role,
        Address: address,
        Intrests: interests,
      };

      const objectWithoutAddress = {
        Username: username,
        Name: name,
        Surname: surname,
        Email: email,
        Phone: phone,
        Role: role,
        AddressId: addressId,
        Intrests: interests,
      };

      const objectWithoutInterests = {
        Username: username,
        Name: name,
        Surname: surname,
        Email: email,
        Phone: phone,
        Role: role,
        AddressId: addressId,
        Address: address,
      };

      expect(() => UserDtoMapper.fromObject(object)).toThrow(ModelMappingError);
      expect(() => UserDtoMapper.fromObject(objectWithoutUsername)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutName)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutSurname)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutEmail)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutPhone)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutRole)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutAddressId)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutAddress)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutInterests)).toThrow(
        ModelMappingError,
      );
    });
  });
});
