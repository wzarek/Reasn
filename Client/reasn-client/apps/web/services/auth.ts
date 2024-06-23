import { sendRequest, HttpMethod } from "@/lib/request";
import {
  TokenPayload,
  TokenPayloadMapper,
} from "@reasn/common/src/schemas/TokenPayload";
import { LoginRequest } from "@reasn/common/src/schemas/LoginRequest";
import { RegisterRequest } from "@reasn/common/src/schemas/RegisterRequest";
import { UserDto, UserDtoMapper } from "@reasn/common/src/schemas/UserDto";

const baseUrl = `${process.env.REASN_API_URL}/api/v1/auth`;

export const login = async (
  loginRequest: LoginRequest,
): Promise<TokenPayload> => {
  const url = new URL(`${baseUrl}/login`);

  const response = await sendRequest<TokenPayload>(url, {
    method: HttpMethod.POST,
    body: loginRequest,
  });
  return TokenPayloadMapper.fromObject(response);
};

export const register = async (
  registerRequest: RegisterRequest,
): Promise<UserDto> => {
  const url = new URL(`${baseUrl}/register`);

  const response = await sendRequest<UserDto>(url, {
    method: HttpMethod.POST,
    body: registerRequest,
  });
  return UserDtoMapper.fromObject(response);
};
