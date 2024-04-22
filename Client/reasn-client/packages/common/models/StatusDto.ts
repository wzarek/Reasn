import ModelMappingError from '@reasn/common/errors/ModelMappingError'

export class StatusDto {
    Name: string
    ObjectTypeId: number
    
    constructor(name: string, objectTypeId: number) {
        this.Name = name
        this.ObjectTypeId = objectTypeId
    }

    static fromJson(json: string): StatusDto | null {
        if (!json) {
            return null
        }

        return StatusDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): StatusDto | null {
        if (!obj) {
            return null
        }

        if ('Name' in obj === false || typeof obj.Name !== 'string') throw new ModelMappingError('StatusDto','Name is required')
        if ('ObjectTypeId' in obj === false || typeof obj.ObjectTypeId !== 'number') throw new ModelMappingError('StatusDto','ObjectTypeId is required')

        return new StatusDto(obj.Name, obj.ObjectTypeId)
    }
}