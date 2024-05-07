import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { z } from "zod"

export const ImageDtoSchema = z.object({
    ImageData: z.string(),
    ObjectId: z.number(),
    ObjectTypeId: z.number()
})

export type ImageDto = z.infer<typeof ImageDtoSchema>

export const ImageDtoMapper = {
    fromObject: (entity: object): ImageDto => {
        const result = ImageDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('ImageDto', result.error.name)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        const result = ImageDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('ImageDto', result.error.name)
        }
        return result.data
    }
}