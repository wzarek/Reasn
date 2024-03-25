interface AuthData {
    token: string;
    role: string;
}

const AUTH_DATA_KEY : string = 'REASN__AUTH_DATA'

export const getAuthDataFromSessionStorage = (): AuthData | null => {
    const data = sessionStorage.getItem(AUTH_DATA_KEY)

    if (data === null) {
        console.warn("No auth data found in session storage.")
        return null
    }

    let dataObj: { token: string; role: string } = JSON.parse(data)

    return {
        token: dataObj.token,
        role: dataObj.role
    }
}

export const setAuthDataInSessionStorage = (authData: AuthData): void => {
    if (!authData) {
        console.error("Cannot set an empty auth data in session storage.")
        return;
    }

    sessionStorage.setItem(AUTH_DATA_KEY, JSON.stringify(authData))
};

export const clearAuthDataInSessionStorage = (): void => {
    sessionStorage.removeItem(AUTH_DATA_KEY)
};
