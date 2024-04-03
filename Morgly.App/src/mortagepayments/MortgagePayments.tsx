import { Component } from "react";

import MortgagePaymentsService from "../services/MortgagePaymentsService";
import { MortgagePayment } from "./MortgagePayment";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";


export class MortagePayments extends Component<{id: string}, { id: string, show: boolean, items: MortgagePayment[]  }> {

    constructor(props: any) {
        super(props)
        this.state = { id: props.id, show: false, items: [] };
    }
    loadItems = async () => {
        MortgagePaymentsService
            .GetItems(this.state.id)            
            .then(data => {

                this.setState({ items: data });
            }).catch(err => console.error(err))
    }

    close = () => {
        this.setState({ show: false });
    }

    show = () => {
        this.setState({ show: true });
    }
    componentDidMount(): void {
        this.loadItems();
    }
    render() {
        return (
            <>
            
            <Button  onClick={this.show}>
              Payments
            </Button>
      
            <Dialog open={this.state.show} onClose={this.close}  >
              <DialogTitle >
                Upcoming payments
              </DialogTitle>
              <DialogContent>
              <Table aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Amount to pay</TableCell>
                        <TableCell>MonthlyInterest</TableCell>
                        <TableCell>MonthlyPrincipal</TableCell>
                        <TableCell>RemainingAmount</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {this.state.items.map((g) => <tr ><td>{g.amount}</td><td>{g.monthlyInterest}</td><td>{g.monthlyPrincipal}</td><td>{g.remainingAmount}</td></tr>)}
                </TableBody>
            </Table>

              </DialogContent>
              <DialogActions>
                <Button onClick={this.close}>
                  Close
                </Button>
              </DialogActions>
            </Dialog>
          </>
        )
    }
}
