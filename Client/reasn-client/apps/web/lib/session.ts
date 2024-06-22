import { jwtDecode, JwtPayload } from "jwt-decode";
import { UserRole } from "@reasn/common/src/enums/modelsEnums";
import { getToken } from "@/lib/token";

export const SESSION_DEFAULT = {
  token: null,
  isAuthenticated: () => false,
};

type User = {
  email: string;
  role: UserRole;
};

export type Session = {
  token: string | null;
  user?: User;
  isAuthenticated: () => boolean;
};

interface ReasnJwtPayload extends JwtPayload {
  email?: string;
  role?: UserRole;
}

export const getSession = (): Session => {
  const token = getToken();
  if (!token) {
    return SESSION_DEFAULT;
  }

  try {
    const decodedToken = jwtDecode<ReasnJwtPayload>(token);
    const isUserValid =
      decodedToken.email !== undefined && decodedToken.role !== undefined;

    return {
      token: token,
      user: isUserValid
        ? {
            email: decodedToken.email as string,
            role: decodedToken.role as UserRole,
          }
        : undefined,
      isAuthenticated: () => token !== null && isUserValid,
    };
  } catch (error) {
    console.error("[ERROR]: Failed to decode JWT token");
    return SESSION_DEFAULT;
  }
};
