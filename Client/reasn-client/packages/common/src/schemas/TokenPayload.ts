import ModelMappingError from "../errors/ModelMappingError";
import { z } from "zod";

export const TokenPayloadSchema = z.object({
  tokenType: z.string(),
  accessToken: z.string(),
  expiresIn: z.number(),
});

export type TokenPayload = z.infer<typeof TokenPayloadSchema>;

export const TokenPayloadMapper = {
  fromObject: (entity: object): TokenPayload => {
    const result = TokenPayloadSchema.safeParse(entity);
    if (!result.success) {
      throw new ModelMappingError(
        "TokenPayload",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
  fromJSON: (jsonEntity: string): any => {
    if (!jsonEntity) {
      throw new ModelMappingError("TokenPayload", "Empty JSON string");
    }
    const result = TokenPayloadSchema.safeParse(JSON.parse(jsonEntity));
    if (!result.success) {
      throw new ModelMappingError(
        "TokenPayload",
        result.error.message,
        result.error,
      );
    }
    return result.data;
  },
};
