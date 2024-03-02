import axios, { AxiosResponse } from "axios";
import { MortgagePayment } from "../mortagepayments/MortgagePayment";
import MortgageSection from "../mortgage/Mortgage";

class MortgageService {

    public async Create(sections : MortgageSection[]) : Promise<any>{          
            await axios.post<MortgageSection[]>('https://localhost:7158/Mortgages/',{ sections: sections });
    }
}

export default new MortgageService();