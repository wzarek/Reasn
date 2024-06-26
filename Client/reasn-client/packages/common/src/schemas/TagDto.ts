import ModelMappingError from "../errors/ModelMappingError";
import { z } from "zod";

export const TagDtoSchema = z.object({
  name: z
    .string()
    .max(64)
    .regex(/^\p{L}+(?:\s\p{L}+)*$/u),
});

export type TagDto = z.infer<typeof TagDtoSchema>;

export const TagDtoMapper = {
  fromObject: (entity: object): TagDto => {
    const result = TagDtoSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError("TagDto", result.error.message, result.error);
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("TagDto", "Empty JSON string");
    }
    const result = TagDtoSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError("TagDto", result.error.message, result.error);
    }
    return result.data;
  },
};
