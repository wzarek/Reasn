using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq;
using System.Linq.Expressions;

namespace ReasnAPI.Services;
public class TagService (ReasnContext context)
{
    public TagDto CreateTag(TagDto tagDto)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Name == tagDto.Name);
        if (tag != null) return tagDto;

        var newTag = new Tag
        {
            Name = tagDto.Name
        };
        context.Tags.Add(newTag);
        context.SaveChanges();
        return tagDto;
    }

    public TagDto UpdateTag(int tagId,TagDto tagDto)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);
        if(tag == null)
        {
            return null;
        }
        tag.Name = tagDto.Name;
        context.Tags.Update(tag);
        context.SaveChanges();
        return tagDto;
    }   

    public void DeleteTag(int tagId)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);
        if (tag == null)
        {
            return;
        }
        context.Tags.Remove(tag);
        context.SaveChanges();
    }

    public TagDto GetTagById(int tagId)
    {
        var tag = context.Tags.FirstOrDefault(r => r.Id == tagId);
        if(tag == null)
        {
            return null;
        }

        return new TagDto
        {
            Name = tag.Name
        };
    }

    public IEnumerable<TagDto> GetAllTags()
    {
        var tags = context.Tags.ToList();
        return tags.Select(tag => new TagDto { Name = tag.Name }).ToList();
    }

    public IEnumerable<TagDto> GetTagsByFilter(Expression<Func<Tag, bool>> filter)
    {
        var tags = context.Tags.Where(filter).ToList();
        return tags.Select(tag => new TagDto { Name = tag.Name }).ToList();
    }

}
