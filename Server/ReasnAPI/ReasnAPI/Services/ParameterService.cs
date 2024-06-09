using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ReasnAPI.Services;
public class ParameterService(ReasnContext context)
{
    public ParameterDto? CreateParameter(ParameterDto parameterDto)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Key == parameterDto.Key && r.Value == parameterDto.Value);
        if (parameter is not null)
        {
            return null;
        }

        var newParameter = MapParameterFromParameterDto(parameterDto);
        context.Parameters.Add(newParameter);
        context.SaveChanges();
        return parameterDto;
    }

    public ParameterDto? UpdateParameter(int parameterId, ParameterDto parameterDto)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Id == parameterId);

        var parameters = context.Events.Include(p => p.Parameters);

        var parameterCheck = parameters.FirstOrDefault(r => r.Parameters.Any(p => p.Id == parameterId));
            
        if (parameterCheck is not null || parameter is null) // if parameter is associated with an event, it cannot be updated
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

        if (parameter == null)
        {
            return false;
        }

        var eventsWithParameters = context.Events
            .Include(e => e.Parameters)
            .ToList();

        var parameterCheck = eventsWithParameters
            .Any(e => e.Parameters.Any(p => p.Id == parameterId));

        if (parameterCheck) 
        {
            return false;
        }

        context.Parameters.Remove(parameter);
        context.SaveChanges();
        return true;
    }

    public ParameterDto? GetParameterById(int parameterId)
    {
        var parameter = context.Parameters.Find(parameterId);
        if (parameter is null)
        {
            return null;
        }

        var parameterDto = MapParameterDtoFromParameter(parameter);

        return parameterDto;
    }

    public IEnumerable<ParameterDto> GetAllParameters()
    {
        var parameters = context.Parameters.ToList();

        return parameters.Select(parameter => MapParameterDtoFromParameter(parameter))
            .AsEnumerable();
    }

    public IEnumerable<ParameterDto> GetParametersByFilter(Expression<Func<Parameter, bool>> filter)
    {
        var parameters = context.Parameters.Where(filter).ToList();

        return parameters.Select(parameter => MapParameterDtoFromParameter(parameter))
            .AsEnumerable();
    }

    private Parameter MapParameterFromParameterDto(ParameterDto parameterDto)
    {
        return new Parameter
        {
            Key = parameterDto.Key,
            Value = parameterDto.Value
        };
    }

    private ParameterDto MapParameterDtoFromParameter(Parameter parameter)
    {
        return new ParameterDto
        {
            Key = parameter.Key,
            Value = parameter.Value
        };
    }

}