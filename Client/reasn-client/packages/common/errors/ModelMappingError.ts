import { ZodError, ZodIssue } from "zod"

class ModelMappingError extends Error {
    issues: ZodIssue[]

    constructor(entityName: string, message: string, zodError: ZodError | null = null) {
        let issuesMessage = message
        if (zodError) {
            let fieldErrors = zodError.flatten()['fieldErrors']
            issuesMessage = Object.keys(fieldErrors).map(key => `${key}: ${fieldErrors[key]}`).join(', ')
        }
        super(`Entity mapping failed for ${entityName}. ${issuesMessage}`)
        this.name = 'ModelMappingError'
        this.issues = zodError?.issues || []
        console.log(this.message)
    }
}

export default ModelMappingError