import axios, { AxiosResponse } from "axios";
import { MortgagePayment } from "../mortagepayments/MortgagePayment";

class MortgagePaymentsService {

    public async GetItems(id : string) : Promise<MortgagePayment[]>{          
            var d = await axios.get<MortgagePayment[]>('https://localhost:7158/Mortgage/' +id + '/Payments');
            return d.data
    }
}

export default new MortgagePaymentsService();