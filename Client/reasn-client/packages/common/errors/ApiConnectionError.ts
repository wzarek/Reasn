
/**
 * Represents an error that occurs when there is a problem with the API connection.
 */
class ApiConnectionError extends Error {
    statusCode: number
    message: string

    /**
     * Creates a new instance of the ApiConnectionError class.
     * @param statusCode The HTTP status code associated with the error.
     * @param message The error message.
     */
    constructor(statusCode: number, message: string) {
        super(message)
        this.statusCode = statusCode
        this.message = message
        this.name = 'ApiConnectionError'
    }
}

export default ApiConnectionError