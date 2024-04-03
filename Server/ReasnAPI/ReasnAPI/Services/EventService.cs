using Microsoft.EntityFrameworkCore;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class EventService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        /* TODO: Create following functions for this class
         * create (if one eventtag failed stoped the process) 
         * update (with eventtags)
         * delete   (with eventtags) 
         * get by ID (with eventtags) 
         * get list by filter (with eventtags) 
         * get all (with eventtags) 
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
            if(eventDto.Tags != null)
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


            var existingTags = _context.EventTags.Where(r => r.EventId == eventId).ToList();

            var newTags = eventDto.Tags.Select(tagDto => new EventTag
            {
                Tag = new Tag
                { 
                    Name = tagDto.Name,
                }
            }).ToList();

            foreach (var existingTag in existingTags)
            {
                if (!newTags.Any(r => r.TagId == existingTag.TagId))
                {
                    _context.EventTags.Remove(existingTag);
                }
            }

            foreach (var newTag in newTags)
            {
                if (!existingTags.Any(r => r.TagId == newTag.TagId))
                {
                    _context.EventTags.Add(new EventTag { EventId = eventId, TagId = newTag.TagId });
                }
            }

            return eventDto;
        }
        
        public void DeleteEvent(int eventId)
        {
            var eventToDelete = _context.Events.FirstOrDefault(r => r.Id == eventId);
           

            _context.EventTags.RemoveRange(_context.EventTags.Where(r => r.EventId == eventId));
            _context.Events.Remove(eventToDelete);

            _context.SaveChanges();
          
        }

        public EventDto GetEventById(int eventId)
        {
            var eventToReturn = _context.Events.FirstOrDefault(r => r.Id == eventId);
            if(eventToReturn == null)
            {
                return null;
            }

            var eventTags = _context.EventTags.Where(r => r.EventId == eventId).ToList();
            var tags = new List<TagDto>();
            foreach (var eventTag in eventTags)
            {
                var tag = _context.Tags.FirstOrDefault(r => r.Id == eventTag.TagId);
                tags.Add(new TagDto { Name = tag.Name });
            }

            var eventDto = new EventDto
            {
                Name = eventToReturn.Name,
                AddressId = eventToReturn.AddressId,
                Description = eventToReturn.Description,
                OrganizerId = eventToReturn.OrganizerId,
                StartAt = eventToReturn.StartAt,
                EndAt = eventToReturn.EndAt,
                CreatedAt = eventToReturn.CreatedAt,
                UpdatedAt = eventToReturn.UpdatedAt,
                Slug = eventToReturn.Slug,
                StatusId = eventToReturn.StatusId,
                Tags = tags
            };

            return eventDto;
        }

        public List<EventDto> GetEventsByFilter(Expression<Func<Event, bool>> filter)
        {
            var events = _context.Events.Where(filter).ToList();
            var eventDtos = new List<EventDto>();
            foreach (var eventToReturn in events)
            {
                var eventTags = _context.EventTags.Where(r => r.EventId == eventToReturn.Id).ToList();
                var tags = new List<TagDto>();
                foreach (var eventTag in eventTags)
                {
                    var tag = _context.Tags.FirstOrDefault(r => r.Id == eventTag.TagId);
                    tags.Add(new TagDto { Name = tag.Name });
                }

                var eventDto = new EventDto
                {
                    Name = eventToReturn.Name,
                    AddressId = eventToReturn.AddressId,
                    Description = eventToReturn.Description,
                    OrganizerId = eventToReturn.OrganizerId,
                    StartAt = eventToReturn.StartAt,
                    EndAt = eventToReturn.EndAt,
                    CreatedAt = eventToReturn.CreatedAt,
                    UpdatedAt = eventToReturn.UpdatedAt,
                    Slug = eventToReturn.Slug,
                    StatusId = eventToReturn.StatusId,
                    Tags = tags
                };

                eventDtos.Add(eventDto);
            }

            return eventDtos;
        }

        public List<EventDto> GetAllEvents()
        {
            var events = _context.Events.ToList();
            var eventDtos = new List<EventDto>();
            foreach (var eventToReturn in events)
            {
                var eventTags = _context.EventTags.Where(r => r.EventId == eventToReturn.Id).ToList();
                var tags = new List<TagDto>();
                foreach (var eventTag in eventTags)
                {
                    var tag = _context.Tags.FirstOrDefault(r => r.Id == eventTag.TagId);
                    tags.Add(new TagDto { Name = tag.Name });
                }

                var eventDto = new EventDto
                {
                    Name = eventToReturn.Name,
                    AddressId = eventToReturn.AddressId,
                    Description = eventToReturn.Description,
                    OrganizerId = eventToReturn.OrganizerId,
                    StartAt = eventToReturn.StartAt,
                    EndAt = eventToReturn.EndAt,
                    CreatedAt = eventToReturn.CreatedAt,
                    UpdatedAt = eventToReturn.UpdatedAt,
                    Slug = eventToReturn.Slug,
                    StatusId = eventToReturn.StatusId,
                    Tags = tags
                };

                eventDtos.Add(eventDto);
            }

            return eventDtos;
        }
        


    }
}
