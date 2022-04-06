import {
    Typography,
    Tooltip,
    Icon,
    EdsProvider,
    Button,
    TextField,
} from "@equinor/eds-core-react"
import {
    useState,
    ChangeEventHandler,
    MouseEventHandler,
} from "react"
import { useParams } from "react-router-dom"
import styled from "styled-components"
import { edit } from "@equinor/eds-icons"
import { Modal } from "../Components/Modal"
import { Project } from "../models/Project"
import { Case } from "../models/Case"
import { GetCaseService } from "../Services/CaseService"
import { Asset } from "../models/assets/Asset"

const EditCaseNameForm = styled.form`
    width: 30rem;

    > * {
        margin-bottom: 1.5rem;
    }
`

const AssetName = (asset: Asset) => {
    console.log("Hei")
    console.log(asset)
    const params = useParams()
    const [assetName, setAssetName] = useState<string>(asset?.name ?? "")

    const handleAssetNameChange: ChangeEventHandler<HTMLInputElement> = async (e) => {
        setAssetName(e.target.value)
    }

    return (
                <EditCaseNameForm>
                    <TextField
                        label="Asset name"
                        id="assetName"
                        name="name"
                        placeholder={assetName}
                        onChange={handleAssetNameChange}
                    />
                </EditCaseNameForm>
    )
}

export default AssetName
