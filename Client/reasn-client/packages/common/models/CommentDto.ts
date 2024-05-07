import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { z } from "zod"

export const CommentDtoSchema = z.object({
    EventId: z.number(),
    Content: z.string(),
    CreatedAt: z.date(),
    UserId: z.number()
})

export type CommentDto = z.infer<typeof CommentDtoSchema>

export const CommentDtoMapper = {
    fromObject: (entity: object): CommentDto => {
        const result = CommentDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('CommentDto', result.error.name)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        const result = CommentDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('CommentDto', result.error.name)
        }
        return result.data
    }
}