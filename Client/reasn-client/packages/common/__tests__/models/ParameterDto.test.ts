import { ParameterDto, ParameterDtoMapper } from '@reasn/common/models/ParameterDto'
import ModelMappingError from '@reasn/common/errors/ModelMappingError'

describe('ParameterDto', () => {
    const key = 'Test Key'
    const value = 'Test Value'

    describe('fromJson', () => {
        it('should create an instance of ParameterDto from JSON string', () => {
            const json = `{
                "Key": "${key}",
                "Value": "${value}"
            }`

            let parameter = ParameterDtoMapper.fromJSON(json)
            parameter = parameter as ParameterDto

            expect(parameter.Key).toBe(key)
            expect(parameter.Value).toBe(value)
        })

        it('should return null if the JSON string is empty', () => {
            expect(() => ParameterDtoMapper.fromJSON('')).toThrow(ModelMappingError)
        })

        it('should throw an error when providing json without each property individually', () => {  
            const jsonWithoutKey = `{
                "Value": "${value}"
            }`

            const jsonWithoutValue = `{
                "Key": "${key}"
            }`

            expect(() => ParameterDtoMapper.fromJSON(jsonWithoutKey)).toThrow(ModelMappingError)
            expect(() => ParameterDtoMapper.fromJSON(jsonWithoutValue)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of ParameterDto from an object', () => {
            const object = {
                Key: key,
                Value: value
            }

            let parameter = ParameterDtoMapper.fromObject(object)
            parameter = parameter as ParameterDto

            expect(parameter.Key).toBe(key)
            expect(parameter.Value).toBe(value)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                Key: true,
                Value: null
            }

            const objectWithoutKey = {
                Value: value
            }

            const objectWithoutValue = {
                Key: key
            }

            expect(() => ParameterDtoMapper.fromObject(object)).toThrow(ModelMappingError)
            expect(() => ParameterDtoMapper.fromObject(objectWithoutKey)).toThrow(ModelMappingError)
            expect(() => ParameterDtoMapper.fromObject(objectWithoutValue)).toThrow(ModelMappingError)
        })
    })
})