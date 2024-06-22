import ModelMappingError from "../errors/ModelMappingError";
import { TagDtoSchema } from "./TagDto";
import { EventStatus } from "../enums/schemasEnums";
import { z } from "zod";

export const EventDtoSchema = z.object({
  Name: z.string().max(64),
  AddressId: z.number(),
  Description: z.string().max(4048),
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
  Slug: z
    .string()
    .nullable()
    .refine(
      (value) =>
        value === null ||
        (value.length <= 128 && /^[\p{L}\d]+[\p{L}\d-]*$/u.test(value)),
    ),

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
