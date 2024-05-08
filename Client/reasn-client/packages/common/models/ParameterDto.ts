import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { z } from "zod"

export const ParameterDtoSchema = z.object({
    Key: z.string(),
    Value: z.string()
})

export type ParameterDto = z.infer<typeof ParameterDtoSchema>

export const ParameterDtoMapper = {
    fromObject: (entity: object): ParameterDto => {
        const result = ParameterDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('ParameterDto', result.error.message)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        if (!jsonEntity) {
            throw new ModelMappingError('ParameterDto', 'Empty JSON string')
        }
        const result = ParameterDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('ParameterDto', result.error.message)
        }
        return result.data
    }
}