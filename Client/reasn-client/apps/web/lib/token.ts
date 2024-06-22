import { cookies } from "next/headers";
import { TokenPayload } from "@reasn/common/src/schemas/tokenPayload";

const TOKEN_KEY = "REASN_TOKEN";

export const setToken = (tokenPayload: TokenPayload): void => {
  cookies().set(TOKEN_KEY, tokenPayload.accessToken, {
    maxAge: tokenPayload.expiresIn,
    secure: process.env.NODE_ENV === "production",
    sameSite: "strict",
    httpOnly: true,
  });
};

export const clearToken = (): void => {
  cookies().set(TOKEN_KEY, "", { maxAge: 0 });
};

export const getToken = (): string | null => {
  const token = cookies().get(TOKEN_KEY)?.value;
  if (!token) return null;
  return token;
};
