﻿using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class ParameterService (ReasnContext context){
        private readonly ReasnContext _context = context;

        public ParameterDto CreateParameter(ParameterDto parameterDto)
        {
            var parameter = new Parameter
            {
                Key = parameterDto.Key,
                Value = parameterDto.Value
            };
            _context.Parameters.Add(parameter);
            _context.SaveChanges();
            return parameterDto;
        }

        public ParameterDto UpdateParameter(int parameterId,ParameterDto parameterDto)
        {
            var parameter = _context.Parameters.FirstOrDefault(r => r.Id == parameterId);
            
            if(parameter == null)
            {
                return null;
            }
            parameter.Key = parameterDto.Key;
            parameter.Value = parameterDto.Value;
            _context.Parameters.Update(parameter);
            _context.SaveChanges();
            return parameterDto;
        }

        public void DeleteParameter(int parameterId)
        {
            var parameter = _context.Parameters.FirstOrDefault(r => r.Id == parameterId);
            if (parameter == null)
            {
                return;
            }
            _context.Parameters.Remove(parameter);
            _context.SaveChanges();
        }

        public ParameterDto GetParameterById(int parameterId)
        {
            var parameter = _context.Parameters.FirstOrDefault(r => r.Id == parameterId);
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
            var parameters = _context.Parameters.ToList();

            return parameters.Select(parameter => new ParameterDto { Key = parameter.Key, Value = parameter.Value }).ToList();
        }

       public IEnumerable<ParameterDto> GetParametersByFilter(Expression<Func<Parameter, bool>> filter)
        {
            var parameters = _context.Parameters.Where(filter).ToList();

            return parameters.Select(parameter => new ParameterDto { Key = parameter.Key, Value = parameter.Value }).ToList();

        }

    }
}