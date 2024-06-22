import ApiAuthorizationError from "@reasn/common/src/errors/ApiAuthorizationError";
import ApiConnectionError from "@reasn/common/src/errors/ApiConnectionError";
import fetch from "cross-fetch";
import { getToken } from "@/lib/token";

export enum HttpMethod {
  GET = "GET",
  POST = "POST",
  PUT = "PUT",
  DELETE = "DELETE",
}

export type ProblemDetails = {
  type?: string;
  title: string;
  status: number;
  details?: string;
  instance?: string;
};

export const sendRequest = async <T>(
  url: string,
  httpMethod: HttpMethod,
  body: Object = {},
  authRequired: boolean = false,
): Promise<T> => {
  try {
    let headers: HeadersInit = new Headers();
    if (authRequired) {
      const token = getToken();
      if (!token) {
        throw new ApiAuthorizationError(
          "Unauthorized access. No token found in cookies",
        );
      }
      headers.set("Authorization", `Bearer ${token}`);
    }

    const fetchOptions: RequestInit = {
      method: httpMethod,
      headers,
    };

    if (httpMethod == HttpMethod.POST || httpMethod == HttpMethod.PUT) {
      headers.set("Content-Type", "application/json");
      fetchOptions.body = JSON.stringify(body);
    }

    const response = await fetch(url, fetchOptions);
    if (!response.ok) {
      const problemDetails = (await response.json()) as ProblemDetails;
      console.error(
        `[HTTP ${problemDetails.instance ?? ""} ${response.status}]: ${
          problemDetails.details ?? problemDetails.title
        }`,
      );

      throw new ApiConnectionError(
        response.status,
        problemDetails.details ?? problemDetails.title,
      );
    }

    return (await response.json()) as T;
  } catch (error) {
    console.error(
      `Error while sending request to ${url} with method ${httpMethod}: ${error}`,
    );
    throw Error;
  }
};
