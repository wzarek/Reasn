import ModelMappingError from '@reasn/common/errors/ModelMappingError'

export class InterestDto {
    Name: string
    Level: number
    
    constructor(name: string, level: number) {
        this.Name = name
        this.Level = level
    }

    static fromJson(json: string): InterestDto | null {
        if (!json) {
            return null
        }

        return InterestDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): InterestDto | null {
        if (!obj) {
            return null
        }

        if ('Name' in obj === false || typeof obj.Name !== 'string') throw new ModelMappingError('InterestDto','Name is required')
        if ('Level' in obj === false || typeof obj.Level !== 'number') throw new ModelMappingError('InterestDto','Level is required')

        return new InterestDto(obj.Name, obj.Level)
    }
}