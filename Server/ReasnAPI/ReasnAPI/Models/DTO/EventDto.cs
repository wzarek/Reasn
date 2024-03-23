using System;

namespace ReasnAPI.Models.DTO {
    public class EventDto {
        public string Name { get; set; }
        public int AddressId { get; set; }
        public string Description { get; set; }
        public int OrganizerId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Slug { get; set; }
        public int StatusId { get; set; }
    }
}