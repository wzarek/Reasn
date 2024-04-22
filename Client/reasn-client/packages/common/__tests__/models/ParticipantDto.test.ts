import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { ParticipantDto } from '@reasn/common/models/ParticipantDto'

describe('ParticipantDto', () => {
    const eventId = 1
    const userId = 2
    const statusId = 3

    describe('constructor', () => {
        it('should create an instance of ParticipantDto', () => {
            const participant = new ParticipantDto(eventId, userId, statusId)

            expect(participant).toBeInstanceOf(ParticipantDto)
            expect(participant.EventId).toBe(eventId)
            expect(participant.UserId).toBe(userId)
            expect(participant.StatusId).toBe(statusId)
        })
    })

    describe('fromJson', () => {
        it('should create an instance of ParticipantDto from JSON string', () => {
            const json = `{
                "EventId": ${eventId},
                "UserId": ${userId},
                "StatusId": ${statusId}
            }`

            let participant = ParticipantDto.fromJson(json)

            expect(participant).toBeInstanceOf(ParticipantDto)
            participant = participant as ParticipantDto

            expect(participant.EventId).toBe(eventId)
            expect(participant.UserId).toBe(userId)
            expect(participant.StatusId).toBe(statusId)
        })

        it('should return null if the JSON string is empty', () => {
            const json = ''

            const participant = ParticipantDto.fromJson(json)

            expect(participant).toBeNull()
        })

        it('should throw an error when providing JSON without each property individually', () => {  
            const jsonWithoutEventId = `{
                "UserId": ${userId},
                "StatusId": ${statusId}
            }`

            const jsonWithoutUserId = `{
                "EventId": ${eventId},
                "StatusId": ${statusId}
            }`

            const jsonWithoutStatusId = `{
                "EventId": ${eventId},
                "UserId": ${userId}
            }`

            expect(() => ParticipantDto.fromJson(jsonWithoutEventId)).toThrow(ModelMappingError)
            expect(() => ParticipantDto.fromJson(jsonWithoutUserId)).toThrow(ModelMappingError)
            expect(() => ParticipantDto.fromJson(jsonWithoutStatusId)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of ParticipantDto from an object', () => {
            const object = {
                EventId: eventId,
                UserId: userId,
                StatusId: statusId
            }

            let participant = ParticipantDto.fromObject(object)

            expect(participant).toBeInstanceOf(ParticipantDto)
            participant = participant as ParticipantDto

            expect(participant.EventId).toBe(eventId)
            expect(participant.UserId).toBe(userId)
            expect(participant.StatusId).toBe(statusId)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                EventId: true,
                UserId: null,
                StatusId: 'invalid'
            }

            const objectWithoutEventId = {
                UserId: userId,
                StatusId: statusId
            }

            const objectWithoutUserId = {
                EventId: eventId,
                StatusId: statusId
            }

            const objectWithoutStatusId = {
                EventId: eventId,
                UserId: userId
            }

            expect(() => ParticipantDto.fromObject(object)).toThrow(ModelMappingError)
            expect(() => ParticipantDto.fromObject(objectWithoutEventId)).toThrow(ModelMappingError)
            expect(() => ParticipantDto.fromObject(objectWithoutUserId)).toThrow(ModelMappingError)
            expect(() => ParticipantDto.fromObject(objectWithoutStatusId)).toThrow(ModelMappingError)
        })
    })
})