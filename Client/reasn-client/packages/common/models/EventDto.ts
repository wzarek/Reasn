import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { TagDto } from '@reasn/common/models/TagDto'

export class EventDto {
    Name: string
    AddressId: number
    Description: string
    OrganizerId: number
    StartAt: Date
    EndAt: Date
    CreatedAt: Date
    UpdatedAt: Date
    Slug: string | null
    StatusId: number
    Tags: TagDto[] | null

    constructor(name: string, addressId: number, description: string, organizerId: number, startAt: Date, endAt: Date, createdAt: Date, updatedAt: Date, slug: string, statusId: number, tags: TagDto[] | null) {
        this.Name = name
        this.AddressId = addressId
        this.Description = description
        this.OrganizerId = organizerId
        this.StartAt = startAt
        this.EndAt = endAt
        this.CreatedAt = createdAt
        this.UpdatedAt = updatedAt
        this.Slug = slug
        this.StatusId = statusId
        this.Tags = tags
    }

    static fromJson(json: string): EventDto | null {
        if (!json) {
            return null
        }

        return EventDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): EventDto | null {
        if (!obj) {
            return null
        }

        if ('Name' in obj === false || typeof obj.Name !== 'string') throw new ModelMappingError('EventDto','Name is required')
        if ('AddressId' in obj === false || typeof obj.AddressId !== 'number') throw new ModelMappingError('EventDto','AddressId is required')
        if ('Description' in obj === false || typeof obj.Description !== 'string') throw new ModelMappingError('EventDto','Description is required')
        if ('OrganizerId' in obj === false || typeof obj.OrganizerId !== 'number') throw new ModelMappingError('EventDto','OrganizerId is required')
        if (!('StartAt' in obj) || (typeof obj.StartAt !== 'string' && !(obj.StartAt instanceof Date))) throw new ModelMappingError('CommentDto','StartAt is required')
        if (typeof obj.StartAt === 'string' && isNaN(Date.parse(obj.StartAt))) throw new ModelMappingError('CommentDto','StartAt is invalid')
        if (!('EndAt' in obj) || (typeof obj.EndAt !== 'string' && !(obj.EndAt instanceof Date))) throw new ModelMappingError('CommentDto','EndAt is required')
        if (typeof obj.EndAt === 'string' && isNaN(Date.parse(obj.EndAt))) throw new ModelMappingError('CommentDto','EndAt is invalid')
        if (!('CreatedAt' in obj) || (typeof obj.CreatedAt !== 'string' && !(obj.CreatedAt instanceof Date))) throw new ModelMappingError('CommentDto','CreatedAt is required')
        if (typeof obj.CreatedAt === 'string' && isNaN(Date.parse(obj.CreatedAt))) throw new ModelMappingError('CommentDto','CreatedAt is invalid')
        if (!('UpdatedAt' in obj) || (typeof obj.UpdatedAt !== 'string' && !(obj.UpdatedAt instanceof Date))) throw new ModelMappingError('CommentDto','UpdatedAt is required')
        if (typeof obj.UpdatedAt === 'string' && isNaN(Date.parse(obj.UpdatedAt))) throw new ModelMappingError('CommentDto','UpdatedAt is invalid')
        if ('Slug' in obj === false || (typeof obj.Slug !== 'string')) throw new ModelMappingError('EventDto','Slug is required')
        if ('StatusId' in obj === false || typeof obj.StatusId !== 'number') throw new ModelMappingError('EventDto','StatusId is required')
        if ('Tags' in obj === false || obj.Tags === null || !Array.isArray(obj.Tags)) throw new ModelMappingError('EventDto','Tags is required')

        let tags: TagDto[] = obj.Tags.map((x: object) => TagDto.fromObject(x)).filter((x: TagDto | null) => x !== null) as TagDto[]

        if (typeof obj.StartAt === 'string') {
            obj.StartAt = new Date(obj.StartAt)
        }

        if (typeof obj.EndAt === 'string') {
            obj.EndAt = new Date(obj.EndAt)
        }

        if (typeof obj.CreatedAt === 'string') {
            obj.CreatedAt = new Date(obj.CreatedAt)
        }

        if (typeof obj.UpdatedAt === 'string') {
            obj.UpdatedAt = new Date(obj.UpdatedAt)
        }

        return new EventDto(obj.Name, obj.AddressId, obj.Description, obj.OrganizerId, obj.StartAt as Date, obj.EndAt as Date, obj.CreatedAt as Date, obj.UpdatedAt as Date, obj.Slug, obj.StatusId, tags)
    }
}