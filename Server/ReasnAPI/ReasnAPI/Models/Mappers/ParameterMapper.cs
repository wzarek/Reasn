using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Models.Mappers
{
    public static class ParameterMapper
    {
        public static ParameterDto ToDto(this Parameter parameter)
        {
            return new ParameterDto
            {
                Key = parameter.Key,
                Value = parameter.Value
            };
        }
        public static List<ParameterDto> ToDtoList(this IEnumerable<Parameter> parameter)
        {
            return parameter.Select(ToDto).ToList();
        }
        public static Parameter ToEntity(this ParameterDto parameterDto)
        {
            return new Parameter
            {
                Key = parameterDto.Key,
                Value = parameterDto.Value
            };
        }

    }
}
