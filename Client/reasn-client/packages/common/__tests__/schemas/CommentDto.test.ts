import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";
import {
  CommentDto,
  CommentDtoMapper,
} from "@reasn/common/src/schemas/CommentDto";

describe("CommentDto", () => {
  const eventSlug = "event-slug";
  const content = "Test Content";
  const createdAt = new Date();
  const username = "username";
  const userImageUrl = "url";

  describe("fromJson", () => {
    it("should create an instance of CommentDto from JSON string", () => {
      const json = `{
                "eventSlug": "${eventSlug}",
                "content": "${content}",
                "createdAt": "${createdAt.toISOString()}",
                "username": "${username}",
                "userImageUrl": "${userImageUrl}"
            }`;

      let comment = CommentDtoMapper.fromJSON(json);
      comment = comment as CommentDto;

      expect(comment.eventSlug).toBe(eventSlug);
      expect(comment.content).toBe(content);
      expect(comment.createdAt).toEqual(createdAt);
      expect(comment.username).toBe(username);
      expect(comment.userImageUrl).toBe(userImageUrl);
    });

    it("should throw an error if the JSON string is empty", () => {
      expect(() => CommentDtoMapper.fromJSON("")).toThrow(ModelMappingError);
    });

    it("should throw an error when providing JSON without each property individually", () => {
      const jsonWithoutEventSlug = `{
                "content": "${content}",
                "createdAt": "${createdAt.toISOString()}",
                "username": "${username}",
                "userImageUrl": "${userImageUrl}"
            }`;

      const jsonWithoutContent = `{
                "eventSlug": "${eventSlug}",
                "createdAt": "${createdAt.toISOString()}",
                "username": "${username}",
                "userImageUrl": "${userImageUrl}"
            }`;

      const jsonWithoutCreatedAt = `{
                "eventSlug": "${eventSlug}",
                "content": "${content}",
                "username": "${username}",
                "userImageUrl": "${userImageUrl}"
            }`;

      const jsonWithoutUsername = `{
                "eventSlug": "${eventSlug}",
                "content": "${content}",
                "createdAt": "${createdAt.toISOString()}",
                "userImageUrl": "${userImageUrl}"
            }`;

      const jsonWithoutUserImageUrl = `{
                "eventSlug": "${eventSlug}",
                "content": "${content}",
                "createdAt": "${createdAt.toISOString()}",
                "username": "${username}"
            }`;

      expect(() => CommentDtoMapper.fromJSON(jsonWithoutEventSlug)).toThrow(
        ModelMappingError,
      );
      expect(() => CommentDtoMapper.fromJSON(jsonWithoutContent)).toThrow(
        ModelMappingError,
      );
      expect(() => CommentDtoMapper.fromJSON(jsonWithoutCreatedAt)).toThrow(
        ModelMappingError,
      );
      expect(() => CommentDtoMapper.fromJSON(jsonWithoutUsername)).toThrow(
        ModelMappingError,
      );
      expect(() => CommentDtoMapper.fromJSON(jsonWithoutUserImageUrl)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of CommentDto from an object", () => {
      const object = {
        eventSlug: eventSlug,
        content: content,
        createdAt: createdAt,
        username: username,
        userImageUrl: userImageUrl,
      };

      let comment = CommentDtoMapper.fromObject(object);
      comment = comment as CommentDto;

      expect(comment.eventSlug).toBe(eventSlug);
      expect(comment.content).toBe(content);
      expect(comment.createdAt).toEqual(createdAt);
      expect(comment.username).toBe(username);
      expect(comment.userImageUrl).toBe(userImageUrl);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        eventSlug: eventSlug,
        content: true,
        createdAt: createdAt,
        username: null,
        userImageUrl: userImageUrl,
      };

      const objectWithoutEventSlug = {
        content: content,
        createdAt: createdAt,
        username: username,
        userImageUrl: userImageUrl,
      };

      const objectWithoutContent = {
        eventSlug: eventSlug,
        createdAt: createdAt,
        username: username,
        userImageUrl: userImageUrl,
      };

      const objectWithoutCreatedAt = {
        eventSlug: eventSlug,
        content: content,
        username: username,
        userImageUrl: userImageUrl,
      };

      const objectWithoutUsername = {
        eventSlug: eventSlug,
        content: content,
        createdAt: createdAt,
        userImageUrl: userImageUrl,
      };

      const objectWithoutUserImageUrl = {
        eventSlug: eventSlug,
        content: content,
        createdAt: createdAt,
        username: username,
      };

      expect(() => CommentDtoMapper.fromObject(object)).toThrow(
        ModelMappingError,
      );
      expect(() => CommentDtoMapper.fromObject(objectWithoutEventSlug)).toThrow(
        ModelMappingError,
      );
      expect(() => CommentDtoMapper.fromObject(objectWithoutContent)).toThrow(
        ModelMappingError,
      );
      expect(() => CommentDtoMapper.fromObject(objectWithoutCreatedAt)).toThrow(
        ModelMappingError,
      );
      expect(() => CommentDtoMapper.fromObject(objectWithoutUsername)).toThrow(
        ModelMappingError,
      );
      expect(() =>
        CommentDtoMapper.fromObject(objectWithoutUserImageUrl),
      ).toThrow(ModelMappingError);
    });

    it("should throw an error if date is in incorrect format", () => {
      const object = {
        eventSlug: eventSlug,
        content: content,
        createdAt: "invalid date",
        username: username,
        userImageUrl: userImageUrl,
      };

      const objectMissingZ = {
        eventSlug: eventSlug,
        content: content,
        createdAt: "2009-06-15T13:45:30.0000000",
        username: username,
        userImageUrl: userImageUrl,
      };

      expect(() => CommentDtoMapper.fromObject(object)).toThrow(
        ModelMappingError,
      );
      expect(() => CommentDtoMapper.fromObject(objectMissingZ)).toThrow(
        ModelMappingError,
      );
    });

    it("should properly parse date string", () => {
      const object = {
        eventSlug: eventSlug,
        content: content,
        createdAt: "2009-06-15T13:45:30.0000000-07:00",
        username: username,
        userImageUrl: userImageUrl,
      };

      const objectWithoutOffset = {
        eventSlug: eventSlug,
        content: content,
        createdAt: "2009-06-15T13:45:30.0000000Z",
        username: username,
        userImageUrl: userImageUrl,
      };

      const objectWithoutMilliseconds = {
        eventSlug: eventSlug,
        content: content,
        createdAt: "2009-06-15T13:45:30Z",
        username: username,
        userImageUrl: userImageUrl,
      };

      let comment = CommentDtoMapper.fromObject(object);
      comment = comment as CommentDto;

      expect(comment.createdAt).toEqual(
        new Date("2009-06-15T13:45:30.0000000-07:00"),
      );

      comment = CommentDtoMapper.fromObject(objectWithoutOffset);
      comment = comment as CommentDto;

      expect(comment.createdAt).toEqual(
        new Date("2009-06-15T13:45:30.0000000Z"),
      );

      comment = CommentDtoMapper.fromObject(objectWithoutMilliseconds);
      comment = comment as CommentDto;

      expect(comment.createdAt).toEqual(new Date("2009-06-15T13:45:30Z"));
    });
  });
});
