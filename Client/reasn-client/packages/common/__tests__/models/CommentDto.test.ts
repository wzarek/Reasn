import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { CommentDto } from '@reasn/common/models/CommentDto'

describe('CommentDto', () => {
    const eventId = 1
    const content = 'Test Content'
    const createdAt = new Date()
    const userId = 2

    describe('constructor', () => {
        it('should create an instance of CommentDto', () => {
            const comment = new CommentDto(eventId, content, createdAt, userId)

            expect(comment).toBeInstanceOf(CommentDto)
            expect(comment.EventId).toBe(eventId)
            expect(comment.Content).toBe(content)
            expect(comment.CreatedAt).toBe(createdAt)
            expect(comment.UserId).toBe(userId)
        })
    })

    describe('fromJson', () => {
        it('should create an instance of CommentDto from JSON string', () => {
            const json = `{
                "EventId": ${eventId},
                "Content": "${content}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UserId": ${userId}
            }`

            let comment = CommentDto.fromJson(json)

            expect(comment).toBeInstanceOf(CommentDto)
            comment = comment as CommentDto

            expect(comment.EventId).toBe(eventId)
            expect(comment.Content).toBe(content)
            expect(comment.CreatedAt).toEqual(createdAt)
            expect(comment.UserId).toBe(userId)
        })

        it('should return null if the JSON string is empty', () => {
            const json = ''

            const comment = CommentDto.fromJson(json)

            expect(comment).toBeNull()
        })

        it('should throw an error when providing JSON without each property individually', () => {
            const jsonWithoutEventId = `{
                "Content": "${content}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UserId": ${userId}
            }`

            const jsonWithoutContent = `{
                "EventId": ${eventId},
                "CreatedAt": "${createdAt.toISOString()}",
                "UserId": ${userId}
            }`

            const jsonWithoutCreatedAt = `{
                "EventId": ${eventId},
                "Content": "${content}",
                "UserId": ${userId}
            }`

            const jsonWithoutUserId = `{
                "EventId": ${eventId},
                "Content": "${content}",
                "CreatedAt": "${createdAt.toISOString()}"
            }`

            expect(() => CommentDto.fromJson(jsonWithoutEventId)).toThrow(ModelMappingError)
            expect(() => CommentDto.fromJson(jsonWithoutContent)).toThrow(ModelMappingError)
            expect(() => CommentDto.fromJson(jsonWithoutCreatedAt)).toThrow(ModelMappingError)
            expect(() => CommentDto.fromJson(jsonWithoutUserId)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of CommentDto from an object', () => {
            const object = {
                EventId: eventId,
                Content: content,
                CreatedAt: createdAt,
                UserId: userId
            }

            let comment = CommentDto.fromObject(object)

            expect(comment).toBeInstanceOf(CommentDto)
            comment = comment as CommentDto

            expect(comment.EventId).toBe(eventId)
            expect(comment.Content).toBe(content)
            expect(comment.CreatedAt).toEqual(createdAt)
            expect(comment.UserId).toBe(userId)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                EventId: eventId,
                Content: true,
                CreatedAt: createdAt,
                UserId: null
            }

            const objectWithoutEventId = {
                Content: content,
                CreatedAt: createdAt,
                UserId: userId
            }

            const objectWithoutContent = {
                EventId: eventId,
                CreatedAt: createdAt,
                UserId: userId
            }

            const objectWithoutCreatedAt = {
                EventId: eventId,
                Content: content,
                UserId: userId
            }

            const objectWithoutUserId = {
                EventId: eventId,
                Content: content,
                CreatedAt: createdAt
            }

            expect(() => CommentDto.fromObject(object)).toThrow(ModelMappingError)
            expect(() => CommentDto.fromObject(objectWithoutEventId)).toThrow(ModelMappingError)
            expect(() => CommentDto.fromObject(objectWithoutContent)).toThrow(ModelMappingError)
            expect(() => CommentDto.fromObject(objectWithoutCreatedAt)).toThrow(ModelMappingError)
            expect(() => CommentDto.fromObject(objectWithoutUserId)).toThrow(ModelMappingError)
        })
    })
})