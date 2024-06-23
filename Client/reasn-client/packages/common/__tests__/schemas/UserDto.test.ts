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
    country: "Test country",
    city: "Test city",
    street: "Test street",
    state: "Test state",
    zipCode: "12345",
  };
  const interests: UserInterestDto[] = [
    { interest: { name: "Programming" }, level: 5 },
    { interest: { name: "Music" }, level: 3 },
  ];

  describe("fromJson", () => {
    it("should create an instance of UserDto from JSON string", () => {
      const json = `{
                "username": "${username}",
                "name": "${name}",
                "surname": "${surname}",
                "email": "${email}",
                "phone": "${phone}",
                "role": "${role}",
                "addressId": ${addressId},
                "address": ${JSON.stringify(address)},
                "intrests": ${JSON.stringify(interests)}
            }`;

      let user = UserDtoMapper.fromJSON(json);
      user = user as UserDto;

      expect(user.username).toBe(username);
      expect(user.name).toBe(name);
      expect(user.surname).toBe(surname);
      expect(user.email).toBe(email);
      expect(user.phone).toBe(phone);
      expect(user.role).toBe(role);
      expect(user.addressId).toBe(addressId);
      expect(user.address).toEqual(address);
      expect(user.intrests).toEqual(interests);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => UserDtoMapper.fromJSON("")).toThrow(ModelMappingError);
    });

    it("should throw an error when providing JSON without each property individually", () => {
      const jsonWithoutusername = `{
                "name": "${name}",
                "surname": "${surname}",
                "email": "${email}",
                "phone": "${phone}",
                "role": "${role}",
                "addressId": ${addressId},
                "address": ${JSON.stringify(address)},
                "intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutname = `{
                "username": "${username}",
                "surname": "${surname}",
                "email": "${email}",
                "phone": "${phone}",
                "role": "${role}",
                "addressId": ${addressId},
                "address": ${JSON.stringify(address)},
                "intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutsurname = `{
                "username": "${username}",
                "name": "${name}",
                "email": "${email}",
                "phone": "${phone}",
                "role": "${role}",
                "addressId": ${addressId},
                "address": ${JSON.stringify(address)},
                "intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutemail = `{
                "username": "${username}",
                "name": "${name}",
                "surname": "${surname}",
                "phone": "${phone}",
                "role": "${role}",
                "addressId": ${addressId},
                "address": ${JSON.stringify(address)},
                "intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutphone = `{
                "username": "${username}",
                "name": "${name}",
                "surname": "${surname}",
                "email": "${email}",
                "role": "${role}",
                "addressId": ${addressId},
                "intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutrole = `{
                "username": "${username}",
                "name": "${name}",
                "surname": "${surname}",
                "email": "${email}",
                "phone": "${phone}",
                "addressId": ${addressId},
                "address": ${JSON.stringify(address)},
                "intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutaddressId = `{
                "username": "${username}",
                "name": "${name}",
                "surname": "${surname}",
                "email": "${email}",
                "phone": "${phone}",
                "role": "${role}",
                "address": ${JSON.stringify(address)},
                "intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutaddress = `{
              "username": "${username}",
              "name": "${name}",
              "surname": "${surname}",
              "email": "${email}",
              "phone": "${phone}",
              "addressId": ${addressId},
              "role": "${role}",
              "intrests": ${JSON.stringify(interests)}
            }`;

      const jsonWithoutinterests = `{
                "username": "${username}",
                "name": "${name}",
                "surname": "${surname}",
                "email": "${email}",
                "phone": "${phone}",
                "role": "${role}",
                "addressId": ${addressId}
            }`;

      expect(() => UserDtoMapper.fromJSON(jsonWithoutusername)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutname)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutsurname)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutemail)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutphone)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutrole)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutaddressId)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutaddress)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromJSON(jsonWithoutinterests)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of UserDto from an object", () => {
      const object = {
        username: username,
        name: name,
        surname: surname,
        email: email,
        phone: phone,
        role: role,
        addressId: addressId,
        address: address,
        intrests: interests,
      };

      let user = UserDtoMapper.fromObject(object);
      user = user as UserDto;

      expect(user.username).toBe(username);
      expect(user.name).toBe(name);
      expect(user.surname).toBe(surname);
      expect(user.email).toBe(email);
      expect(user.phone).toBe(phone);
      expect(user.role).toBe(role);
      expect(user.addressId).toBe(addressId);
      expect(user.address).toEqual(address);
      expect(user.intrests).toEqual(interests);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        username: true,
        name: null,
        surname: "Doe",
        email: "john.doe@example.com",
        phone: "+1234567890",
        role: UserRole.USER,
        addressId: 2,
        intrests: interests,
      };

      const objectWithoutusername = {
        name: name,
        surname: surname,
        email: email,
        phone: phone,
        role: role,
        addressId: addressId,
        address: address,
        intrests: interests,
      };

      const objectWithoutname = {
        username: username,
        surname: surname,
        email: email,
        phone: phone,
        role: role,
        addressId: addressId,
        address: address,
        intrests: interests,
      };

      const objectWithoutsurname = {
        username: username,
        name: name,
        email: email,
        phone: phone,
        role: role,
        addressId: addressId,
        address: address,
        intrests: interests,
      };

      const objectWithoutemail = {
        username: username,
        name: name,
        surname: surname,
        phone: phone,
        role: role,
        addressId: addressId,
        address: address,
        intrests: interests,
      };

      const objectWithoutphone = {
        username: username,
        name: name,
        surname: surname,
        email: email,
        role: role,
        addressId: addressId,
        address: address,
        intrests: interests,
      };

      const objectWithoutrole = {
        username: username,
        name: name,
        surname: surname,
        email: email,
        phone: phone,
        addressId: addressId,
        address: address,
        intrests: interests,
      };

      const objectWithoutaddressId = {
        username: username,
        name: name,
        surname: surname,
        email: email,
        phone: phone,
        role: role,
        address: address,
        intrests: interests,
      };

      const objectWithoutaddress = {
        username: username,
        name: name,
        surname: surname,
        email: email,
        phone: phone,
        role: role,
        addressId: addressId,
        intrests: interests,
      };

      const objectWithoutinterests = {
        username: username,
        name: name,
        surname: surname,
        email: email,
        phone: phone,
        role: role,
        addressId: addressId,
        address: address,
      };

      expect(() => UserDtoMapper.fromObject(object)).toThrow(ModelMappingError);
      expect(() => UserDtoMapper.fromObject(objectWithoutusername)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutname)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutsurname)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutemail)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutphone)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutrole)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutaddressId)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutaddress)).toThrow(
        ModelMappingError,
      );
      expect(() => UserDtoMapper.fromObject(objectWithoutinterests)).toThrow(
        ModelMappingError,
      );
    });
  });
});
