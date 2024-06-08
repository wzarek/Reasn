import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { ParticipantDto, ParticipantDtoMapper } from '@reasn/common/models/ParticipantDto'
import { ParticipantStatus } from "@reasn/common/enums/modelsEnums"

describe('ParticipantDto', () => {
    const eventId = 1
    const userId = 2
    const status = ParticipantStatus.INTERESTED

    describe('fromJson', () => {
        it('should create an instance of ParticipantDto from JSON string', () => {
            const json = `{
                "EventId": ${eventId},
                "UserId": ${userId},
                "Status": "${status}"
            }`

            let participant = ParticipantDtoMapper.fromJSON(json)
            participant = participant as ParticipantDto

            expect(participant.EventId).toBe(eventId)
            expect(participant.UserId).toBe(userId)
            expect(participant.Status).toBe(status)
        })

        it('should return null if the JSON string is empty', () => {
            expect(() => ParticipantDtoMapper.fromJSON('')).toThrow(ModelMappingError)
        })

        it('should throw an error when providing JSON without each property individually', () => {  
            const jsonWithoutEventId = `{
                "UserId": ${userId},
                "Status": "${status}"
            }`

            const jsonWithoutUserId = `{
                "EventId": ${eventId},
                "Status": "${status}"
            }`

            const jsonWithoutStatusId = `{
                "EventId": ${eventId},
                "UserId": ${userId}
            }`

            expect(() => ParticipantDtoMapper.fromJSON(jsonWithoutEventId)).toThrow(ModelMappingError)
            expect(() => ParticipantDtoMapper.fromJSON(jsonWithoutUserId)).toThrow(ModelMappingError)
            expect(() => ParticipantDtoMapper.fromJSON(jsonWithoutStatusId)).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of ParticipantDto from an object', () => {
            const object = {
                EventId: eventId,
                UserId: userId,
                Status: status
            }

            let participant = ParticipantDtoMapper.fromObject(object)
            participant = participant as ParticipantDto

            expect(participant.EventId).toBe(eventId)
            expect(participant.UserId).toBe(userId)
            expect(participant.Status).toBe(status)
        })

        it('should throw an error if the object is invalid', () => {
            const object = {
                EventId: true,
                UserId: null,
                Status: 'invalid'
            }

            const objectWithoutEventId = {
                UserId: userId,
                Status: status
            }

            const objectWithoutUserId = {
                EventId: eventId,
                Status: status
            }

            const objectWithoutStatusId = {
                EventId: eventId,
                UserId: userId
            }

            expect(() => ParticipantDtoMapper.fromObject(object)).toThrow(ModelMappingError)
            expect(() => ParticipantDtoMapper.fromObject(objectWithoutEventId)).toThrow(ModelMappingError)
            expect(() => ParticipantDtoMapper.fromObject(objectWithoutUserId)).toThrow(ModelMappingError)
            expect(() => ParticipantDtoMapper.fromObject(objectWithoutStatusId)).toThrow(ModelMappingError)
        })
    })
})