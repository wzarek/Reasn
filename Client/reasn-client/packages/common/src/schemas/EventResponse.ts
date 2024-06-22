import { z } from "zod";
import { AddressDtoSchema } from "@reasn/common/src/schemas/AddressDto";
import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";
import { TagDtoSchema } from "@reasn/common/src/schemas/TagDto";
import { ParameterDtoSchema } from "@reasn/common/src/schemas/ParameterDto";
import { EventStatus } from "@reasn/common/src/enums/schemasEnums";

export const EventRespsoneSchema = z.object({
  name: z.string().max(64),
  addressId: z.number(),
  address: AddressDtoSchema,
  description: z.string().max(4048),
  organizer: z.object({
    username: z.string(),
    image: z.string(),
  }),
  startAt: z
    .string()
    .datetime({ offset: true })
    .or(z.date())
    .transform((arg) => new Date(arg)),
  endAt: z
    .string()
    .datetime({ offset: true })
    .or(z.date())
    .transform((arg) => new Date(arg)),
  createdAt: z
    .string()
    .datetime({ offset: true })
    .or(z.date())
    .transform((arg) => new Date(arg)),
  updatedAt: z
    .string()
    .datetime({ offset: true })
    .or(z.date())
    .transform((arg) => new Date(arg)),
  slug: z
    .string()
    .max(128)
    .regex(/^[\p{L}\d]+[\p{L}\d-]*$/u)
    .nullable(),
  status: z.nativeEnum(EventStatus),
  tags: z.array(TagDtoSchema),
  parameters: z.array(ParameterDtoSchema),
  participants: z.object({
    interested: z.number(),
    participating: z.number(),
  }),
});

export type EventResponse = z.infer<typeof EventRespsoneSchema>;

export const EventResponseMapper = {
  fromObject: (entity: object): EventResponse => {
    const result = EventRespsoneSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "EventResponse",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("EventResponse", "Empty JSON string");
    }
    const result = EventRespsoneSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "EventResponse",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
