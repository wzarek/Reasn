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

export type RequestOptions = {
  method: HttpMethod;
  body?: Object | FormData;
  authRequired?: boolean;
};

export const sendRequest = async <T>(
  url: string | URL,
  { method, body = {}, authRequired = false }: RequestOptions,
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
      method: method,
      headers,
    };

    if (method == HttpMethod.POST || method == HttpMethod.PUT) {
      if (body instanceof FormData) {
        fetchOptions.body = body;
      } else {
        headers.set("Content-Type", "application/json");
        fetchOptions.body = JSON.stringify(body);
      }
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

    return response.json().catch(() => {}) as T;
  } catch (error) {
    console.error(
      `Error while sending request to ${url} with method ${method}: ${error}`,
    );
    throw Error(`Error while sending request to ${url}`);
  }
};
