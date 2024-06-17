import ModelMappingError from "../errors/ModelMappingError";
import { ParticipantStatus } from "../enums/modelsEnums";
import { z } from "zod";

export const ParticipantDtoSchema = z.object({
  EventId: z.number(),
  UserId: z.number(),
  Status: z.nativeEnum(ParticipantStatus),
});

export type ParticipantDto = z.infer<typeof ParticipantDtoSchema>;

export const ParticipantDtoMapper = {
  fromObject: (entity: object): ParticipantDto => {
    const result = ParticipantDtoSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "ParticipantDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("ParticipantDto", "Empty JSON string");
    }
    const result = ParticipantDtoSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "ParticipantDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
