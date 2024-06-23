import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";
import { TagDto, TagDtoMapper } from "@reasn/common/src/schemas/TagDto";

describe("TagDto", () => {
  const name = "tag name";

  describe("fromJson", () => {
    it("should create an instance of TagDto from JSON string", () => {
      const json = `{
                "name": "${name}"
            }`;

      let tag = TagDtoMapper.fromJSON(json);
      tag = tag as TagDto;

      expect(tag.name).toBe(name);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => TagDtoMapper.fromJSON("")).toThrow(ModelMappingError);
    });

    it("should throw an error when providing JSON without the Name property", () => {
      const jsonWithoutName = `{}`;

      expect(() => TagDtoMapper.fromJSON(jsonWithoutName)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of TagDto from an object", () => {
      const object = {
        name: name,
      };

      let tag = TagDtoMapper.fromObject(object);
      tag = tag as TagDto;

      expect(tag.name).toBe(name);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        name: null,
      };

      const objectWithoutName = {};

      expect(() => TagDtoMapper.fromObject(object)).toThrow(ModelMappingError);
      expect(() => TagDtoMapper.fromObject(objectWithoutName)).toThrow(
        ModelMappingError,
      );
    });
  });
});
