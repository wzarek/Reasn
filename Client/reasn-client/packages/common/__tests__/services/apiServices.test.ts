import { sendRequest } from "@reasn/common/src/services/apiServices";
import { getAuthDataFromSessionStorage } from "@reasn/common/src/services/authorizationServices";
import { AuthData } from "@reasn/common/src/interfaces/AuthData";
import { HttpMethod } from "@reasn/common/src/enums/serviceEnums";
import { describe, expect, it, jest, beforeEach } from "@jest/globals";
import fetch from "cross-fetch";
import ApiConnectionError from "@reasn/common/src/errors/ApiConnectionError";
import ApiAuthorizationError from "@reasn/common/src/errors/ApiAuthorizationError";

jest.mock("cross-fetch");
jest.mock("@reasn/common/src/services/authorizationServices");

describe("sendRequest", () => {
  beforeEach(() => {
    (fetch as jest.Mock).mockClear();
    (getAuthDataFromSessionStorage as jest.Mock).mockClear();
  });

  it("should return data when response is ok", async () => {
    const mockData = { key: "value" };
    (fetch as jest.Mock).mockImplementationOnce(() => ({
      ok: true,
      json: () => Promise.resolve(mockData),
    }));

    const data = await sendRequest("http://example.com", HttpMethod.GET);

    expect(data).toEqual(mockData);
  });

  it("should throw an API error when response is not ok", async () => {
    const mockData = { message: "Error message" };
    (fetch as jest.Mock).mockImplementationOnce(() => ({
      ok: false,
      status: 500,
      statusText: mockData.message,
      json: () => Promise.resolve(mockData),
    }));

    await expect(
      sendRequest("http://example.com", HttpMethod.GET),
    ).rejects.toThrow(ApiConnectionError);
  });

  it("should include auth token in headers when authRequired is true", async () => {
    const mockData = { key: "value" };
    (fetch as jest.Mock).mockImplementationOnce(() => ({
      ok: true,
      json: () => Promise.resolve(mockData),
    }));
    (getAuthDataFromSessionStorage as jest.Mock).mockReturnValueOnce({
      token: "token",
    } as AuthData);

    await sendRequest("http://example.com", HttpMethod.GET, {}, true);

    expect(fetch).toHaveBeenCalledWith("http://example.com", {
      method: HttpMethod.GET,
      headers: { Authorization: "Bearer token" },
    });
  });

  it("should have correct fetch options", async () => {
    const mockData = { key: "value" };
    (fetch as jest.Mock).mockImplementationOnce(() => ({
      ok: true,
      json: () => Promise.resolve(mockData),
    }));

    await sendRequest("http://example.com", HttpMethod.POST, { key: "value" });

    expect(fetch).toHaveBeenCalledWith("http://example.com", {
      method: HttpMethod.POST,
      headers: {},
      body: JSON.stringify({ key: "value" }),
    });
  });

  it("should throw an AUTH error when authRequired is true and no auth data is found", async () => {
    (getAuthDataFromSessionStorage as jest.Mock).mockReturnValueOnce(null);

    await expect(
      sendRequest("http://example.com", HttpMethod.GET, {}, true),
    ).rejects.toThrow(ApiAuthorizationError);
  });
});
