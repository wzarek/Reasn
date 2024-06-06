/**
 * Represents an error that occurs when there is a problem with the authorization token when API service is used.
 */
class ApiAuthorizationError extends Error {
  message: string;

  /**
   * Creates a new instance of the ApiAuthorizationError class.
   * @param message The error message.
   */
  constructor(message: string) {
    super(message);
    this.message = message;
    this.name = "ApiAuthorizationError";
  }
}

export default ApiAuthorizationError;
