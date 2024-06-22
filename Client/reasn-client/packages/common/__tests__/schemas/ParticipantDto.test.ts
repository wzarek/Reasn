import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";
import {
  ParticipantDto,
  ParticipantDtoMapper,
} from "@reasn/common/src/schemas/ParticipantDto";
import { ParticipantStatus } from "@reasn/common/src/enums/schemasEnums";

describe("ParticipantDto", () => {
  const eventSlug = "event-slug";
  const username = "username";
  const status = ParticipantStatus.INTERESTED;

  describe("fromJson", () => {
    it("should create an instance of ParticipantDto from JSON string", () => {
      const json = `{
                "EventSlug": "${eventSlug}",
                "Username": "${username}",
                "Status": "${status}"
            }`;

      let participant = ParticipantDtoMapper.fromJSON(json);
      participant = participant as ParticipantDto;

      expect(participant.EventSlug).toBe(eventSlug);
      expect(participant.Username).toBe(username);
      expect(participant.Status).toBe(status);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => ParticipantDtoMapper.fromJSON("")).toThrow(
        ModelMappingError,
      );
    });

    it("should throw an error when providing JSON without each property individually", () => {
      const jsonWithoutEventSlug = `{
                "Username": "${username}",
                "Status": "${status}"
            }`;

      const jsonWithoutUsername = `{
                "EventSlug": "${eventSlug}",
                "Status": "${status}"
            }`;

      const jsonWithoutStatus = `{
                "EventSlug": "${eventSlug}",
                "Username": "${username}"
            }`;

      expect(() => ParticipantDtoMapper.fromJSON(jsonWithoutEventSlug)).toThrow(
        ModelMappingError,
      );
      expect(() => ParticipantDtoMapper.fromJSON(jsonWithoutUsername)).toThrow(
        ModelMappingError,
      );
      expect(() => ParticipantDtoMapper.fromJSON(jsonWithoutStatus)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of ParticipantDto from an object", () => {
      const object = {
        EventSlug: eventSlug,
        Username: username,
        Status: status,
      };

      let participant = ParticipantDtoMapper.fromObject(object);
      participant = participant as ParticipantDto;

      expect(participant.EventSlug).toBe(eventSlug);
      expect(participant.Username).toBe(username);
      expect(participant.Status).toBe(status);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        EventStatus: true,
        Username: null,
        Status: "invalid",
      };

      const objectWithoutEventSlug = {
        Username: username,
        Status: status,
      };

      const objectWithoutUsername = {
        EventSlug: eventSlug,
        Status: status,
      };

      const objectWithoutStatus = {
        EventSlug: eventSlug,
        Username: username,
      };

      expect(() => ParticipantDtoMapper.fromObject(object)).toThrow(
        ModelMappingError,
      );
      expect(() =>
        ParticipantDtoMapper.fromObject(objectWithoutEventSlug),
      ).toThrow(ModelMappingError);
      expect(() =>
        ParticipantDtoMapper.fromObject(objectWithoutUsername),
      ).toThrow(ModelMappingError);
      expect(() =>
        ParticipantDtoMapper.fromObject(objectWithoutStatus),
      ).toThrow(ModelMappingError);
    });
  });
});
