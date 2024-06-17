import ModelMappingError from "../errors/ModelMappingError";
import { AddressDtoSchema } from "../schemas/AddressDto";
import { z } from "zod";

export const RegisterRequestSchema = z.object({
  name: z
    .string()
    .max(64)
    .regex(/^[\p{L}\d._%+-]{4,}$/u),
  surname: z
    .string()
    .max(64)
    .regex(/^\p{L}+(?:[\s'-]\p{L}+)*$/u),
  email: z.string().email(),
  username: z
    .string()
    .max(64)
    .regex(/^[\p{L}\d._%+-]{4,}$/u),
  password: z
    .string()
    .regex(/^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$/u),
  phone: z
    .string()
    .nullable()
    .refine((value) => value === null || /^\+\d{1,3}\s\d{1,15}$/.test(value)),
  address: AddressDtoSchema,
  Role: z.enum(["User", "Organizer"]),
});

export type RegisterRequest = z.infer<typeof RegisterRequestSchema>;

export const RegisterRequestMapper = {
  fromObject: (entity: object): RegisterRequest => {
    const result = RegisterRequestSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "RegisterRequest",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("RegisterRequest", "Empty JSON string");
    }
    const result = RegisterRequestSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "RegisterRequest",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
