import { getAuthDataFromSessionStorage } from '@reasn-services/authorizationServices'
import { HttpMethod } from '@reasn-enums/servicesEnums'
import ApiConnectionError from '@reasn-errors/ApiConnectionError'
import fetch from "cross-fetch"
import ApiAuthorizationError from '@reasn-errors/ApiAuthorizationError'

/**
 * Sends an HTTP request to the specified URL.
 * 
 * @param url - The URL to send the request to.
 * @param httpMethod - The HTTP method to use for the request.
 * @param bodyData - The data to include in the request body.
 * @param authRequired - Indicates whether the request requires authentication. Default is false.
 * @returns A promise that resolves to the response data of type T.
 * @throws {ApiConnectionError} If the response status is not ok.
 */
export const sendRequest = async<T>(url: string, httpMethod: HttpMethod, bodyData: Object = {}, authRequired: boolean = false): Promise<T> => {
    try {
        let headers = {}
        if (authRequired){
            const authData = getAuthDataFromSessionStorage()
            if (!authData) {
                throw new ApiAuthorizationError('Unauthorized access. No auth data found in session storage')
            }
            headers['Authorization'] = `Bearer ${authData?.token}`
        }

        const fetchOptions = {
            method: httpMethod, 
            headers: headers,
        }

        if (httpMethod === HttpMethod.POST || httpMethod === HttpMethod.PUT) {
            fetchOptions["body"] = JSON.stringify(bodyData)
        }
        
        const response = await fetch(url, fetchOptions)

        if (!response.ok) {
            const errorData = await response.json() as {message:string};
            console.error(`[HTTP ${response.status}]: ${errorData.message ?? 'No message provided'}`)
            throw new ApiConnectionError(response.status, `${errorData.message ?? 'No message provided'}`)
        }
        
        return await response.json() as T
    } catch (error) {
        console.error(`Error while fetching data for url: ${httpMethod} ${url}, error: ${error}`)
        throw error
    }
}