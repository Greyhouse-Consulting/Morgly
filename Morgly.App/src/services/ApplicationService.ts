import axios, { AxiosResponse } from "axios";
import { Application } from "../applications/application";
import { BASE_APPLICATIONS_URL } from "../Constats";


class ApplicationService {

    public async GetItems() : Promise<Application[]>{          
            var d = await axios.get<Application[]>(BASE_APPLICATIONS_URL);
            return d.data
    }

    public async Delete(id:string) : Promise<any> {      
        await axios.delete(BASE_APPLICATIONS_URL + '/' + id)
    }
}

export default new ApplicationService();