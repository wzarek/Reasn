using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services;
public class ParameterService (ReasnContext context)
{
    public ParameterDto CreateParameter(ParameterDto parameterDto)
    {
        var parameter = new Parameter
        {
            Key = parameterDto.Key,
            Value = parameterDto.Value
        };
        context.Parameters.Add(parameter);
        context.SaveChanges();
        return parameterDto;
    }

    public ParameterDto UpdateParameter(int parameterId,ParameterDto parameterDto)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Id == parameterId);
        
        if(parameter == null)
        {
            return null;
        }
        parameter.Key = parameterDto.Key;
        parameter.Value = parameterDto.Value;
        context.Parameters.Update(parameter);
        context.SaveChanges();
        return parameterDto;
    }

    public void DeleteParameter(int parameterId)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Id == parameterId);
        if (parameter == null)
        {
            return;
        }
        context.Parameters.Remove(parameter);
        context.SaveChanges();
    }

    public ParameterDto GetParameterById(int parameterId)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Id == parameterId);
        if(parameter == null)
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

        return parameters.Select(parameter => new ParameterDto { Key = parameter.Key, Value = parameter.Value }).ToList();

    }

}

