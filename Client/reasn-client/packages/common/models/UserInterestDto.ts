import ModelMappingError from "@reasn/common/errors/ModelMappingError";
import { InterestDtoSchema } from "@reasn/common/models/InterestDto";
import { z } from "zod";

export const UserInterestDtoSchema = z.object({
  Interest: InterestDtoSchema,
  Level: z.number(),
});

export type UserInterestDto = z.infer<typeof UserInterestDtoSchema>;

export const UserInterestDtoMapper = {
  fromObject: (entity: object): UserInterestDto => {
    const result = UserInterestDtoSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "UserDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("UserDto", "Empty JSON string");
    }
    const result = UserInterestDtoSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "UserDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
