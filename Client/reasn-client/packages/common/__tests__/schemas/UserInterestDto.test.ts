import { InterestDto } from "@reasn/common/src/schemas/InterestDto";
import {
  UserInterestDto,
  UserInterestDtoMapper,
} from "@reasn/common/src/schemas/UserInterestDto";
import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";

describe("UserInterestDto", () => {
  const interest = { name: "Interest" } as InterestDto;
  const level = 1;

  describe("fromJson", () => {
    it("should create an instance of UserInterestDto from JSON string", () => {
      const json = `{
                "interest": { "name": "${interest.name}" },
                "level": ${level}
            }`;

      let userInterest = UserInterestDtoMapper.fromJSON(json);
      userInterest = userInterest as UserInterestDto;

      expect(userInterest.interest.name).toBe(interest.name);
      expect(userInterest.level).toBe(level);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => UserInterestDtoMapper.fromJSON("")).toThrow(
        ModelMappingError,
      );
    });

    it("should throw an error when providing json without each property individaully", () => {
      const jsonWithoutInterest = `{
                "level": ${level}
            }`;

      const jsonWithoutLevel = `{
                "interest": { "Name": "${interest.name}" }
            }`;

      expect(() => UserInterestDtoMapper.fromJSON(jsonWithoutInterest)).toThrow(
        ModelMappingError,
      );
      expect(() => UserInterestDtoMapper.fromJSON(jsonWithoutLevel)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of UserInterestDto from an object", () => {
      const object = {
        interest: interest,
        level: level,
      };

      let userInterest = UserInterestDtoMapper.fromObject(object);
      userInterest = userInterest as UserInterestDto;

      expect(userInterest.interest.name).toBe(interest.name);
      expect(userInterest.level).toBe(level);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        interest: { name: 1 },
        level: true,
      };

      const objectWithoutInterest = {
        level: level,
      };

      const objectWithoutLevel = {
        interest: interest,
      };

      expect(() => UserInterestDtoMapper.fromObject(object)).toThrow(
        ModelMappingError,
      );
      expect(() =>
        UserInterestDtoMapper.fromObject(objectWithoutInterest),
      ).toThrow(ModelMappingError);
      expect(() =>
        UserInterestDtoMapper.fromObject(objectWithoutLevel),
      ).toThrow(ModelMappingError);
    });
  });
});
