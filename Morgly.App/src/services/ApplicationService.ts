import axios, { AxiosResponse } from "axios";
import { Application } from "../applications/application";


const baseUrl : string = "https://localhost:7158/MortgageApplications";
class ApplicationService {

    public async GetItems() : Promise<Application[]>{          
            var d = await axios.get<Application[]>('https://localhost:7158/MortgageApplications');
            return d.data
    }

    public async Delete(id:string) : Promise<any> {      
        await axios.delete(baseUrl + '/' + id)
    }
}

export default new ApplicationService();