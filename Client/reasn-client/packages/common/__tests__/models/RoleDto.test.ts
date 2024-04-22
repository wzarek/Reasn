import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { RoleDto } from '@reasn/common/models/RoleDto'

describe('RoleDto', () => {
    const roleName = 'admin'

    describe('constructor', () => {
        it('should create an instance of RoleDto', () => {
            const role = new RoleDto(roleName)

            expect(role).toBeInstanceOf(RoleDto)
            expect(role.Name).toBe(roleName)
        })
    })

    describe('fromJson', () => {
        it('should create an instance of RoleDto from JSON string', () => {
            const json = `{
                "Name": "${roleName}"
            }`

            let role = RoleDto.fromJson(json)

            expect(role).toBeInstanceOf(RoleDto)
            role = role as RoleDto

            expect(role.Name).toBe(roleName)
        })

        it('should return null if the JSON string is empty', () => {
            const json = ''

            const role = RoleDto.fromJson(json)

            expect(role).toBeNull()
        })

        it('should throw an error when providing JSON without each property individually', () => {  
            const jsonWithoutName = `{}`

            expect(() => RoleDto.fromJson(jsonWithoutName)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of RoleDto from an object', () => {
            const object = {
                Name: roleName
            }

            let role = RoleDto.fromObject(object)

            expect(role).toBeInstanceOf(RoleDto)
            role = role as RoleDto

            expect(role.Name).toBe(roleName)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                Name: null
            }
            
            const objectWithoutName = {}

            expect(() => RoleDto.fromObject(object)).toThrow(ModelMappingError)
            expect(() => RoleDto.fromObject(objectWithoutName)).toThrow(ModelMappingError)
        })
    })
})