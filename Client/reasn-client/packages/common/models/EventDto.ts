import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { TagDtoSchema } from '@reasn/common/models/TagDto'
import { z } from "zod"

export const EventDtoSchema = z.object({
    Name: z.string(),
    AddressId: z.number(),
    Description: z.string(),
    OrganizerId: z.number(),
    StartAt: z.date(),
    EndAt: z.date(),
    CreatedAt: z.date(),
    UpdatedAt: z.date(),
    Slug: z.string().nullable(),
    StatusId: z.number(),
    Tags: z.array(TagDtoSchema)
})

export type EventDto = z.infer<typeof EventDtoSchema>

export const EventDtoMapper = {
    fromObject: (entity: object): EventDto => {
        const result = EventDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('EventDto', result.error.name)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        const result = EventDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('EventDto', result.error.name)
        }
        return result.data
    }
}