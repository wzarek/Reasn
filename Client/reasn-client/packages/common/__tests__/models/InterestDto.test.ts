import { InterestDto, InterestDtoMapper } from '@reasn/common/models/InterestDto'
import ModelMappingError from '@reasn/common/errors/ModelMappingError'

describe('InterestDto', () => {
    const name = 'Test Interest'
    const level = 1

    describe('fromJson', () => {
        it('should create an instance of InterestDto from JSON string', () => {
            const json = `{
                "Name": "${name}",
                "Level": ${level}
            }`

            let interest = InterestDtoMapper.fromJSON(json)
            interest = interest as InterestDto

            expect(interest.Name).toBe(name)
            expect(interest.Level).toBe(level)
        })

        it('should return null if the JSON string is empty', () => {
            expect(() => InterestDtoMapper.fromJSON('')).toThrow(ModelMappingError)
        })

        it('should throw an error when providing json without each property individually', () => {  
            const jsonWithoutName = `{
                "Level": ${level}
            }`

            const jsonWithoutLevel = `{
                "Name": "${name}"
            }`

            expect(() => InterestDtoMapper.fromJSON(jsonWithoutName)).toThrow(ModelMappingError)
            expect(() => InterestDtoMapper.fromJSON(jsonWithoutLevel)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of InterestDto from an object', () => {
            const object = {
                Name: name,
                Level: level
            }

            let interest = InterestDtoMapper.fromObject(object)
            interest = interest as InterestDto

            expect(interest.Name).toBe(name)
            expect(interest.Level).toBe(level)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                Name: true,
                Level: null
            }

            const objectWithoutName = {
                Level: level
            }

            const objectWithoutLevel = {
                Name: name
            }

            expect(() => InterestDtoMapper.fromObject(object)).toThrow(ModelMappingError)
            expect(() => InterestDtoMapper.fromObject(objectWithoutName)).toThrow(ModelMappingError)
            expect(() => InterestDtoMapper.fromObject(objectWithoutLevel)).toThrow(ModelMappingError)
        })
    })
})