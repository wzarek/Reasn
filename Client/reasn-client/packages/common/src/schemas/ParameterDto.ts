import ModelMappingError from "../errors/ModelMappingError";
import { z } from "zod";

export const ParameterDtoSchema = z.object({
  key: z
    .string()
    .max(32)
    .regex(/^\p{L}+(?:\s\p{L}+)*$/u),
  value: z
    .string()
    .max(64)
    .regex(/^[\p{L}\d]+(?:\s[\p{L}\d]+)*$/u),
});

export type ParameterDto = z.infer<typeof ParameterDtoSchema>;

export const ParameterDtoMapper = {
  fromObject: (entity: object): ParameterDto => {
    const result = ParameterDtoSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "ParameterDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("ParameterDto", "Empty JSON string");
    }
    const result = ParameterDtoSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "ParameterDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
