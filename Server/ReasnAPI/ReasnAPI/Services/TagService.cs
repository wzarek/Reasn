using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Services {
    public class TagService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        /* TODO: Create following functions for this class
         * create (cheacking if already exist if not add)
         * update
         * delete
         * get by ID
         * get list by filter
         * get all
         */

        public TagDto CreateTag(TagDto tagDto)
        {
            var tag = _context.Tags.FirstOrDefault(r => r.Name == tagDto.Name);
            if (tag == null)
            {
                var newTag = new Tag
                {
                    Name = tagDto.Name
                };

                _context.Tags.Add(newTag);
                _context.SaveChanges();
            }
            

            return tagDto;
        }
        

        public TagDto UpdateTag(int tagId,TagDto tagDto)
        {
            var tag = _context.Tags.FirstOrDefault(r => r.Id == tagId);
            if(tag == null)
            {
                return null;
            }
            tag.Name = tagDto.Name;

            _context.Tags.Update(tag);
            _context.SaveChanges();

            return tagDto;
        }   

        public bool DeleteTag(int tagId)
        {
            var tag = _context.Tags.FirstOrDefault(r => r.Id == tagId);
            if(tag == null)
            {
                return false;
            }

            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return true;
        }

        public TagDto GetTagById(int tagId)
        {
            var tag = _context.Tags.FirstOrDefault(r => r.Id == tagId);
            if(tag == null)
            {
                return null;
            }

            return new TagDto
            {
                Name = tag.Name
            };
        }

        public List<TagDto> GetAllTags()
        {
            var tags = _context.Tags.ToList();
            var tagDtos = new List<TagDto>();
            foreach(var tag in tags)
            {
                tagDtos.Add(new TagDto
                {
                    Name = tag.Name
                });
            }

            return tagDtos;
        }

        public List<TagDto> GetTagsByFilter(string filter)
        {
            var tags = _context.Tags.Where(r => r.Name.Contains(filter)).ToList();
            var tagDtos = new List<TagDto>();
            foreach(var tag in tags)
            {
                tagDtos.Add(new TagDto
                {
                    Name = tag.Name
                });
            }

            return tagDtos;
        }

    }
}
