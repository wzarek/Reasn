import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { z } from "zod"

export const AddressDtoSchema = z.object({
    Country: z.string(),
    City: z.string(),
    Street: z.string(),
    State: z.string(),
    ZipCode: z.string().nullable()
})

export type AddressDto = z.infer<typeof AddressDtoSchema>

export const AddressDtoMapper = {
    fromObject: (entity: object): AddressDto => {
        const result = AddressDtoSchema.safeParse(entity)
        if (!result.success) {
            throw new ModelMappingError('AddressDto', result.error.message, result.error.issues)
        }
        return result.data
    },
    fromJSON: (jsonEntity: string): any => {
        if (!jsonEntity) {
            throw new ModelMappingError('AddressDto', 'Empty JSON string')
        }
        const result = AddressDtoSchema.safeParse(JSON.parse(jsonEntity))
        if (!result.success) {
            throw new ModelMappingError('AddressDto', result.error.message, result.error.issues)
        }
        return result.data
    }
}