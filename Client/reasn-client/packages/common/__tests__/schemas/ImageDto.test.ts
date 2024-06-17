import { ImageDto, ImageDtoMapper } from "@reasn/common/src/schemas/ImageDto";
import { ObjectType } from "@reasn/common/src/enums/modelsEnums";
import ModelMappingError from "@reasn/common/src/errors/ModelMappingError";

describe("ImageDto", () => {
  const imageData = "Test Image";
  const objectId = 1;
  const objectType = ObjectType.USER;

  describe("fromJson", () => {
    it("should create an instance of ImageDto from JSON string", () => {
      const json = `{
                "ImageData": "${imageData}",
                "ObjectId": ${objectId},
                "ObjectType": "${objectType}"
            }`;

      let image = ImageDtoMapper.fromJSON(json);
      image = image as ImageDto;

      expect(image.ImageData).toBe(imageData);
      expect(image.ObjectId).toBe(objectId);
      expect(image.ObjectType).toBe(objectType);
    });

    it("should return null if the JSON string is empty", () => {
      expect(() => ImageDtoMapper.fromJSON("")).toThrow(ModelMappingError);
    });

    it("should throw an error when providing json without each property individually", () => {
      const jsonWithoutImageData = `{
                "ObjectId": ${objectId},
                "ObjectType": "${objectType}"
            }`;

      const jsonWithoutObjectId = `{
                "ImageData": "${imageData}",
                "ObjectType": "${objectType}"
            }`;

      const jsonWithoutObjectTypeId = `{
                "ImageData": "${imageData}",
                "ObjectId": ${objectId}
            }`;

      expect(() => ImageDtoMapper.fromJSON(jsonWithoutImageData)).toThrow(
        ModelMappingError,
      );
      expect(() => ImageDtoMapper.fromJSON(jsonWithoutObjectId)).toThrow(
        ModelMappingError,
      );
      expect(() => ImageDtoMapper.fromJSON(jsonWithoutObjectTypeId)).toThrow(
        ModelMappingError,
      );
    });
  });

  describe("fromObject", () => {
    it("should create an instance of ImageDto from an object", () => {
      const object = {
        ImageData: imageData,
        ObjectId: objectId,
        ObjectType: objectType,
      };

      let image = ImageDtoMapper.fromObject(object);
      image = image as ImageDto;

      expect(image.ImageData).toBe(imageData);
      expect(image.ObjectId).toBe(objectId);
      expect(image.ObjectType).toBe(objectType);
    });

    it("should throw an error if the object is invalid", () => {
      const object = {
        ImageData: true,
        ObjectId: null,
        ObjectType: undefined,
      };

      const objectWithoutImageData = {
        ObjectId: objectId,
        ObjectType: objectType,
      };

      const objectWithoutObjectId = {
        ImageData: imageData,
        ObjectType: objectType,
      };

      const objectWithoutObjectType = {
        ImageData: imageData,
        ObjectId: objectId,
      };

      expect(() => ImageDtoMapper.fromObject(object)).toThrow(
        ModelMappingError,
      );
      expect(() => ImageDtoMapper.fromObject(objectWithoutImageData)).toThrow(
        ModelMappingError,
      );
      expect(() => ImageDtoMapper.fromObject(objectWithoutObjectId)).toThrow(
        ModelMappingError,
      );
      expect(() => ImageDtoMapper.fromObject(objectWithoutObjectType)).toThrow(
        ModelMappingError,
      );
    });
  });
});
