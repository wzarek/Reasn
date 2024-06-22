import { sendRequest, HttpMethod } from "@/lib/request";
import { UserDto, UserDtoMapper } from "@reasn/common/src/schemas/UserDto";
import {
  ParticipantDto,
  ParticipantDtoMapper,
} from "@reasn/common/src/schemas/ParticipantDto";

const baseUrl = `${process.env.REASN_API_URL}/api/v1`;
const baseUsersUrl = `${baseUrl}/users`;
const baseMeUrl = `${baseUrl}/me`;

export const getUsers = async (
  params: Record<string, string> = {},
): Promise<any> => {
  const url = new URL(baseUsersUrl);
  url.search = new URLSearchParams(params).toString();

  const response = await sendRequest<any>(url, {
    method: HttpMethod.GET,
    authRequired: true,
  });
  return response;
};

export const getUserByUsername = async (username: string): Promise<UserDto> => {
  const url = new URL(`${baseUsersUrl}/${username}`);

  const response = await sendRequest<UserDto>(url, { method: HttpMethod.GET });
  return UserDtoMapper.fromObject(response);
};

export const updateUser = async (
  username: string,
  user: UserDto,
): Promise<UserDto> => {
  const url = new URL(`${baseUsersUrl}/${username}`);

  const response = await sendRequest<UserDto>(url, {
    method: HttpMethod.PUT,
    body: user,
    authRequired: true,
  });
  return UserDtoMapper.fromObject(response);
};

export const getUsersInterests = async (
  params: Record<string, string> = {},
): Promise<any> => {
  const url = new URL(`${baseUsersUrl}/interests`);
  url.search = new URLSearchParams(params).toString();

  const response = await sendRequest<any>(url, { method: HttpMethod.GET });
  return response;
};

export const deleteUserInterest = async (
  insterestId: number,
): Promise<Object> => {
  const url = new URL(`${baseUsersUrl}/interests/${insterestId}`);

  const response = await sendRequest<Object>(url, {
    method: HttpMethod.DELETE,
    authRequired: true,
  });
  return response;
};

export const getCurrentUser = async (): Promise<UserDto> => {
  const url = new URL(baseMeUrl);

  const response = await sendRequest<UserDto>(url, {
    method: HttpMethod.GET,
    authRequired: true,
  });
  return UserDtoMapper.fromObject(response);
};

export const updateCurrentUser = async (user: UserDto): Promise<UserDto> => {
  const url = new URL(baseMeUrl);

  const response = await sendRequest<UserDto>(url, {
    method: HttpMethod.PUT,
    body: user,
    authRequired: true,
  });
  return UserDtoMapper.fromObject(response);
};

export const addCurrentUserImage = async (image: Blob): Promise<Object> => {
  const url = new URL(`${baseMeUrl}/image`);

  const formData = new FormData();
  formData.append("images", image);

  const response = await sendRequest<Object>(url, {
    method: HttpMethod.POST,
    body: formData,
    authRequired: true,
  });
  return response;
};

export const updateCurrentUserImage = async (image: Blob): Promise<Object> => {
  const url = new URL(`${baseMeUrl}/image`);

  const formData = new FormData();
  formData.append("images", image);

  const response = await sendRequest<Object>(url, {
    method: HttpMethod.PUT,
    body: formData,
    authRequired: true,
  });
  return response;
};

export const deleteCurrentUserImage = async (): Promise<Object> => {
  const url = new URL(`${baseMeUrl}/image`);

  const response = await sendRequest<Object>(url, {
    method: HttpMethod.DELETE,
    authRequired: true,
  });
  return response;
};

export const getCurrentUserEvents = async (
  params: Record<string, string> = {},
): Promise<any> => {
  const url = new URL(`${baseMeUrl}/events`);
  url.search = new URLSearchParams(params).toString();

  const response = await sendRequest<any>(url, {
    method: HttpMethod.GET,
    authRequired: true,
  });
  return response;
};

export const enrollCurrentUserInEvent = async (
  slug: string,
): Promise<ParticipantDto> => {
  const url = new URL(`${baseMeUrl}/events/${slug}/enroll`);

  const response = await sendRequest<ParticipantDto>(url, {
    method: HttpMethod.POST,
    authRequired: true,
  });
  return ParticipantDtoMapper.fromObject(response);
};

export const confirmCurrentUserAttendace = async (
  slug: string,
): Promise<ParticipantDto> => {
  const url = new URL(`${baseMeUrl}/events/${slug}/confirm`);

  const response = await sendRequest<ParticipantDto>(url, {
    method: HttpMethod.POST,
    authRequired: true,
  });
  return ParticipantDtoMapper.fromObject(response);
};

export const cancelCurrentUserAttendance = async (
  slug: string,
): Promise<ParticipantDto> => {
  const url = new URL(`${baseMeUrl}/events/${slug}/cancel`);

  const response = await sendRequest<ParticipantDto>(url, {
    method: HttpMethod.POST,
    authRequired: true,
  });
  return ParticipantDtoMapper.fromObject(response);
};
