import { UserRole } from "@reasn/common/enums/serviceEnums";
import { AuthData } from "@reasn/common/interfaces/AuthData";

const AUTH_DATA_KEY = "REASN_AUTH_DATA";

/**
 * Retrieves the authentication data from the session storage.
 * @returns The authentication data if found, otherwise null.
 */
export const getAuthDataFromSessionStorage = (): AuthData | null => {
  const data = sessionStorage.getItem(AUTH_DATA_KEY);

  if (data === null) {
    console.warn("No auth data found in session storage.");
    return null;
  }

  let dataObj: { token: string; role: UserRole } = JSON.parse(data);

  return {
    token: dataObj.token,
    role: dataObj.role,
  };
};

/**
 * Sets the authentication data in the session storage.
 * @param authData - The authentication data to be set.
 */
export const setAuthDataInSessionStorage = (authData: AuthData): void => {
  if (!authData) {
    console.error("Cannot set an empty auth data in session storage.");
    return;
  }

  sessionStorage.setItem(AUTH_DATA_KEY, JSON.stringify(authData));
};

/**
 * Clears the authentication data from the session storage.
 */
export const clearAuthDataInSessionStorage = (): void => {
  sessionStorage.removeItem(AUTH_DATA_KEY);
};
