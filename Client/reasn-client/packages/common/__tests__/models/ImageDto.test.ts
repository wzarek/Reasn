import { ImageDto, ImageDtoMapper } from '@reasn/common/models/ImageDto'
import ModelMappingError from '@reasn/common/errors/ModelMappingError'

describe('ImageDto', () => {
    const imageData = 'Test Image'
    const objectId = 1
    const objectTypeId = 1

    describe('fromJson', () => {
        it('should create an instance of ImageDto from JSON string', () => {
            const json = `{
                "ImageData": "${imageData}",
                "ObjectId": ${objectId},
                "ObjectTypeId": ${objectTypeId}
            }`

            let image = ImageDtoMapper.fromJSON(json)
            image = image as ImageDto

            expect(image.ImageData).toBe(imageData)
            expect(image.ObjectId).toBe(objectId)
            expect(image.ObjectTypeId).toBe(objectTypeId)
        })

        it('should return null if the JSON string is empty', () => {
            expect(() => ImageDtoMapper.fromJSON('')).toThrow(ModelMappingError)
        })

        it('should throw an error when providing json without each property individually', () => {  
            const jsonWithoutImageData = `{
                "ObjectId": ${objectId},
                "ObjectTypeId": ${objectTypeId}
            }`

            const jsonWithoutObjectId = `{
                "ImageData": "${imageData}",
                "ObjectTypeId": ${objectTypeId}
            }`

            const jsonWithoutObjectTypeId = `{
                "ImageData": "${imageData}",
                "ObjectId": ${objectId}
            }`

            expect(() => ImageDtoMapper.fromJSON(jsonWithoutImageData)).toThrow(ModelMappingError)
            expect(() => ImageDtoMapper.fromJSON(jsonWithoutObjectId)).toThrow(ModelMappingError)
            expect(() => ImageDtoMapper.fromJSON(jsonWithoutObjectTypeId)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of ImageDto from an object', () => {
            const object = {
                ImageData: imageData,
                ObjectId: objectId,
                ObjectTypeId: objectTypeId
            }

            let image = ImageDtoMapper.fromObject(object)
            image = image as ImageDto

            expect(image.ImageData).toBe(imageData)
            expect(image.ObjectId).toBe(objectId)
            expect(image.ObjectTypeId).toBe(objectTypeId)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                ImageData: true,
                ObjectId: null,
                ObjectTypeId: undefined
            }

            const objectWithoutImageData = {
                ObjectId: objectId,
                ObjectTypeId: objectTypeId
            }

            const objectWithoutObjectId = {
                ImageData: imageData,
                ObjectTypeId: objectTypeId
            }

            const objectWithoutObjectTypeId = {
                ImageData: imageData,
                ObjectId: objectId
            }

            expect(() => ImageDtoMapper.fromObject(object)).toThrow(ModelMappingError)
            expect(() => ImageDtoMapper.fromObject(objectWithoutImageData)).toThrow(ModelMappingError)
            expect(() => ImageDtoMapper.fromObject(objectWithoutObjectId)).toThrow(ModelMappingError)
            expect(() => ImageDtoMapper.fromObject(objectWithoutObjectTypeId)).toThrow(ModelMappingError)
        })
    })
})