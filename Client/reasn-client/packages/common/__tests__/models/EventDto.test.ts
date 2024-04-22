import { EventDto } from '@reasn/common/models/EventDto'
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

    describe('constructor', () => {
        it('should create an instance of EventDto', () => {
            const event = new EventDto(
                name,
                addressId,
                description,
                organizerId,
                startAt,
                endAt,
                createdAt,
                updatedAt,
                slug,
                statusId,
                tags
            )

            expect(event).toBeInstanceOf(EventDto)
            expect(event.Name).toBe(name)
            expect(event.AddressId).toBe(addressId)
            expect(event.Description).toBe(description)
            expect(event.OrganizerId).toBe(organizerId)
            expect(event.StartAt).toBe(startAt)
            expect(event.EndAt).toBe(endAt)
            expect(event.CreatedAt).toBe(createdAt)
            expect(event.UpdatedAt).toBe(updatedAt)
            expect(event.Slug).toBe(slug)
            expect(event.StatusId).toBe(statusId)
            expect(event.Tags).toBe(tags)
        })
    })

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

            let event = EventDto.fromJson(json)

            expect(event).toBeInstanceOf(EventDto)
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
            const json = ''

            const event = EventDto.fromJson(json)

            expect(event).toBeNull()
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
                EventDto.fromJson(jsonWithoutName)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutAddressId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutDescription)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutOrganizerId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutStartAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutEndAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutCreatedAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutUpdatedAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutSlug)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutStatusId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromJson(jsonWithoutTags)
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

            let event = EventDto.fromObject(obj)

            expect(event).toBeInstanceOf(EventDto)
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
                EventDto.fromObject(invalidObj)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutName)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutAddressId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutDescription)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutOrganizerId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutStartAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutEndAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutCreatedAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutUpdatedAt)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutSlug)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutStatusId)
            }).toThrow(ModelMappingError)
            expect(() => {
                EventDto.fromObject(objWithoutTags)
            }).toThrow(ModelMappingError)
        })
    })
})
