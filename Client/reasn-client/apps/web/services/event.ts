import { sendRequest, HttpMethod } from "@/lib/request";

const baseUrl = `${process.env.REASN_API_URL}/api/v1/events`;

export const getEvents = async (
  params: Record<string, string> = {},
): Promise<any> => {
  const url = new URL(baseUrl);
  url.search = new URLSearchParams(params).toString();

  const response = await sendRequest<any>(url, { method: HttpMethod.GET });
  return response;
};

export const createEvent = async (event: any): Promise<any> => {
  const url = new URL(baseUrl);

  const response = await sendRequest<any>(url, {
    method: HttpMethod.POST,
    body: event,
    authRequired: true,
  });
  return response;
};

export const getEventBySlug = async (slug: string): Promise<any> => {
  const url = new URL(`${baseUrl}/${slug}`);

  const response = await sendRequest<any>(url, { method: HttpMethod.GET });
  return response;
};

export const updateEvent = async (slug: string, event: any): Promise<any> => {
  const url = new URL(`${baseUrl}/${slug}`);

  const response = await sendRequest<any>(url, {
    method: HttpMethod.PUT,
    body: event,
    authRequired: true,
  });
  return response;
};

export const getEventsRequests = async (): Promise<any> => {
  const url = new URL(`${baseUrl}/requests`);

  const response = await sendRequest<any>(url, {
    method: HttpMethod.GET,
    authRequired: true,
  });
  return response;
};

export const approveEventRequest = async (slug: string): Promise<any> => {
  const url = new URL(`${baseUrl}/requests/${slug}`);

  const response = await sendRequest<any>(url, {
    method: HttpMethod.POST,
    authRequired: true,
  });
  return response;
};

export const addEventImage = async (
  slug: string,
  images: Blob[],
): Promise<any> => {
  const url = new URL(`${baseUrl}/${slug}/images`);

  const formData = new FormData();
  images.forEach((image) => {
    formData.append("images", image);
  });

  const response = await sendRequest<any>(url, {
    method: HttpMethod.POST,
    body: formData,
    authRequired: true,
  });
  return response;
};

export const updateEventImage = async (
  slug: string,
  images: Blob[],
): Promise<any> => {
  const url = new URL(`${baseUrl}/${slug}/images`);

  const formData = new FormData();
  images.forEach((image) => {
    formData.append("images", image);
  });

  const response = await sendRequest<any>(url, {
    method: HttpMethod.PUT,
    body: formData,
    authRequired: true,
  });
  return response;
};

export const getEventImages = async (slug: string): Promise<any> => {
  const url = new URL(`${baseUrl}/${slug}/images`);

  const response = await sendRequest<any>(url, { method: HttpMethod.GET });
  return response;
};

export const getEventParticipants = async (slug: string): Promise<any> => {
  const url = new URL(`${baseUrl}/${slug}/participants`);

  const response = await sendRequest<any>(url, { method: HttpMethod.GET });
  return response;
};

export const getEventComments = async (slug: string): Promise<any> => {
  const url = new URL(`${baseUrl}/${slug}/comments`);

  const response = await sendRequest<any>(url, { method: HttpMethod.GET });
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

export const getEventsParameters = async (): Promise<any> => {
  const url = new URL(`${baseUrl}/parameters`);

  const response = await sendRequest<any>(url, {
    method: HttpMethod.GET,
    authRequired: true,
  });
  return response;
};

export const getEventsTags = async (): Promise<any> => {
  const url = new URL(`${baseUrl}/tags`);

  const response = await sendRequest<any>(url, {
    method: HttpMethod.GET,
    authRequired: true,
  });
  return response;
};

export const deleteEventsTag = async (tagId: number): Promise<any> => {
  const url = new URL(`${baseUrl}/tags/${tagId}`);

  const response = await sendRequest<any>(url, {
    method: HttpMethod.DELETE,
    authRequired: true,
  });
  return response;
};
