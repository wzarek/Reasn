import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { StatusDto, StatusDtoMapper } from '@reasn/common/models/StatusDto'

describe('StatusDto', () => {
    const statusName = 'Active'
    const objectTypeId = 1

    describe('fromJson', () => {
        it('should create an instance of StatusDto from JSON string', () => {
            const json = `{
                "Name": "${statusName}",
                "ObjectTypeId": ${objectTypeId}
            }`

            let status = StatusDtoMapper.fromJSON(json)
            status = status as StatusDto

            expect(status.Name).toBe(statusName)
            expect(status.ObjectTypeId).toBe(objectTypeId)
        })

        it('should return null if the JSON string is empty', () => {
            expect(() => StatusDtoMapper.fromJSON('')).toThrow(ModelMappingError)
        })

        it('should throw an error when providing JSON without each property individually', () => {  
            const jsonWithoutObjectTypeId = `{
                "Name": "${statusName}"
            }`

            const jsonWithoutName = `{
                "ObjectTypeId": ${objectTypeId}
            }`

            expect(() => StatusDtoMapper.fromJSON(jsonWithoutName)).toThrow(ModelMappingError)
            expect(() => StatusDtoMapper.fromJSON(jsonWithoutObjectTypeId)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of StatusDto from an object', () => {
            const object = {
                Name: statusName,
                ObjectTypeId: objectTypeId
            }

            let status = StatusDtoMapper.fromObject(object)
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

            expect(() => StatusDtoMapper.fromObject(object)).toThrow(ModelMappingError)
            expect(() => StatusDtoMapper.fromObject(objectWithoutName)).toThrow(ModelMappingError)
            expect(() => StatusDtoMapper.fromObject(objectWithoutObjectTypeId)).toThrow(ModelMappingError)
        })
    })
})