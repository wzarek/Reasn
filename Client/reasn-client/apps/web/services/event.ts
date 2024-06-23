import { sendRequest, HttpMethod } from "@/lib/request";
import { EventRequest } from "@reasn/common/src/schemas/EventRequest";
import {
  EventResponse,
  EventResponseMapper,
} from "@reasn/common/src/schemas/EventResponse";
import { ParameterDto } from "@reasn/common/src/schemas/ParameterDto";
import { TagDto } from "@reasn/common/src/schemas/TagDto";
import { CommentDto } from "@reasn/common/src/schemas/CommentDto";

const baseUrl = `${process.env.REASN_API_URL}/api/v1/events`;

export const getEvents = async (
  params: Record<string, string> = {},
): Promise<EventResponse[]> => {
  const url = new URL(baseUrl);
  url.search = new URLSearchParams(params).toString();

  const response = await sendRequest<EventResponse[]>(url, {
    method: HttpMethod.GET,
  });
  return response;
};

export const createEvent = async (
  eventRequest: EventRequest,
): Promise<EventResponse> => {
  const url = new URL(baseUrl);

  const response = await sendRequest<EventResponse>(url, {
    method: HttpMethod.POST,
    body: eventRequest,
    authRequired: true,
  });
  return EventResponseMapper.fromObject(response);
};

export const getEventBySlug = async (slug: string): Promise<EventResponse> => {
  const url = new URL(`${baseUrl}/${slug}`);

  const response = await sendRequest<EventResponse>(url, {
    method: HttpMethod.GET,
  });
  return EventResponseMapper.fromObject(response);
};

export const updateEvent = async (
  slug: string,
  event: any,
): Promise<EventResponse> => {
  const url = new URL(`${baseUrl}/${slug}`);

  const response = await sendRequest<EventResponse>(url, {
    method: HttpMethod.PUT,
    body: event,
    authRequired: true,
  });
  return EventResponseMapper.fromObject(response);
};

export const getEventsRequests = async (): Promise<EventResponse[]> => {
  const url = new URL(`${baseUrl}/requests`);

  const response = await sendRequest<EventResponse[]>(url, {
    method: HttpMethod.GET,
    authRequired: true,
  });
  return response;
};

export const approveEventRequest = async (slug: string): Promise<Object> => {
  const url = new URL(`${baseUrl}/requests/${slug}`);

  const response = await sendRequest<Object>(url, {
    method: HttpMethod.POST,
    authRequired: true,
  });
  return response;
};

export const addEventImage = async (
  slug: string,
  images: Blob[],
): Promise<Object> => {
  const url = new URL(`${baseUrl}/${slug}/images`);

  const formData = new FormData();
  images.forEach((image) => {
    formData.append("images", image);
  });

  const response = await sendRequest<Object>(url, {
    method: HttpMethod.POST,
    body: formData,
    authRequired: true,
  });
  return response;
};

export const updateEventImage = async (
  slug: string,
  images: Blob[],
): Promise<Object> => {
  const url = new URL(`${baseUrl}/${slug}/images`);

  const formData = new FormData();
  images.forEach((image) => {
    formData.append("images", image);
  });

  const response = await sendRequest<Object>(url, {
    method: HttpMethod.PUT,
    body: formData,
    authRequired: true,
  });
  return response;
};

export const getEventImages = async (slug: string): Promise<string[]> => {
  const url = new URL(`${baseUrl}/${slug}/images`);

  const response = await sendRequest<string[]>(url, { method: HttpMethod.GET });
  return response;
};

export const getEventParticipants = async (slug: string): Promise<any> => {
  const url = new URL(`${baseUrl}/${slug}/participants`);

  const response = await sendRequest<any>(url, { method: HttpMethod.GET });
  return response;
};

export const getEventComments = async (slug: string): Promise<CommentDto[]> => {
  const url = new URL(`${baseUrl}/${slug}/comments`);

  const response = await sendRequest<CommentDto[]>(url, {
    method: HttpMethod.GET,
  });
  return response;
};

export const addEventComment = async (
  slug: string,
  comment: any,
): Promise<any> => {
  const url = new URL(`${baseUrl}/${slug}/comments`);

  const response = await sendRequest<any>(url, {
    method: HttpMethod.POST,
    body: comment,
    authRequired: true,
  });
  return response;
};

export const getEventsParameters = async (): Promise<ParameterDto[]> => {
  const url = new URL(`${baseUrl}/parameters`);

  const response = await sendRequest<ParameterDto[]>(url, {
    method: HttpMethod.GET,
    authRequired: true,
  });
  return response;
};

export const getEventsTags = async (): Promise<TagDto[]> => {
  const url = new URL(`${baseUrl}/tags`);

  const response = await sendRequest<TagDto[]>(url, {
    method: HttpMethod.GET,
    authRequired: true,
  });
  return response;
};

export const deleteEventsTag = async (tagId: number): Promise<Object> => {
  const url = new URL(`${baseUrl}/tags/${tagId}`);

  const response = await sendRequest<Object>(url, {
    method: HttpMethod.DELETE,
    authRequired: true,
  });
  return response;
};
