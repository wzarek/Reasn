import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { InterestDtoSchema } from '@reasn/common/models/InterestDto'
import { z } from "zod"

export const UserDtoSchema = z.object({
    Username: z.string(),
    Name: z.string(),
    Surname: z.string(),
    Email: z.string(),
    Phone: z.string().nullable(),
    RoleId: z.number(),
    AddressId: z.number(),
    Intrests: z.array(InterestDtoSchema)
})

export type UserDto = z.infer<typeof UserDtoSchema>

export const UserDtoMapper = {
    fromObject: (entity: object): UserDto => {
        const result = UserDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('UserDto', result.error.message)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        if (!jsonEntity) {
            throw new ModelMappingError('UserDto', 'Empty JSON string')
        }
        const result = UserDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('UserDto', result.error.message)
        }
        return result.data
    }
}