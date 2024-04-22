import ModelMappingError from '@reasn/common/errors/ModelMappingError'

export class AddressDto {
    Country: string
    City: string
    Street: string
    State: string
    ZipCode: string | null

    constructor(country: string, city: string, street: string, state: string, zipCode: string | null) {
        this.Country = country
        this.City = city
        this.Street = street
        this.State = state
        this.ZipCode = zipCode
    }

    static fromJson(json: string): AddressDto | null {
        if (!json) {
            return null
        }

        return AddressDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): AddressDto | null {
        if (!obj) {
            return null
        }

        if ('Country' in obj === false || typeof obj.Country !== 'string') throw new ModelMappingError('AddressDto','Country is required')
        if ('City' in obj === false || typeof obj.City !== 'string') throw new ModelMappingError('AddressDto','City is required')
        if ('State' in obj === false || typeof obj.State !== 'string') throw new ModelMappingError('AddressDto','State is required')
        if ('Street' in obj === false || typeof obj.Street !== 'string') throw new ModelMappingError('AddressDto','Street is required')
        if ('ZipCode' in obj === true && (obj.ZipCode !== null && typeof obj.ZipCode !== 'string')) throw new ModelMappingError('AddressDto','ZipCode is invalid')

        let zipCode: string | null = 'ZipCode' in obj === true ? obj.ZipCode as string : null

        return new AddressDto(obj.Country, obj.City, obj.Street, obj.State, zipCode)
    }
}