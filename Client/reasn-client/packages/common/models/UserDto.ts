import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { InterestDto } from '@reasn/common/models/InterestDto'

export class UserDto {
    Username: string
    Name: string
    Surname: string
    Email: string
    Phone: string | null
    RoleId: number
    AddressId: number
    Intrests: InterestDto[] | null

    constructor(username: string, name: string, surname: string, email: string, phone: string | null, roleId: number, addressId: number, intrests: InterestDto[] | null) {
        this.Username = username
        this.Name = name
        this.Surname = surname
        this.Email = email
        this.Phone = phone
        this.RoleId = roleId
        this.AddressId = addressId
        this.Intrests = intrests
    }

    static fromJson(json: string): UserDto | null {
        if (!json) {
            return null
        }

        return UserDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): UserDto | null {
        if (!obj) {
            return null
        }

        if (!('Username' in obj) || typeof obj.Username !== 'string') throw new ModelMappingError('UserDto','Username is required')
        if (!('Name' in obj) || typeof obj.Name !== 'string') throw new ModelMappingError('UserDto','Name is required')
        if (!('Surname' in obj) || typeof obj.Surname !== 'string') throw new ModelMappingError('UserDto','Surname is required')
        if (!('Email' in obj) || typeof obj.Email !== 'string') throw new ModelMappingError('UserDto','Email is required')
        if (!('Phone' in obj) || obj.Phone === null || typeof obj.Phone !== 'string') throw new ModelMappingError('UserDto','Phone is required')
        if (!('RoleId' in obj) || typeof obj.RoleId !== 'number') throw new ModelMappingError('UserDto','RoleId is required')
        if (!('AddressId' in obj) || typeof obj.AddressId !== 'number') throw new ModelMappingError('UserDto','AddressId is required')
        if (!('Intrests' in obj) || obj.Intrests === null || !Array.isArray(obj.Intrests)) throw new ModelMappingError('UserDto','Intrests is required')

        let interests: InterestDto[] = obj.Intrests.map((x: object) => InterestDto.fromObject(x)).filter((x: InterestDto | null) => x !== null) as InterestDto[]

        return new UserDto(obj.Username, obj.Name, obj.Surname, obj.Email, obj.Phone, obj.RoleId, obj.AddressId, interests)
    }
}