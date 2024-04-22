class ModelMappingError extends Error {
    constructor(entityName: string, message: string) {
        super(`Entity mapping failed for ${entityName}. ${message}`)
        this.name = 'ModelMappingError'
    }
}

export default ModelMappingError