import { ImageDto } from '@reasn/common/models/ImageDto'
import ModelMappingError from '@reasn/common/errors/ModelMappingError'

describe('ImageDto', () => {
    const imageData = 'Test Image'
    const objectId = 1
    const objectTypeId = 1

    describe('constructor', () => {
        it('should create an instance of ImageDto', () => {
            let image = new ImageDto(
                imageData,
                objectId,
                objectTypeId
            )

            expect(image).toBeInstanceOf(ImageDto)
            image = image as ImageDto

            expect(image.ImageData).toBe(imageData)
            expect(image.ObjectId).toBe(objectId)
            expect(image.ObjectTypeId).toBe(objectTypeId)
        })
    })

    describe('fromJson', () => {
        it('should create an instance of ImageDto from JSON string', () => {
            const json = `{
                "ImageData": "${imageData}",
                "ObjectId": ${objectId},
                "ObjectTypeId": ${objectTypeId}
            }`

            let image = ImageDto.fromJson(json)

            expect(image).toBeInstanceOf(ImageDto)
            image = image as ImageDto

            expect(image.ImageData).toBe(imageData)
            expect(image.ObjectId).toBe(objectId)
            expect(image.ObjectTypeId).toBe(objectTypeId)
        })

        it('should return null if the JSON string is empty', () => {
            const json = ''

            const image = ImageDto.fromJson(json)

            expect(image).toBeNull()
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

            expect(() => ImageDto.fromJson(jsonWithoutImageData)).toThrow(ModelMappingError)
            expect(() => ImageDto.fromJson(jsonWithoutObjectId)).toThrow(ModelMappingError)
            expect(() => ImageDto.fromJson(jsonWithoutObjectTypeId)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of ImageDto from an object', () => {
            const object = {
                ImageData: imageData,
                ObjectId: objectId,
                ObjectTypeId: objectTypeId
            }

            let image = ImageDto.fromObject(object)

            expect(image).toBeInstanceOf(ImageDto)
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

            expect(() => ImageDto.fromObject(object)).toThrow(ModelMappingError)
            expect(() => ImageDto.fromObject(objectWithoutImageData)).toThrow(ModelMappingError)
            expect(() => ImageDto.fromObject(objectWithoutObjectId)).toThrow(ModelMappingError)
            expect(() => ImageDto.fromObject(objectWithoutObjectTypeId)).toThrow(ModelMappingError)
        })
    })
})