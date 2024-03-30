using Microsoft.EntityFrameworkCore;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Services {
    public class EventService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        /* TODO: Create following functions for this class
         * create (if one eventtag failed stoped the process)
         * update
         * delete
         * get by ID
         * get list by filter
         * get all
         */

        public EventDto CreateEvent(EventDto eventDto)
        {

            var newEvent = new Event
            {
                Name = eventDto.Name,
                AddressId = eventDto.AddressId,                
                Description = eventDto.Description,
                OrganizerId = eventDto.OrganizerId,
                StartAt = eventDto.StartAt,
                EndAt = eventDto.EndAt,
                CreatedAt = eventDto.CreatedAt,
                UpdatedAt = eventDto.UpdatedAt,
                Slug = eventDto.Slug,
                StatusId = eventDto.StatusId,
               
            };
            _context.Events.Add(newEvent);
            _context.SaveChanges();
            var tempEvent = _context.Events.FirstOrDefault(r => r.Slug == eventDto.Slug);
            int eventId = tempEvent.Id;

            foreach (var tag in eventDto.Tags)
            {
                var tempTag = _context.Tags.FirstOrDefault(r => r.Name == tag.Name);
                if(tempTag == null)
                {
                    var newTag = new Tag
                    {
                        Name = tag.Name
                    };
                    _context.Tags.Add(newTag);
                    _context.SaveChanges();
                    tempTag = _context.Tags.FirstOrDefault(r => r.Name == tag.Name);
                }

                var eventTag = new EventTag
                {
                    EventId = eventId,
                    TagId = tempTag.Id
                };
                _context.EventTags.Add(eventTag);
                _context.SaveChanges();
            }

               return eventDto;

        }

        public EventDto UpdateEvent(int eventId, EventDto eventDto)
        {
            var eventToUpdate = _context.Events.FirstOrDefault(r => r.Id == eventId);
            if(eventToUpdate == null)
            {
                return null;
            }

            eventToUpdate.Name = eventDto.Name;
            eventToUpdate.AddressId = eventDto.AddressId;
            eventToUpdate.Description = eventDto.Description;
            eventToUpdate.OrganizerId = eventDto.OrganizerId;
            eventToUpdate.StartAt = eventDto.StartAt;
            eventToUpdate.EndAt = eventDto.EndAt;
            eventToUpdate.CreatedAt = eventDto.CreatedAt;
            eventToUpdate.UpdatedAt = eventDto.UpdatedAt;
            eventToUpdate.Slug = eventDto.Slug;
            eventToUpdate.StatusId = eventDto.StatusId;

            _context.Events.Update(eventToUpdate);
            _context.SaveChanges();


            var existingTags = _context.EventTags.Where(et => et.EventId == eventId).ToList();

            var newTags = eventDto.Tags.Select(tagDto => new EventTag
            {
                Tag = new Tag
                { 
                    Name = tagDto.Name,
                }
            }).ToList();

            foreach (var existingTag in existingTags)
            {
                if (!newTags.Any(t => t.Tag == existingTag.Tag))
                {
                    _context.EventTags.Remove(existingTag);
                }
            }

            foreach (var newTag in newTags)
            {
                if (!existingTags.Any(t => t.Tag == newTag.Tag))
                {
                    _context.EventTags.Add(new EventTag { EventId = eventId, Tag = newTag.Tag });
                }
            }

            return eventDto;
        }




    }
}
