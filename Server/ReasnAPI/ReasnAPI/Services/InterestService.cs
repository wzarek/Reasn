using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class InterestService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        public InterestDto CreateInterest(InterestDto interestDto)
        {
            var newInterest = new Interest
            {
                Name = interestDto.Name
            };

            _context.Interests.Add(newInterest);
            _context.SaveChanges();
            return interestDto;
        }

        public InterestDto UpdateInterest(int interestId,InterestDto interestDto)
        {
            var interest = _context.Interests.FirstOrDefault(r => r.Id == interestId);
            if (interest == null)
            {
                return null;
            }

            interest.Name = interestDto.Name;

            _context.Interests.Update(interest);
            _context.SaveChanges();
            return interestDto;
        }

        public void DeleteInterest(int id)
        {
            var interest = _context.Interests.Find(id);
        
            _context.Interests.Remove(interest);
            _context.SaveChanges();
          
        }

        public InterestDto GetInterestById(int interestId)
        {
            var interest = _context.Interests.Find(interestId);
            if (interest == null)
            {
                return null;
            }

            var interestDto = new InterestDto
            {
                Name = interest.Name
            };

            return intrestDto;
        }

        public List<InterestDto> GetAllInterests()
        {
            var interests = _context.Interests.ToList();
            var interestDtos = new List<InterestDto>();
            foreach (var intrest in interests)
            {
                interestDtos.Add(new InterestDto
                {
                    Name = intrest.Name
                });
            }

            return interestDtos;
        }

        public List<InterestDto> GetInterestsByFilter(Expression<Func<Interest, bool>> filter)
        {
            var interests = _context.Interests.Where(filter).ToList();
            
            var interestDtos = interests.Select(interest => new InterestDto
            {
                Name = interest.Name
            }).ToList();

            return interestDtos;
        }

    }
}
