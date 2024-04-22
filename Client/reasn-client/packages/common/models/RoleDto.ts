import ModelMappingError from '@reasn/common/errors/ModelMappingError'

export class RoleDto {
    Name: string

    constructor(name: string) {
        this.Name = name
    }

    static fromJson(json: string): RoleDto | null {
        if (!json) {
            return null
        }

        return RoleDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): RoleDto | null {
        if (!obj) {
            return null
        }

        if ('Name' in obj === false || typeof obj.Name !== 'string') throw new ModelMappingError('RoleDto','Name is required')

        return new RoleDto(obj.Name)
    }
}