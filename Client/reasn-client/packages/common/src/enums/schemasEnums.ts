/**
 * Enum representing available user roles.
 */
export enum UserRole {
  ADMIN = "Admin",
  USER = "User",
  ORGANIZER = "Organizer",
}

/**
 * Enum representing available event statuses.
 */
export enum EventStatus {
  COMPLETED = "Completed",
  ONGOING = "Ongoing",
  CANCELLED = "Cancelled",
  APPROVED = "Approved",
  PENDING_APPROVAL = "Pending approval",
  REJECTED = "Rejected",
}

/**
 * Enum representing available object types.
 */
export enum ObjectType {
  USER = "User",
  EVENT = "Event",
}

/**
 * Enum representing available participant statuses.
 */
export enum ParticipantStatus {
  INTERESTED = "Interested",
  PARTICIPATING = "Participating",
}
