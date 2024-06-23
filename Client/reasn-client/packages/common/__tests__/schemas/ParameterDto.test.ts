import {
  ParameterDto,
  ParameterDtoMapper,
} from "@reasn/common/src/schemas/ParameterDto";
import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";

describe("ParameterDto", () => {
  const key = "Test Key";
  const value = "Test Value";

  describe("fromJson", () => {
    it("should create an instance of ParameterDto from JSON string", () => {
      const json = `{
                "key": "${key}",
                "value": "${value}"
            }`;

      let parameter = ParameterDtoMapper.fromJSON(json);
      parameter = parameter as ParameterDto;

      expect(parameter.key).toBe(key);
      expect(parameter.value).toBe(value);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => ParameterDtoMapper.fromJSON("")).toThrow(ModelMappingError);
    });

    it("should throw an error when providing json without each property individually", () => {
      const jsonWithoutKey = `{
                "value": "${value}"
            }`;

      const jsonWithoutValue = `{
                "key": "${key}"
            }`;

      expect(() => ParameterDtoMapper.fromJSON(jsonWithoutKey)).toThrow(
        ModelMappingError,
      );
      expect(() => ParameterDtoMapper.fromJSON(jsonWithoutValue)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of ParameterDto from an object", () => {
      const object = {
        key: key,
        value: value,
      };

      let parameter = ParameterDtoMapper.fromObject(object);
      parameter = parameter as ParameterDto;

      expect(parameter.key).toBe(key);
      expect(parameter.value).toBe(value);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        key: true,
        value: null,
      };

      const objectWithoutKey = {
        value: value,
      };

      const objectWithoutValue = {
        key: key,
      };

      expect(() => ParameterDtoMapper.fromObject(object)).toThrow(
        ModelMappingError,
      );
      expect(() => ParameterDtoMapper.fromObject(objectWithoutKey)).toThrow(
        ModelMappingError,
      );
      expect(() => ParameterDtoMapper.fromObject(objectWithoutValue)).toThrow(
        ModelMappingError,
      );
    });
  });
});
