import { EventDto, EventDtoMapper } from '@reasn/common/models/EventDto'
import ModelMappingError from '@reasn/common/errors/ModelMappingError'
import { TagDto } from '@reasn/common/models/TagDto'

describe('EventDto', () => {
    const name = 'Test Event'
    const addressId = 1
    const description = 'This is a test event'
    const organizerId = 1
    const startAt = new Date('2022-01-01')
    const endAt = new Date('2022-01-02')
    const createdAt = new Date('2022-01-01')
    const updatedAt = new Date('2022-01-01')
    const slug = 'test-event'
    const statusId = 1
    const tags: TagDto[] = [{ Name: 'Tag 1' }, { Name: 'Tag 2' }]

    describe('fromJson', () => {
        it('should create an instance of EventDto from JSON string', () => {
            const json = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            let event = EventDtoMapper.fromJSON(json)
            event = event as EventDto

            expect(event.Name).toBe(name)
            expect(event.AddressId).toBe(addressId)
            expect(event.Description).toBe(description)
            expect(event.OrganizerId).toBe(organizerId)
            expect(event.StartAt).toEqual(startAt)
            expect(event.EndAt).toEqual(endAt)
            expect(event.CreatedAt).toEqual(createdAt)
            expect(event.UpdatedAt).toEqual(updatedAt)
            expect(event.Slug).toBe(slug)
            expect(event.StatusId).toBe(statusId)
            expect(event.Tags).toEqual(tags)
        })

        it('should return null if the JSON string is empty', () => {
            expect(() => EventDtoMapper.fromJSON('')).toThrow(ModelMappingError)
        })

        it('should throw an error when providing json without each property individually', () => {  
            const jsonWithoutName = `{
                "AddressId": ${addressId},
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutAddressId = `{
                "Name": "${name}",
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutDescription = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutOrganizerId = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "Description": "${description}",
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutStartAt = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutEndAt = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutCreatedAt = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutUpdatedAt = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutSlug = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "StatusId": ${statusId},
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutStatusId = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "Tags": ${JSON.stringify(tags)}
            }`

            const jsonWithoutTags = `{
                "Name": "${name}",
                "AddressId": ${addressId},
                "Description": "${description}",
                "OrganizerId": ${organizerId},
                "StartAt": "${startAt.toISOString()}",
                "EndAt": "${endAt.toISOString()}",
                "CreatedAt": "${createdAt.toISOString()}",
                "UpdatedAt": "${updatedAt.toISOString()}",
                "Slug": "${slug}",
                "StatusId": ${statusId}
            }`

            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutName)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutAddressId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutDescription)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutOrganizerId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutStartAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutEndAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutCreatedAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutUpdatedAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutSlug)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutStatusId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromJSON(jsonWithoutTags)
            }).toThrow(ModelMappingError)
        })
    })

    describe('fromObject', () => {
        it('should create an instance of EventDto from an object', () => {
            const obj = {
                Name: name,
                AddressId: addressId,
                Description: description,
                OrganizerId: organizerId,
                StartAt: startAt,
                EndAt: endAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                StatusId: statusId,
                Tags: tags
            }

            let event = EventDtoMapper.fromObject(obj)
            event = event as EventDto

            expect(event.Name).toBe(name)
            expect(event.AddressId).toBe(addressId)
            expect(event.Description).toBe(description)
            expect(event.OrganizerId).toBe(organizerId)
            expect(event.StartAt).toEqual(startAt)
            expect(event.EndAt).toEqual(endAt)
            expect(event.CreatedAt).toEqual(createdAt)
            expect(event.UpdatedAt).toEqual(updatedAt)
            expect(event.Slug).toBe(slug)
            expect(event.StatusId).toBe(statusId)
            expect(event.Tags).toEqual(tags)
        })

        it('should return null if the object is invalid', () => {
            const invalidObj = {
                Name: 1,
                AddressId: null,
                Description: 65,
                OrganizerId: true,
                StartAt: "abc",
                EndAt: NaN,
                CreatedAt: -1,
                UpdatedAt: "true",
                Slug: undefined,
                StatusId: -45,
                Tags: "no tags"
            }

            const objWithoutName = {
                AddressId: addressId,
                Description: description,
                OrganizerId: organizerId,
                StartAt: startAt,
                EndAt: endAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                StatusId: statusId,
                Tags: tags
            }

            const objWithoutAddressId = {
                Name: name,
                Description: description,
                OrganizerId: organizerId,
                StartAt: startAt,
                EndAt: endAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                StatusId: statusId,
                Tags: tags
            }

            const objWithoutDescription = {
                Name: name,
                AddressId: addressId,
                OrganizerId: organizerId,
                StartAt: startAt,
                EndAt: endAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                StatusId: statusId,
                Tags: tags
            }

            const objWithoutOrganizerId = {
                Name: name,
                AddressId: addressId,
                Description: description,
                StartAt: startAt,
                EndAt: endAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                StatusId: statusId,
                Tags: tags
            }

            const objWithoutStartAt = {
                Name: name,
                AddressId: addressId,
                Description: description,
                OrganizerId: organizerId,
                EndAt: endAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                StatusId: statusId,
                Tags: tags
            }

            const objWithoutEndAt = {
                Name: name,
                AddressId: addressId,
                Description: description,
                OrganizerId: organizerId,
                StartAt: startAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                StatusId: statusId,
                Tags: tags
            }

            const objWithoutCreatedAt = {
                Name: name,
                AddressId: addressId,
                Description: description,
                OrganizerId: organizerId,
                StartAt: startAt,
                EndAt: endAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                StatusId: statusId,
                Tags: tags
            }

            const objWithoutUpdatedAt = {
                Name: name,
                AddressId: addressId,
                Description: description,
                OrganizerId: organizerId,
                StartAt: startAt,
                EndAt: endAt,
                CreatedAt: createdAt,
                Slug: slug,
                StatusId: statusId,
                Tags: tags
            }

            const objWithoutSlug = {
                Name: name,
                AddressId: addressId,
                Description: description,
                OrganizerId: organizerId,
                StartAt: startAt,
                EndAt: endAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                StatusId: statusId,
                Tags: tags
            }

            const objWithoutStatusId = {
                Name: name,
                AddressId: addressId,
                Description: description,
                OrganizerId: organizerId,
                StartAt: startAt,
                EndAt: endAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                Tags: tags
            }

            const objWithoutTags = {
                Name: name,
                AddressId: addressId,
                Description: description,
                OrganizerId: organizerId,
                StartAt: startAt,
                EndAt: endAt,
                CreatedAt: createdAt,
                UpdatedAt: updatedAt,
                Slug: slug,
                StatusId: statusId
            }

            expect(() => {
                EventDtoMapper.fromObject(invalidObj)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutName)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutAddressId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutDescription)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutOrganizerId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutStartAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutEndAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutCreatedAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutUpdatedAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutSlug)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutStatusId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDtoMapper.fromObject(objWithoutTags)
            }).toThrow(ModelMappingError)
        })
    })
})
