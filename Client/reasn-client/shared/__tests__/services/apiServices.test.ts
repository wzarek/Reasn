import { sendRequest } from '@reasn-services/apiServices';
import { HttpMethods } from '@reasn-enums/servicesEnums';
import { describe, expect, it } from '@jest/globals';
import fetch from 'cross-fetch';
import ApiConnectionError from '@reasn-errors/ApiConnectionError';

jest.mock('cross-fetch');

describe('sendRequest', () => {
    beforeEach(() => {
        (fetch as jest.Mock).mockClear();
      });

  it('should return data when response is ok', async () => {
    const mockData = { key: 'value' };
    (fetch as jest.Mock).mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve(mockData),
      });

    const data = await sendRequest('http://example.com', HttpMethods.GET);

    expect(data).toEqual(mockData);
  });

  it('should throw an error when response is not ok', async () => {
    const mockData = { message: 'Error message' };
    (fetch as jest.Mock).mockResolvedValueOnce({
        ok: false,
        status: 500,
        statusText: mockData.message,
        json: () => Promise.resolve(mockData),
      });

    await expect(sendRequest('http://example.com', HttpMethods.GET)).rejects.toThrow(ApiConnectionError);
  });
});