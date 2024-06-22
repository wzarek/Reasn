import { UserRole } from "../enums/schemasEnums";

/**
 * Represents the authentication data.
 */
export interface AuthData {
  token: string;
  role: UserRole;
}
