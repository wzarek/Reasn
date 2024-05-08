import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { CommentDto, CommentDtoMapper } from '@reasn/common/models/CommentDto'

describe('CommentDto', () => {
    const eventId = 1
    const content = 'Test Content'
    const createdAt = new Date()
    const userId = 2

    describe('fromJson', () => {
        it('should create an instance of CommentDto from JSON string', () => {
            const json = `{
                "EventId": ${eventId},
                "Content": "${content}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UserId": ${userId}
            }`

            let comment = CommentDtoMapper.fromJSON(json)
            comment = comment as CommentDto

            expect(comment.EventId).toBe(eventId)
            expect(comment.Content).toBe(content)
            expect(comment.CreatedAt).toEqual(createdAt)
            expect(comment.UserId).toBe(userId)
        })

        it('should throw an error if the JSON string is empty', () => {
            expect(() => CommentDtoMapper.fromJSON('')).toThrow(ModelMappingError)
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

            expect(() => CommentDtoMapper.fromJSON(jsonWithoutEventId)).toThrow(ModelMappingError)
            expect(() => CommentDtoMapper.fromJSON(jsonWithoutContent)).toThrow(ModelMappingError)
            expect(() => CommentDtoMapper.fromJSON(jsonWithoutCreatedAt)).toThrow(ModelMappingError)
            expect(() => CommentDtoMapper.fromJSON(jsonWithoutUserId)).toThrow(ModelMappingError)
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

            let comment = CommentDtoMapper.fromObject(object)
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

            expect(() => CommentDtoMapper.fromObject(object)).toThrow(ModelMappingError)
            expect(() => CommentDtoMapper.fromObject(objectWithoutEventId)).toThrow(ModelMappingError)
            expect(() => CommentDtoMapper.fromObject(objectWithoutContent)).toThrow(ModelMappingError)
            expect(() => CommentDtoMapper.fromObject(objectWithoutCreatedAt)).toThrow(ModelMappingError)
            expect(() => CommentDtoMapper.fromObject(objectWithoutUserId)).toThrow(ModelMappingError)
        })
    })
})