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
                "eventSlug": "${eventSlug}",
                "username": "${username}",
                "status": "${status}"
            }`;

      let participant = ParticipantDtoMapper.fromJSON(json);
      participant = participant as ParticipantDto;

      expect(participant.eventSlug).toBe(eventSlug);
      expect(participant.username).toBe(username);
      expect(participant.status).toBe(status);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => ParticipantDtoMapper.fromJSON("")).toThrow(
        ModelMappingError,
      );
    });

    it("should throw an error when providing JSON without each property individually", () => {
      const jsonWithoutEventSlug = `{
                "username": "${username}",
                "status": "${status}"
            }`;

      const jsonWithoutUsername = `{
                "eventSlug": "${eventSlug}",
                "status": "${status}"
            }`;

      const jsonWithoutStatus = `{
                "eventSlug": "${eventSlug}",
                "username": "${username}"
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
        eventSlug: eventSlug,
        username: username,
        status: status,
      };

      let participant = ParticipantDtoMapper.fromObject(object);
      participant = participant as ParticipantDto;

      expect(participant.eventSlug).toBe(eventSlug);
      expect(participant.username).toBe(username);
      expect(participant.status).toBe(status);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        eventStatus: true,
        username: null,
        status: "invalid",
      };

      const objectWithoutEventSlug = {
        username: username,
        status: status,
      };

      const objectWithoutUsername = {
        eventSlug: eventSlug,
        status: status,
      };

      const objectWithoutStatus = {
        eventSlug: eventSlug,
        username: username,
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
