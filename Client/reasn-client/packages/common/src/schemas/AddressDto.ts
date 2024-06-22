import ModelMappingError from "../errors/ModelMappingError";
import { z } from "zod";

export const AddressDtoSchema = z.object({
  Country: z
    .string()
    .max(64)
    .regex(/^\p{Lu}[\p{L}\s'-]*(?<![\s-])$/u),
  City: z
    .string()
    .max(64)
    .regex(/^\p{Lu}[\p{Ll}'.]+(?:[\s-][\p{L}'.]+)*$/u),
  Street: z
    .string()
    .max(64)
    .regex(/^[\p{L}\d\s\-/.,#']+(?<![-\s#,])$/u),
  State: z
    .string()
    .max(64)
    .regex(/^\p{Lu}\p{Ll}+(?:(\s|-)\p{L}+)*$/u),
  ZipCode: z
    .string()
    .regex(/^[\p{L}\d\s-]{3,}$/u)
    .nullable(),
});

export type AddressDto = z.infer<typeof AddressDtoSchema>;

export const AddressDtoMapper = {
  fromObject: (entity: object): AddressDto => {
    const result = AddressDtoSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "AddressDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("AddressDto", "Empty JSON string");
    }
    const result = AddressDtoSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "AddressDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
