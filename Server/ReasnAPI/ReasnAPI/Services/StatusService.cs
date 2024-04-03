using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class StatusService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        /* TODO: Create following functions for this class
         * create
         * update
         * delete
         * get by ID
         * get list by filter
         * get all
         */

        public StatusDto CreateStatus(StatusDto statusDto)
        {
            var status = new Status
            {
                Name = statusDto.Name
            };

            _context.Statuses.Add(status);
            _context.SaveChanges();

            return statusDto;
        }

        public StatusDto UpdateStatus(int statusId,StatusDto statusDto)
        {
            var status = _context.Statuses.FirstOrDefault(r => r.Id == statusId);
            if(status == null)
            {
                return null;
            }

            status.Name = statusDto.Name;

            _context.Statuses.Update(status);
            _context.SaveChanges();

            return statusDto;
        }

        public void DeleteStatus(int statusId)
        {
            var status = _context.Statuses.FirstOrDefault(r => r.Id == statusId);
           

            _context.Statuses.Remove(status);
            _context.SaveChanges();

        }

        public StatusDto GetStatusById(int statusId)
        {
            var status = _context.Statuses.FirstOrDefault(r => r.Id == statusId);
            if(status == null)
            {
                return null;
            }

            return new StatusDto
            {
                Name = status.Name
            };
        }

        public List<StatusDto> GetAllStatuses()
        {
            var statuses = _context.Statuses.ToList();
            var statusDtos = new List<StatusDto>();
            foreach(var status in statuses)
            {
                statusDtos.Add(new StatusDto
                {
                    Name = status.Name
                });
            }

            return statusDtos;
        }

        public List<StatusDto> GetStatusesByFilter(Expression<Func<Status, bool>> filter)
        {
            var statuses = _context.Statuses.Where(filter).ToList();
            var statusDtos = new List<StatusDto>();
            foreach(var status in statuses)
            {
                statusDtos.Add(new StatusDto
                {
                    Name = status.Name
                });
            }

            return statusDtos;



        }

    }
}
