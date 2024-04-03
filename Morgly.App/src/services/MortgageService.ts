import axios, { AxiosResponse } from "axios";
import { MortgagePayment } from "../mortagepayments/MortgagePayment";
import MortgageSection from "../mortgage/Mortgage";
import { BASE_MORTGAGES_URL } from "../Constats";

class MortgageService {

    public async Create(sections : MortgageSection[]) : Promise<any>{          
            await axios.post<MortgageSection[]>(BASE_MORTGAGES_URL,{ sections: sections });
    }
}

export default new MortgageService();