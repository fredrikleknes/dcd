import { TopsideCostProfile } from "./TopsideCostProfile"
import { Asset } from "../Asset"
export class Topside extends Asset implements Components.Schemas.TopsideDto {
    costProfile?: TopsideCostProfile | undefined
    dryWeight?: number | undefined
    oilCapacity?: number | undefined
    gasCapacity?: number | undefined
    facilitiesAvailability?: number | undefined
    artificialLift?: Components.Schemas.ArtificialLift | undefined
    maturity?: Components.Schemas.Maturity | undefined

    constructor(data?: Components.Schemas.TopsideDto) {
        super(data)
        if (data !== undefined) {
            this.costProfile = TopsideCostProfile.fromJSON(data.costProfile)
            this.dryWeight = data.dryWeight
            this.maturity = data.maturity
        } 
    }

    static fromJSON(data: Components.Schemas.TopsideDto): Topside {
        return new Topside(data)
    }
}
