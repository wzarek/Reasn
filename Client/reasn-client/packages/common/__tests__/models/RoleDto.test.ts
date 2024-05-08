import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { RoleDto, RoleDtoMapper } from '@reasn/common/models/RoleDto'

describe('RoleDto', () => {
    const roleName = 'admin'

    describe('fromJson', () => {
        it('should create an instance of RoleDto from JSON string', () => {
            const json = `{
                "Name": "${roleName}"
            }`

            let role = RoleDtoMapper.fromJSON(json)
            role = role as RoleDto

            expect(role.Name).toBe(roleName)
        })

        it('should return null if the JSON string is empty', () => {
            expect(() => RoleDtoMapper.fromJSON('')).toThrow(ModelMappingError)
        })

        it('should throw an error when providing JSON without each property individually', () => {  
            const jsonWithoutName = `{}`

            expect(() => RoleDtoMapper.fromJSON(jsonWithoutName)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of RoleDto from an object', () => {
            const object = {
                Name: roleName
            }

            let role = RoleDtoMapper.fromObject(object)
            role = role as RoleDto

            expect(role.Name).toBe(roleName)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                Name: null
            }
            
            const objectWithoutName = {}

            expect(() => RoleDtoMapper.fromObject(object)).toThrow(ModelMappingError)
            expect(() => RoleDtoMapper.fromObject(objectWithoutName)).toThrow(ModelMappingError)
        })
    })
})