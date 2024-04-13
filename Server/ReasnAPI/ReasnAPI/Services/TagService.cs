using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class TagService (ReasnContext context) {
        private readonly ReasnContext _context = context;
        
        public TagDto CreateTag(TagDto tagDto)
        {
            var tag = _context.Tags.FirstOrDefault(r => r.Name == tagDto.Name);
            if (tag != null) return tagDto;

            var newTag = new Tag
            {
                Name = tagDto.Name
            };

            _context.Tags.Add(newTag);
            _context.SaveChanges();


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

        public void DeleteTag(int tagId)
        {
            var tag = _context.Tags.FirstOrDefault(r => r.Id == tagId);
            if (tag == null)
            {
                return;
            }
            _context.Tags.Remove(tag);
            _context.SaveChanges();
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

        public List<TagDto> GetTagsByFilter(Expression<Func<Tag, bool>> filter)
        {
            var tags = _context.Tags.Where(filter).ToList();
            var tagsDtos = new List<TagDto>();
            foreach (var tag in tags)
            {
                tagsDtos.Add(new TagDto
                {
                    Name = tag.Name
                });
            }

            return tagsDtos;
        }

    }
}
