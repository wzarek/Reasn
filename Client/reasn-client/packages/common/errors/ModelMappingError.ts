import { ZodIssue } from "zod"

class ModelMappingError extends Error {
    constructor(entityName: string, message: string, issues?: ZodIssue[]) {
        super(`Entity mapping failed for ${entityName}. ${message}`)
        this.name = 'ModelMappingError'
    }
}

export default ModelMappingError