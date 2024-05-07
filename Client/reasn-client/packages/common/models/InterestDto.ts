import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { z } from "zod"

export const InterestDtoSchema = z.object({
    Name: z.string(),
    Level: z.number()
})

export type InterestDto = z.infer<typeof InterestDtoSchema>

export const InterestDtoMapper = {
    fromObject: (entity: object): InterestDto => {
        const result = InterestDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('InterestDto', result.error.name)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        const result = InterestDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('InterestDto', result.error.name)
        }
        return result.data
    }
}