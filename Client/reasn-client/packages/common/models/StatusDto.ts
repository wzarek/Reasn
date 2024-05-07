import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { z } from "zod"

export const StatusDtoSchema = z.object({
    Name: z.string(),
    ObjectTypeId: z.number()
})

export type StatusDto = z.infer<typeof StatusDtoSchema>

export const StatusDtoMapper = {
    fromObject: (entity: object): StatusDto => {
        const result = StatusDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('StatusDto', result.error.name)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        const result = StatusDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('StatusDto', result.error.name)
        }
        return result.data
    }
}