import ModelMappingError from '@reasn/common/errors/ModelMappingError'

export class ImageDto {
    ImageData: string
    ObjectId: number
    ObjectTypeId: number
    
    constructor(imageData: string, objectId: number, objectTypeId: number) {
        this.ImageData = imageData
        this.ObjectId = objectId
        this.ObjectTypeId = objectTypeId
    }

    static fromJson(json: string): ImageDto | null {
        if (!json) {
            return null
        }

        return ImageDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): ImageDto | null {
        if (!obj) {
            return null
        }

        if ('ImageData' in obj === false || typeof obj.ImageData !== 'string') throw new ModelMappingError('ImageDto','ImageData is required')
        if ('ObjectId' in obj === false || typeof obj.ObjectId !== 'number') throw new ModelMappingError('ImageDto','ObjectId is required')
        if ('ObjectTypeId' in obj === false || typeof obj.ObjectTypeId !== 'number') throw new ModelMappingError('ImageDto','ObjectTypeId is required')

        return new ImageDto(obj.ImageData, obj.ObjectId, obj.ObjectTypeId)
    }
}