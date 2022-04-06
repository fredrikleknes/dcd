export class Asset {
    id?: string | undefined
    projectId?: string | undefined
    name?: string | undefined

    constructor(data?: any) {
        if (data !== undefined) {
            this.id = data.id
            this.projectId = data.projectId
            this.name = data.name ?? ""
        } else {
            this.id = "00000000-0000-0000-0000-000000000000"
            this.name = ""
        }
    }
}
