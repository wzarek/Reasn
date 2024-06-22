import ModelMappingError from "../errors/ModelMappingError";
import { UserInterestDtoSchema } from "../schemas/UserInterestDto";
import { AddressDtoSchema } from "../schemas/AddressDto";
import { UserRole } from "../enums/schemasEnums";
import { z } from "zod";

export const UserDtoSchema = z.object({
  Username: z
    .string()
    .max(64)
    .regex(/^[\p{L}\d._%+-]{4,}$/u),
  Name: z
    .string()
    .max(64)
    .regex(/^\p{Lu}[\p{Ll}\s'-]+$/u),
  Surname: z
    .string()
    .max(64)
    .regex(/^\p{L}+(?:[\s'-]\p{L}+)*$/u),
  Email: z.string().email(),
  Phone: z
    .string()
    .nullable()
    .refine((value) => value === null || /^\+\d{1,3}\s\d{1,15}$/.test(value)),
  Role: z.nativeEnum(UserRole),
  AddressId: z.number(),
  Address: AddressDtoSchema,
  Intrests: z.array(UserInterestDtoSchema),
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
