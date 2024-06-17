import ModelMappingError from "../errors/ModelMappingError";
import { z } from "zod";

export const LoginRequestSchema = z.object({
  email: z.string().email(),
  password: z.string(),
});

export type LoginRequest = z.infer<typeof LoginRequestSchema>;

export const LoginRequestMapper = {
  fromObject: (entity: object): LoginRequest => {
    const result = LoginRequestSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "LoginRequest",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("LoginRequest", "Empty JSON string");
    }
    const result = LoginRequestSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "LoginRequest",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
