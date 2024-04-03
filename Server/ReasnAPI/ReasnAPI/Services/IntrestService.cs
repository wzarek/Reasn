using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class IntrestService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        /* TODO: Create following functions for this class
         * create
         * update
         * delete
         * get by ID
         * get list by filter
         * get all
         */

        public IntrestDto CreateIntrest(IntrestDto intrestDto)
        {
            var newIntrest = new Interest
            {
                Name = intrestDto.Name
            };

            _context.Interests.Add(newIntrest);
            _context.SaveChanges();
            return intrestDto;
        }


        public IntrestDto UpdateIntrest(int intrestId,IntrestDto intrestDto)
        {
            var intrest = _context.Interests.FirstOrDefault(r => r.Id == intrestId);
            if (intrest == null)
            {
                return null;
            }

            intrest.Name = intrestDto.Name;

            _context.Interests.Update(intrest);
            _context.SaveChanges();
            return intrestDto;
        }

        public void DeleteIntrest(int id)
        {
            var intrest = _context.Interests.Find(id);
        
            _context.Interests.Remove(intrest);
            _context.SaveChanges();
          
        }

        public IntrestDto GetIntrestById(int intrestId)
        {
            var intrest = _context.Interests.Find(intrestId);
            if (intrest == null)
            {
                return null;
            }

            var intrestDto = new IntrestDto
            {
                Name = intrest.Name
            };

            return intrestDto;
        }


        public List<IntrestDto> GetAllIntrests()
        {
            var intrests = _context.Interests.ToList();
            var intrestDtos = new List<IntrestDto>();
            foreach (var intrest in intrests)
            {
                intrestDtos.Add(new IntrestDto
                {
                    Name = intrest.Name
                });
            }

            return intrestDtos;
        }

        public List<IntrestDto> GetIntrestsByFilter(Expression<Func<Interest, bool>> filter)
        {
            var intrests = _context.Interests.Where(filter).ToList();
            if (intrests == null)
            {
                return null;
            }

            var intrestDtos = intrests.Select(intrest => new IntrestDto
            {
                Name = intrest.Name
            }).ToList();

            return intrestDtos;
        }
        
       


    }


}
