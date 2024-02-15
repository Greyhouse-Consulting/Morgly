import axios, { AxiosResponse } from "axios";
import { Item2 } from "../Item";


const baseUrl : string = "https://localhost:7158/Mortgages";
class MortgageService {

    public async GetItems() : Promise<Item2[]>{          
            var d = await axios.get<Item2[]>('https://localhost:7158/Mortgages');
            return d.data
    }

    public async Delete(id:string) : Promise<any> {      
        await axios.delete(baseUrl + '/' + id)
    }
}

export default new MortgageService();