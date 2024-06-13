using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;
using ReasnAPI.Mappers;

namespace ReasnAPI.Services;
public class ParameterService(ReasnContext context)
{
    public ParameterDto CreateParameter(ParameterDto parameterDto)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Key == parameterDto.Key && r.Value == parameterDto.Value);
        if (parameter is not null)
        {
            throw new BadRequestException("Parameter already exists");
        }

        var newParameter = parameterDto.ToEntity();
        context.Parameters.Add(newParameter);
        context.SaveChanges();
        return parameterDto;
    }

    public ParameterDto UpdateParameter(int parameterId, ParameterDto parameterDto)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Id == parameterId);

        if (parameter is null)
        {
            throw new NotFoundException("Parameter not found");
        }

        var parameters = context.Events.Include(p => p.Parameters);

        var parameterCheck = parameters.FirstOrDefault(r => r.Parameters.Any(p => p.Id == parameterId));

        if (parameterCheck is not null) 
        {
            throw new BadRequestException("Parameter is associated with an event");
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
            throw new NotFoundException("Parameter not found");
        }

        var eventsWithParameters = context.Events
            .Include(e => e.Parameters)
            .ToList();

        var parameterCheck = eventsWithParameters
            .Any(e => e.Parameters.Any(p => p.Id == parameterId));

        if (parameterCheck)
        {
            throw new BadRequestException("Parameter is associated with an event");
        }

        context.Parameters.Remove(parameter);
        context.SaveChanges();
    }

    public void ForceDeleteParameter(ParameterDto parameterDto)
    {
        var parameter = context.Parameters.FirstOrDefault(r => r.Key == parameterDto.Key && r.Value == parameterDto.Value);
        if (parameter is null)
        {
            throw new NotFoundException("Parameter not found");
        }

        var eventsWithParameter = context.Events
            .Where(e => e.Parameters.Any(p => p.Key == parameterDto.Key && p.Value == parameterDto.Value))
            .Include(e => e.Parameters)
            .ToList();

        foreach (var eventWithParameter in eventsWithParameter)
        {
            eventWithParameter.Parameters.Remove(parameter);
        }

        context.Parameters.Remove(parameter);
        context.SaveChanges();
    }

    public void RemoveParametersNotInAnyEvent()
    {
        var parametersNotInAnyEvent = context.Parameters
            .Where(p => !context.Events.Any(e => e.Parameters.Contains(p)))
            .ToList();

        context.Parameters.RemoveRange(parametersNotInAnyEvent);
        context.SaveChanges();
    }

    public void AddParametersFromList(List<Parameter> parametersToAdd)
    {
        var existingParamsInDb = context.Parameters
            .Where(param => parametersToAdd.Any(newParam => newParam.Key == param.Key && newParam.Value == param.Value))
            .ToList();

        var paramsToAdd = parametersToAdd
            .Where(newParam => existingParamsInDb.All(existingParam => existingParam.Key != newParam.Key || existingParam.Value != newParam.Value))
            .ToList();

        context.Parameters.AddRange(paramsToAdd);
        context.SaveChanges();
    }

    public ParameterDto GetParameterById(int parameterId)
    {
        var parameter = context.Parameters.Find(parameterId);
        if (parameter is null)
        {
            throw new NotFoundException("Parameter not found");
        }

        var parameterDto = parameter.ToDto();
        return parameterDto;
    }

    public IEnumerable<ParameterDto> GetAllParameters()
    {
        return context.Parameters
            .ToDtoList()
            .AsEnumerable();
    }

    public IEnumerable<ParameterDto> GetParametersByFilter(Expression<Func<Parameter, bool>> filter)
    {
        return context.Parameters
            .Where(filter)
            .ToDtoList()
            .AsEnumerable();
    }


}