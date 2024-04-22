import ModelMappingError from '@reasn/common/errors/ModelMappingError'

export class CommentDto {
    EventId: number
    Content: string
    CreatedAt: Date
    UserId: number

    constructor(eventId: number, content: string, createdAt: Date, userId: number) {
        this.EventId = eventId
        this.Content = content
        this.CreatedAt = createdAt
        this.UserId = userId
    }

    static fromJson(json: string): CommentDto | null {
        if (!json) {
            return null
        }

        return CommentDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): CommentDto | null {
        if (!obj) {
            return null
        }

        if (!('EventId' in obj) || typeof obj.EventId !== 'number') throw new ModelMappingError('CommentDto','EventId is required')
        if (!('Content' in obj) || typeof obj.Content !== 'string') throw new ModelMappingError('CommentDto','Content is required')
        if (!('CreatedAt' in obj) || (typeof obj.CreatedAt !== 'string' && !(obj.CreatedAt instanceof Date))) throw new ModelMappingError('CommentDto','CreatedAt is required')
        if (typeof obj.CreatedAt === 'string' && isNaN(Date.parse(obj.CreatedAt))) throw new ModelMappingError('CommentDto','CreatedAt is invalid')
        if (!('UserId' in obj) || typeof obj.UserId !== 'number') throw new ModelMappingError('CommentDto','UserId is required')

        if (typeof obj.CreatedAt === 'string') {
            obj.CreatedAt = new Date(obj.CreatedAt)
        }

        return new CommentDto(obj.EventId, obj.Content, obj.CreatedAt as Date, obj.UserId)
    }
}