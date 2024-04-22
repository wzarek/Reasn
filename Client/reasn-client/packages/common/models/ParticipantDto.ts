import ModelMappingError from '@reasn/common/errors/ModelMappingError'

export class ParticipantDto {
    EventId: number
    UserId: number
    StatusId: number

    constructor(eventId: number, userId: number, statusId: number) {
        this.EventId = eventId
        this.UserId = userId
        this.StatusId = statusId
    }

    static fromJson(json: string): ParticipantDto | null {
        if (!json) {
            return null
        }

        return ParticipantDto.fromObject(JSON.parse(json))
    }

    static fromObject(obj: object): ParticipantDto | null {
        if (!obj) {
            return null
        }

        if ('EventId' in obj === false || typeof obj.EventId !== 'number') throw new ModelMappingError('ParticipantDto','EventId is required')
        if ('UserId' in obj === false || typeof obj.UserId !== 'number') throw new ModelMappingError('ParticipantDto','UserId is required')
        if ('StatusId' in obj === false || typeof obj.StatusId !== 'number') throw new ModelMappingError('ParticipantDto','StatusId is required')

        return new ParticipantDto(obj.EventId, obj.UserId, obj.StatusId)
    }
}