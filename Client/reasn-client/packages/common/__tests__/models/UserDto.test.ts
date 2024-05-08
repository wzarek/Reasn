import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { UserDto, UserDtoMapper } from '@reasn/common/models/UserDto'
import { InterestDto } from '@reasn/common/models/InterestDto'

describe('UserDto', () => {
    const username = 'john_doe'
    const name = 'John'
    const surname = 'Doe'
    const email = 'john.doe@example.com'
    const phone = '+1234567890'
    const roleId = 1
    const addressId = 2
    const interests: InterestDto[] = [
        { Name: 'Programming', Level: 5 },
        { Name: 'Music', Level: 3 }
    ]

    describe('fromJson', () => {
        it('should create an instance of UserDto from JSON string', () => {
            const json = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "RoleId": ${roleId},
                "AddressId": ${addressId},
                "Intrests": ${JSON.stringify(interests)}
            }`

            let user = UserDtoMapper.fromJSON(json)
            user = user as UserDto

            expect(user.Username).toBe(username)
            expect(user.Name).toBe(name)
            expect(user.Surname).toBe(surname)
            expect(user.Email).toBe(email)
            expect(user.Phone).toBe(phone)
            expect(user.RoleId).toBe(roleId)
            expect(user.AddressId).toBe(addressId)
            expect(user.Intrests).toEqual(interests)
        })

        it('should return null if the JSON string is empty', () => {
            expect(() => UserDtoMapper.fromJSON('')).toThrow(ModelMappingError)
        })

        it('should throw an error when providing JSON without each property individually', () => {
            const jsonWithoutUsername = `{
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "RoleId": ${roleId},
                "AddressId": ${addressId},
                "Intrests": ${JSON.stringify(interests)}
            }`

            const jsonWithoutName = `{
                "Username": "${username}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "RoleId": ${roleId},
                "AddressId": ${addressId},
                "Intrests": ${JSON.stringify(interests)}
            }`

            const jsonWithoutSurname = `{
                "Username": "${username}",
                "Name": "${name}",
                "Email": "${email}",
                "Phone": "${phone}",
                "RoleId": ${roleId},
                "AddressId": ${addressId},
                "Intrests": ${JSON.stringify(interests)}
            }`

            const jsonWithoutEmail = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Phone": "${phone}",
                "RoleId": ${roleId},
                "AddressId": ${addressId},
                "Intrests": ${JSON.stringify(interests)}
            }`

            const jsonWithoutPhone = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "RoleId": ${roleId},
                "AddressId": ${addressId},
                "Intrests": ${JSON.stringify(interests)}
            }`

            const jsonWithoutRoleId = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "AddressId": ${addressId},
                "Intrests": ${JSON.stringify(interests)}
            }`

            const jsonWithoutAddressId = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "RoleId": ${roleId},
                "Intrests": ${JSON.stringify(interests)}
            }`

            const jsonWithoutInterests = `{
                "Username": "${username}",
                "Name": "${name}",
                "Surname": "${surname}",
                "Email": "${email}",
                "Phone": "${phone}",
                "RoleId": ${roleId},
                "AddressId": ${addressId}
            }`

            expect(() => UserDtoMapper.fromJSON(jsonWithoutUsername)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromJSON(jsonWithoutName)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromJSON(jsonWithoutSurname)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromJSON(jsonWithoutEmail)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromJSON(jsonWithoutPhone)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromJSON(jsonWithoutRoleId)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromJSON(jsonWithoutAddressId)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromJSON(jsonWithoutInterests)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of UserDto from an object', () => {
            const object = {
                Username: username,
                Name: name,
                Surname: surname,
                Email: email,
                Phone: phone,
                RoleId: roleId,
                AddressId: addressId,
                Intrests: interests
            }

            let user = UserDtoMapper.fromObject(object)
            user = user as UserDto

            expect(user.Username).toBe(username)
            expect(user.Name).toBe(name)
            expect(user.Surname).toBe(surname)
            expect(user.Email).toBe(email)
            expect(user.Phone).toBe(phone)
            expect(user.RoleId).toBe(roleId)
            expect(user.AddressId).toBe(addressId)
            expect(user.Intrests).toEqual(interests)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                Username: true,
                Name: null,
                Surname: 'Doe',
                Email: 'john.doe@example.com',
                Phone: '+1234567890',
                RoleId: 1,
                AddressId: 2,
                Intrests: interests
            }

            const objectWithoutUsername = {
                Name: name,
                Surname: surname,
                Email: email,
                Phone: phone,
                RoleId: roleId,
                AddressId: addressId,
                Intrests: interests
            }

            const objectWithoutName = {
                Username: username,
                Surname: surname,
                Email: email,
                Phone: phone,
                RoleId: roleId,
                AddressId: addressId,
                Intrests: interests
            }

            const objectWithoutSurname = {
                Username: username,
                Name: name,
                Email: email,
                Phone: phone,
                RoleId: roleId,
                AddressId: addressId,
                Intrests: interests
            }

            const objectWithoutEmail = {
                Username: username,
                Name: name,
                Surname: surname,
                Phone: phone,
                RoleId: roleId,
                AddressId: addressId,
                Intrests: interests
            }

            const objectWithoutPhone = {
                Username: username,
                Name: name,
                Surname: surname,
                Email: email,
                RoleId: roleId,
                AddressId: addressId,
                Intrests: interests
            }

            const objectWithoutRoleId = {
                Username: username,
                Name: name,
                Surname: surname,
                Email: email,
                Phone: phone,
                AddressId: addressId,
                Intrests: interests
            }

            const objectWithoutAddressId = {
                Username: username,
                Name: name,
                Surname: surname,
                Email: email,
                Phone: phone,
                RoleId: roleId,
                Intrests: interests
            }

            const objectWithoutInterests = {
                Username: username,
                Name: name,
                Surname: surname,
                Email: email,
                Phone: phone,
                RoleId: roleId,
                AddressId: addressId,
            }

            expect(() => UserDtoMapper.fromObject(object)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromObject(objectWithoutUsername)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromObject(objectWithoutName)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromObject(objectWithoutSurname)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromObject(objectWithoutEmail)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromObject(objectWithoutPhone)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromObject(objectWithoutRoleId)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromObject(objectWithoutAddressId)).toThrow(ModelMappingError)
            expect(() => UserDtoMapper.fromObject(objectWithoutInterests)).toThrow(ModelMappingError)
        })
    })
})