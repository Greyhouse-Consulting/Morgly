import axios, { AxiosResponse } from "axios";
import { Item2 } from "../Item";
import { MortgageTerm } from "../mortgageterms/MortgageTerm";


const baseUrl : string = "https://localhost:7158/Mortgages";
class MortgageTermsService {

    public async Get(id: string ) : Promise<MortgageTerm[]>{          
            var d = await axios.get<MortgageTerm[]>('https://localhost:7158/Mortgages/'+ id);
            return d.data
    }

    public async Delete(id:string) : Promise<any> {      
        await axios.delete(baseUrl + '/' + id)
    }
}

export default new MortgageTermsService();