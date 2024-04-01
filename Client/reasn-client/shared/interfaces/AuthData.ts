import { UserRole } from "@reasn-enums/servicesEnums"

/**
 * Represents the authentication data.
 */
export interface AuthData {
    token: string
    role: UserRole
}