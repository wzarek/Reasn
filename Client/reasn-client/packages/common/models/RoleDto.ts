import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { z } from "zod"

export const RoleDtoSchema = z.object({
    Name: z.string()
})

export type RoleDto = z.infer<typeof RoleDtoSchema>

export const RoleDtoMapper = {
    fromObject: (entity: object): RoleDto => {
        const result = RoleDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('RoleDto', result.error.message, result.error.issues)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        if (!jsonEntity) {
            throw new ModelMappingError('RoleDto', 'Empty JSON string')
        }
        const result = RoleDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('RoleDto', result.error.message, result.error.issues)
        }
        return result.data
    }
}