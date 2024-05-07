import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { z } from "zod"

export const TagDtoSchema = z.object({
    Name: z.string()
})

export type TagDto = z.infer<typeof TagDtoSchema>

export const TagDtoMapper = {
    fromObject: (entity: object): TagDto => {
        const result = TagDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('TagDto', result.error.name)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        const result = TagDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('TagDto', result.error.name)
        }
        return result.data
    }
}