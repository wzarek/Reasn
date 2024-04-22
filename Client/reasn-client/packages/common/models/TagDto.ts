import ModelMappingError from '@reasn/common/errors/ModelMappingError'

export class TagDto {
    Name: string
    
    constructor(name: string) {
        this.Name = name
    }

    static fromJson(json: string): TagDto | null {
        if (!json) {
            return null
        }

        return TagDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): TagDto | null {
        if (!obj) {
            return null
        }

        if ('Name' in obj === false || typeof obj.Name !== 'string') throw new ModelMappingError('TagDto','Name is required')

        return new TagDto(obj.Name)
    }
}