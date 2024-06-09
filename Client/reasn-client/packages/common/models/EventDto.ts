import ModelMappingError from "@reasn/common/errors/ModelMappingError";
import { TagDtoSchema } from "@reasn/common/models/TagDto";
import { EventStatus } from "@reasn/common/enums/modelsEnums";
import { z } from "zod";

export const EventDtoSchema = z.object({
  Name: z.string(),
  AddressId: z.number(),
  Description: z.string(),
  OrganizerId: z.number(),
  StartAt: z
    .string()
    .datetime({ offset: true })
    .or(z.date())
    .transform((arg) => new Date(arg)),
  EndAt: z
    .string()
    .datetime({ offset: true })
    .or(z.date())
    .transform((arg) => new Date(arg)),
  CreatedAt: z
    .string()
    .datetime({ offset: true })
    .or(z.date())
    .transform((arg) => new Date(arg)),
  UpdatedAt: z
    .string()
    .datetime({ offset: true })
    .or(z.date())
    .transform((arg) => new Date(arg)),
  Slug: z.string().nullable(),
  Status: z.nativeEnum(EventStatus),
  Tags: z.array(TagDtoSchema),
});

export type EventDto = z.infer<typeof EventDtoSchema>;

export const EventDtoMapper = {
  fromObject: (entity: object): EventDto => {
    const result = EventDtoSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "EventDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("EventDto", "Empty JSON string");
    }
    const result = EventDtoSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "EventDto",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
