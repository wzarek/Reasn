import { UserRole } from "../enums/modelsEnums";

/**
 * Represents the authentication data.
 */
export interface AuthData {
  token: string;
  role: UserRole;
}
