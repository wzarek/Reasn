import ModelMappingError from "../errors/ModelMappingError";
import { UserInterestDtoSchema } from "../schemas/UserInterestDto";
import { AddressDtoSchema } from "../schemas/AddressDto";
import { UserRole } from "../enums/schemasEnums";
import { z } from "zod";

export const UserDtoSchema = z.object({
  username: z
    .string()
    .max(64)
    .regex(/^[\p{L}\d._%+-]{4,}$/u),
  name: z
    .string()
    .max(64)
    .regex(/^\p{Lu}[\p{Ll}\s'-]+$/u),
  surname: z
    .string()
    .max(64)
    .regex(/^\p{L}+(?:[\s'-]\p{L}+)*$/u),
  email: z.string().email(),
  phone: z
    .string()
    .nullable()
    .refine((value) => value === null || /^\+\d{1,3}\s\d{1,15}$/.test(value)),
  role: z.nativeEnum(UserRole),
  addressId: z.number(),
  address: AddressDtoSchema,
  intrests: z.array(UserInterestDtoSchema),
});

export type UserDto = z.infer<typeof UserDtoSchema>;

export const UserDtoMapper = {
  fromObject: (entity: object): UserDto => {
    const result = UserDtoSchema.safeParse(entity);
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
    const result = UserDtoSchema.safeParse(JSON.parse(jsonEntity));
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
