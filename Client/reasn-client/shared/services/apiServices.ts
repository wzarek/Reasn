import { getAuthDataFromSessionStorage } from './authorizationServices'

export enum HTTP_METHODS {
    GET = "GET",
    POST = "POST",
    DELETE = "DELETE",
    PUT = "PUT"
}

export const sendRequest = async<T>(url: string, httpMethod: HTTP_METHODS, bodyData: Object, authRequired: boolean = false): Promise<T> => {
    try {
        let headers = {}
        if (authRequired){
            const authData = getAuthDataFromSessionStorage()
            const token = authData?.token ?? 'no-token'
            headers['Authorization'] = `Bearer ${token}`
        }

        const response = await fetch(url, {
            method: httpMethod, 
            headers: headers,
            body: JSON.stringify(bodyData)
        })
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`)
        }
        
        return await response.json() as T
    } catch (error) {
        console.error('Error fetching data:', error)
        throw error
    }
}