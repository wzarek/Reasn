import { InterestDto } from '@reasn/common/models/InterestDto'
import ModelMappingError from '@reasn/common/errors/ModelMappingError'

describe('InterestDto', () => {
    const name = 'Test Interest'
    const level = 1

    describe('constructor', () => {
        it('should create an instance of InterestDto', () => {
            const interest = new InterestDto(name, level)

            expect(interest).toBeInstanceOf(InterestDto)
            expect(interest.Name).toBe(name)
            expect(interest.Level).toBe(level)
        })
    })

    describe('fromJson', () => {
        it('should create an instance of InterestDto from JSON string', () => {
            const json = `{
                "Name": "${name}",
                "Level": ${level}
            }`

            let interest = InterestDto.fromJson(json)

            expect(interest).toBeInstanceOf(InterestDto)
            interest = interest as InterestDto

            expect(interest.Name).toBe(name)
            expect(interest.Level).toBe(level)
        })

        it('should return null if the JSON string is empty', () => {
            const json = ''

            const interest = InterestDto.fromJson(json)

            expect(interest).toBeNull()
        })

        it('should throw an error when providing json without each property individually', () => {  
            const jsonWithoutName = `{
                "Level": ${level}
            }`

            const jsonWithoutLevel = `{
                "Name": "${name}"
            }`

            expect(() => InterestDto.fromJson(jsonWithoutName)).toThrow(ModelMappingError)
            expect(() => InterestDto.fromJson(jsonWithoutLevel)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of InterestDto from an object', () => {
            const object = {
                Name: name,
                Level: level
            }

            let interest = InterestDto.fromObject(object)

            expect(interest).toBeInstanceOf(InterestDto)
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

            expect(() => InterestDto.fromObject(object)).toThrow(ModelMappingError)
            expect(() => InterestDto.fromObject(objectWithoutName)).toThrow(ModelMappingError)
            expect(() => InterestDto.fromObject(objectWithoutLevel)).toThrow(ModelMappingError)
        })
    })
})