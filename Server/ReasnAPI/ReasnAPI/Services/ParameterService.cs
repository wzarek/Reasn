using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services;
public class ParameterService(ReasnContext context)
{
    public ParameterDto CreateParameter(ParameterDto parameterDto)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Key == parameterDto.Key && r.Value == parameterDto.Value);
        if (parameter != null)
        {
            return null;
        }

        var newParameter = new Parameter
        {
            Key = parameterDto.Key,
            Value = parameterDto.Value
        };
        context.Parameters.Add(newParameter);
        context.SaveChanges();
        return parameterDto;
    }

    public ParameterDto UpdateParameter(int parameterId, ParameterDto parameterDto)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Id == parameterId);

        var parameterCheck = context.EventParameters.FirstOrDefault(r => r.ParameterId == parameterId);
        if (parameterCheck != null) // if parameter is associated with an event, it cannot be updated
        {
            return null;
        }

        if (parameter == null)
        {
            return null;
        }
        parameter.Key = parameterDto.Key;
        parameter.Value = parameterDto.Value;
        context.Parameters.Update(parameter);
        context.SaveChanges();
        return parameterDto;
    }

    public bool DeleteParameter(int parameterId)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Id == parameterId);

        var parameterCheck = context.EventParameters.FirstOrDefault(r => r.ParameterId == parameterId);
        if (parameterCheck != null) // if parameter is associated with an event, it cannot be deleted
        {
            return false;
        }

        if (parameter == null)
        {
            return false;
        }
        context.Parameters.Remove(parameter);
        context.SaveChanges();
        return true;
    }

    public ParameterDto GetParameterById(int parameterId)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Id == parameterId);
        if (parameter == null)
        {
            return null;
        }

        var parameterDto = new ParameterDto
        {
            Key = parameter.Key,
            Value = parameter.Value
        };

        return parameterDto;
    }

    public IEnumerable<ParameterDto> GetAllParameters()
    {
        var parameters = context.Parameters.ToList();

        return parameters.Select(parameter => new ParameterDto { Key = parameter.Key, Value = parameter.Value }).ToList();
    }

    public IEnumerable<ParameterDto> GetParametersByFilter(Expression<Func<Parameter, bool>> filter)
    {
        var parameters = context.Parameters.Where(filter).ToList();

        return parameters.Select(parameter => new ParameterDto { Key = parameter.Key, Value = parameter.Value })
            .ToList();
    }

}