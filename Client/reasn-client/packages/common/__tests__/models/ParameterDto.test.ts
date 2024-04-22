import { ParameterDto } from '@reasn/common/models/ParameterDto'
import ModelMappingError from '@reasn/common/errors/ModelMappingError'

describe('ParameterDto', () => {
    const key = 'Test Key'
    const value = 'Test Value'

    describe('constructor', () => {
        it('should create an instance of ParameterDto', () => {
            const parameter = new ParameterDto(key, value)

            expect(parameter).toBeInstanceOf(ParameterDto)
            expect(parameter.Key).toBe(key)
            expect(parameter.Value).toBe(value)
        })
    })

    describe('fromJson', () => {
        it('should create an instance of ParameterDto from JSON string', () => {
            const json = `{
                "Key": "${key}",
                "Value": "${value}"
            }`

            let parameter = ParameterDto.fromJson(json)

            expect(parameter).toBeInstanceOf(ParameterDto)
            parameter = parameter as ParameterDto

            expect(parameter.Key).toBe(key)
            expect(parameter.Value).toBe(value)
        })

        it('should return null if the JSON string is empty', () => {
            const json = ''

            const parameter = ParameterDto.fromJson(json)

            expect(parameter).toBeNull()
        })

        it('should throw an error when providing json without each property individually', () => {  
            const jsonWithoutKey = `{
                "Value": "${value}"
            }`

            const jsonWithoutValue = `{
                "Key": "${key}"
            }`

            expect(() => ParameterDto.fromJson(jsonWithoutKey)).toThrow(ModelMappingError)
            expect(() => ParameterDto.fromJson(jsonWithoutValue)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of ParameterDto from an object', () => {
            const object = {
                Key: key,
                Value: value
            }

            let parameter = ParameterDto.fromObject(object)

            expect(parameter).toBeInstanceOf(ParameterDto)
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

            expect(() => ParameterDto.fromObject(object)).toThrow(ModelMappingError)
            expect(() => ParameterDto.fromObject(objectWithoutKey)).toThrow(ModelMappingError)
            expect(() => ParameterDto.fromObject(objectWithoutValue)).toThrow(ModelMappingError)
        })
    })
})