import { InterestDto } from "@reasn/common/src/schemas/InterestDto";
import {
  UserInterestDto,
  UserInterestDtoMapper,
} from "@reasn/common/src/schemas/UserInterestDto";
import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";

describe("UserInterestDto", () => {
  const interest = { Name: "Interest" } as InterestDto;
  const level = 1;

  describe("fromJson", () => {
    it("should create an instance of UserInterestDto from JSON string", () => {
      const json = `{
                "Interest": { "Name": "${interest.Name}" },
                "Level": ${level}
            }`;

      let userInterest = UserInterestDtoMapper.fromJSON(json);
      userInterest = userInterest as UserInterestDto;

      expect(userInterest.Interest.Name).toBe(interest.Name);
      expect(userInterest.Level).toBe(level);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => UserInterestDtoMapper.fromJSON("")).toThrow(
        ModelMappingError,
      );
    });

    it("should throw an error when providing json without each property individaully", () => {
      const jsonWithoutInterest = `{
                "Level": ${level}
            }`;

      const jsonWithoutLevel = `{
                "Interest": { "Name": "${interest.Name}" }
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
        Interest: interest,
        Level: level,
      };

      let userInterest = UserInterestDtoMapper.fromObject(object);
      userInterest = userInterest as UserInterestDto;

      expect(userInterest.Interest.Name).toBe(interest.Name);
      expect(userInterest.Level).toBe(level);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        Interest: { Name: 1 },
        Level: true,
      };

      const objectWithoutInterest = {
        Level: level,
      };

      const objectWithoutLevel = {
        Interest: interest,
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
