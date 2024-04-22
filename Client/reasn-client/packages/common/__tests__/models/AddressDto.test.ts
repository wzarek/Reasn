import { AddressDto } from '@reasn/common/models/AddressDto'
import ModelMappingError from '@reasn/common/errors/ModelMappingError'

describe('AddressDto', () => {
    const country = 'Test Country'
    const city = 'Test City'
    const street = 'Test Street'
    const state = 'Test State'
    const zipCode = '12345'

    describe('constructor', () => {
        it('should create an instance of AddressDto', () => {
            const address = new AddressDto(country, city, street, state, zipCode)

            expect(address).toBeInstanceOf(AddressDto)
            expect(address.Country).toBe(country)
            expect(address.City).toBe(city)
            expect(address.Street).toBe(street)
            expect(address.State).toBe(state)
            expect(address.ZipCode).toBe(zipCode)
        })
    })

    describe('fromJson', () => {
        it('should create an instance of AddressDto from JSON string', () => {
            const json = `{
                "Country": "${country}",
                "City": "${city}",
                "Street": "${street}",
                "State": "${state}",
                "ZipCode": "${zipCode}"
            }`

            let address = AddressDto.fromJson(json)

            expect(address).toBeInstanceOf(AddressDto)
            address = address as AddressDto

            expect(address.Country).toBe(country)
            expect(address.City).toBe(city)
            expect(address.Street).toBe(street)
            expect(address.State).toBe(state)
            expect(address.ZipCode).toBe(zipCode)
        })

        it('should return null if the JSON string is empty', () => {
            const json = ''

            const address = AddressDto.fromJson(json)

            expect(address).toBeNull()
        })

        it('should throw an error when providing JSON without each property individually', () => {
            const jsonWithoutCountry = `{
                "City": "${city}",
                "Street": "${street}",
                "State": "${state}",
                "ZipCode": "${zipCode}"
            }`

            const jsonWithoutCity = `{
                "Country": "${country}",
                "Street": "${street}",
                "State": "${state}",
                "ZipCode": "${zipCode}"
            }`

            const jsonWithoutStreet = `{
                "Country": "${country}",
                "City": "${city}",
                "State": "${state}",
                "ZipCode": "${zipCode}"
            }`

            const jsonWithoutState = `{
                "Country": "${country}",
                "City": "${city}",
                "Street": "${street}",
                "ZipCode": "${zipCode}"
            }`

            const jsonWithoutZipCode = `{
                "Country": "${country}",
                "City": "${city}",
                "Street": "${street}",
                "State": "${state}"
            }`

            expect(() => AddressDto.fromJson(jsonWithoutCountry)).toThrow(ModelMappingError)
            expect(() => AddressDto.fromJson(jsonWithoutCity)).toThrow(ModelMappingError)
            expect(() => AddressDto.fromJson(jsonWithoutStreet)).toThrow(ModelMappingError)
            expect(() => AddressDto.fromJson(jsonWithoutState)).toThrow(ModelMappingError)
            
            let addressWithoutZipcode = AddressDto.fromJson(jsonWithoutZipCode)
            expect(addressWithoutZipcode).toBeInstanceOf(AddressDto)
            addressWithoutZipcode = addressWithoutZipcode as AddressDto
            expect(addressWithoutZipcode.ZipCode).toBeNull()
        })
    })

    describe('fromObject', () => {
        it('should create an instance of AddressDto from an object', () => {
            const object = {
                Country: country,
                City: city,
                Street: street,
                State: state,
                ZipCode: zipCode
            }

            let address = AddressDto.fromObject(object)

            expect(address).toBeInstanceOf(AddressDto)
            address = address as AddressDto

            expect(address.Country).toBe(country)
            expect(address.City).toBe(city)
            expect(address.Street).toBe(street)
            expect(address.State).toBe(state)
            expect(address.ZipCode).toBe(zipCode)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                Country: country,
                City: true,
                Street: street,
                State: state,
                ZipCode: null
            }

            const objectWithoutCountry = {
                City: city,
                Street: street,
                State: state,
                ZipCode: zipCode
            }

            const objectWithoutCity = {
                Country: country,
                Street: street,
                State: state,
                ZipCode: zipCode
            }

            const objectWithoutStreet = {
                Country: country,
                City: city,
                State: state,
                ZipCode: zipCode
            }

            const objectWithoutState = {
                Country: country,
                City: city,
                Street: street,
                ZipCode: zipCode
            }

            const objectWithoutZipCode = {
                Country: country,
                City: city,
                Street: street,
                State: state
            }

            expect(() => AddressDto.fromObject(object)).toThrow(ModelMappingError)
            expect(() => AddressDto.fromObject(objectWithoutCountry)).toThrow(ModelMappingError)
            expect(() => AddressDto.fromObject(objectWithoutCity)).toThrow(ModelMappingError)
            expect(() => AddressDto.fromObject(objectWithoutStreet)).toThrow(ModelMappingError)
            expect(() => AddressDto.fromObject(objectWithoutState)).toThrow(ModelMappingError)

            let addressWithoutZipcode = AddressDto.fromObject(objectWithoutZipCode)
            expect(addressWithoutZipcode).toBeInstanceOf(AddressDto)
            addressWithoutZipcode = addressWithoutZipcode as AddressDto
            expect(addressWithoutZipcode.ZipCode).toBeNull()
        })
    })
})