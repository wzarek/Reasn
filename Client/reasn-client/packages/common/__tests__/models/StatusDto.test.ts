import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { StatusDto } from '@reasn/common/models/StatusDto'

describe('StatusDto', () => {
    const statusName = 'Active'
    const objectTypeId = 1

    describe('constructor', () => {
        it('should create an instance of StatusDto', () => {
            const status = new StatusDto(statusName, objectTypeId)

            expect(status).toBeInstanceOf(StatusDto)
            expect(status.Name).toBe(statusName)
            expect(status.ObjectTypeId).toBe(objectTypeId)
        })
    })

    describe('fromJson', () => {
        it('should create an instance of StatusDto from JSON string', () => {
            const json = `{
                "Name": "${statusName}",
                "ObjectTypeId": ${objectTypeId}
            }`

            let status = StatusDto.fromJson(json)

            expect(status).toBeInstanceOf(StatusDto)
            status = status as StatusDto

            expect(status.Name).toBe(statusName)
            expect(status.ObjectTypeId).toBe(objectTypeId)
        })

        it('should return null if the JSON string is empty', () => {
            const json = ''

            const status = StatusDto.fromJson(json)

            expect(status).toBeNull()
        })

        it('should throw an error when providing JSON without each property individually', () => {  
            const jsonWithoutObjectTypeId = `{
                "Name": "${statusName}"
            }`

            const jsonWithoutName = `{
                "ObjectTypeId": ${objectTypeId}
            }`

            expect(() => StatusDto.fromJson(jsonWithoutName)).toThrow(ModelMappingError)
            expect(() => StatusDto.fromJson(jsonWithoutObjectTypeId)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of StatusDto from an object', () => {
            const object = {
                Name: statusName,
                ObjectTypeId: objectTypeId
            }

            let status = StatusDto.fromObject(object)

            expect(status).toBeInstanceOf(StatusDto)
            status = status as StatusDto

            expect(status.Name).toBe(statusName)
            expect(status.ObjectTypeId).toBe(objectTypeId)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                Name: null,
                ObjectTypeId: true
            }

            const objectWithoutName = {
                ObjectTypeId: objectTypeId
            }

            const objectWithoutObjectTypeId = {
                Name: statusName
            }

            expect(() => StatusDto.fromObject(object)).toThrow(ModelMappingError)
            expect(() => StatusDto.fromObject(objectWithoutName)).toThrow(ModelMappingError)
            expect(() => StatusDto.fromObject(objectWithoutObjectTypeId)).toThrow(ModelMappingError)
        })
    })
})