import ModelMappingError from "../errors/ModelMappingError";
import { z } from "zod";

export const CommentDtoSchema = z.object({
  eventSlug: z
    .string()
    .max(128)
    .regex(/^[\p{L}\d]+[\p{L}\d-]*$/u),
  content: z.string().max(1024),
  createdAt: z
    .string()
    .datetime({ offset: true })
    .or(z.date())
    .transform((arg) => new Date(arg)),
  username: z
    .string()
    .max(64)
    .regex(/^[\p{L}\d._%+-]{4,}$/u),
  userImageUrl: z.string().nullable(),
});

export type CommentDto = z.infer<typeof CommentDtoSchema>;

export const CommentDtoMapper = {
  fromObject: (entity: object): CommentDto => {
    const result = CommentDtoSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "CommentDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("CommentDto", "Empty JSON string");
    }
    const result = CommentDtoSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "CommentDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
