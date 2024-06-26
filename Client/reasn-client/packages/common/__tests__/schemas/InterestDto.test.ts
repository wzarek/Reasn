import {
  InterestDto,
  InterestDtoMapper,
} from "@reasn/common/src/schemas/InterestDto";
import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";

describe("InterestDto", () => {
  const name = "Interest";

  describe("fromJson", () => {
    it("should create an instance of InterestDto from JSON string", () => {
      const json = `{
                "name": "${name}"
            }`;

      let interest = InterestDtoMapper.fromJSON(json);
      interest = interest as InterestDto;

      expect(interest.name).toBe(name);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => InterestDtoMapper.fromJSON("")).toThrow(ModelMappingError);
    });

    it("should throw an error when providing json without property", () => {
      const jsonWithoutName = `{}`;

      expect(() => InterestDtoMapper.fromJSON(jsonWithoutName)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of InterestDto from an object", () => {
      const object = {
        name: name,
      };

      let interest = InterestDtoMapper.fromObject(object);
      interest = interest as InterestDto;

      expect(interest.name).toBe(name);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        Name: true,
      };

      expect(() => InterestDtoMapper.fromObject(object)).toThrow(
        ModelMappingError,
      );
    });
  });
});
