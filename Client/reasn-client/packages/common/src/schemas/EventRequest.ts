import { z } from "zod";
import { AddressDtoSchema } from "./AddressDto";
import { TagDtoSchema } from "./TagDto";
import { ParameterDtoSchema } from "./ParameterDto";

export const EventRequestSchema = z
  .object({
    name: z.string().max(64),
    address: AddressDtoSchema,
    description: z.string().max(4048),
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
    tags: z.array(TagDtoSchema).nullable(),
    parameters: z.array(ParameterDtoSchema).nullable(),
  })
  .refine((schema) => schema.startAt < schema.endAt);

export type EventRequest = z.infer<typeof EventRequestSchema>;
