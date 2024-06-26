﻿using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Mappers
{
    public static class TagMapper
    {
        public static TagDto ToDto(this Tag tag)
        {
            return new TagDto
            {
                Name = tag.Name
            };
        }

        public static List<TagDto> ToDtoList(this IEnumerable<Tag> tags)
        {
            return tags.Select(ToDto).ToList();
        }

        public static List<Tag> ToEntityList(this IEnumerable<TagDto> tags)
        {
            return tags.Select(ToEntity).ToList();
        }

        public static Tag ToEntity(this TagDto tagDto)
        {
            return new Tag
            {
                Name = tagDto.Name
            };
        }
    }
}
