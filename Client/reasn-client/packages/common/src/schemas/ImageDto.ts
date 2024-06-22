import ModelMappingError from "../errors/ModelMappingError";
import { ObjectType } from "../enums/schemasEnums";
import { z } from "zod";

export const ImageDtoSchema = z.object({
  ImageData: z.string(),
  ObjectId: z.number(),
  ObjectType: z.nativeEnum(ObjectType),
});

export type ImageDto = z.infer<typeof ImageDtoSchema>;

export const ImageDtoMapper = {
  fromObject: (entity: object): ImageDto => {
    const result = ImageDtoSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "ImageDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("ImageDto", "Empty JSON string");
    }
    const result = ImageDtoSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "ImageDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
