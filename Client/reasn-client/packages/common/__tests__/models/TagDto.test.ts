import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { TagDto } from '@reasn/common/models/TagDto'

describe('TagDto', () => {
    const name = 'tag name'

    describe('constructor', () => {
        it('should create an instance of TagDto', () => {
            const tag = new TagDto(name)

            expect(tag).toBeInstanceOf(TagDto)
            expect(tag.Name).toBe(name)
        })
    })

    describe('fromJson', () => {
        it('should create an instance of TagDto from JSON string', () => {
            const json = `{
                "Name": "${name}"
            }`

            let tag = TagDto.fromJson(json)

            expect(tag).toBeInstanceOf(TagDto)
            tag = tag as TagDto

            expect(tag.Name).toBe(name)
        })

        it('should return null if the JSON string is empty', () => {
            const json = ''

            const tag = TagDto.fromJson(json)

            expect(tag).toBeNull()
        })

        it('should throw an error when providing JSON without the Name property', () => {  
            const jsonWithoutName = `{}`

            expect(() => TagDto.fromJson(jsonWithoutName)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of TagDto from an object', () => {
            const object = {
                Name: name
            }

            let tag = TagDto.fromObject(object)

            expect(tag).toBeInstanceOf(TagDto)
            tag = tag as TagDto

            expect(tag.Name).toBe(name)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                Name: null
            }

            const objectWithoutName = {}

            expect(() => TagDto.fromObject(object)).toThrow(ModelMappingError)
            expect(() => TagDto.fromObject(objectWithoutName)).toThrow(ModelMappingError)
        })
    })
})