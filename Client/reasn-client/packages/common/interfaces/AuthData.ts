import { UserRole } from "@reasn/common/enums/serviceEnums";

/**
 * Represents the authentication data.
 */
export interface AuthData {
  token: string;
  role: UserRole;
}
