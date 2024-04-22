import ModelMappingError from '@reasn/common/errors/ModelMappingError'

export class ParameterDto {
    Key: string
    Value: string
    
    constructor(key: string, value: string) {
        this.Key = key
        this.Value = value
    }

    static fromJson(json: string): ParameterDto | null {
        if (!json) {
            return null
        }

        return ParameterDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): ParameterDto | null {
        if (!obj) {
            return null
        }

        if ('Key' in obj === false || typeof obj.Key !== 'string') throw new ModelMappingError('ParameterDto','Key is required')
        if ('Value' in obj === false || typeof obj.Value !== 'string') throw new ModelMappingError('ParameterDto','Value is required')

        return new ParameterDto(obj.Key, obj.Value)
    }
}