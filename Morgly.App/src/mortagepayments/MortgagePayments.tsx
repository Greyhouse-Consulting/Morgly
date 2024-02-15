import { Component } from "react";
import { Button, Modal, Table } from "react-bootstrap";
import MortgagePaymentsService from "../services/MortgagePaymentsService";
import { MortgagePayment } from "./MortgagePayment";


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
            
            <Button variant="primary" onClick={this.show}>
              Payments
            </Button>
      
            <Modal show={this.state.show} onHide={this.close}  size="lg">
              <Modal.Header closeButton>
                <Modal.Title>Upcoming payments</Modal.Title>
              </Modal.Header>
              <Modal.Body>
              <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Amount to pay</th>
                        <th>MonthlyInterest</th>
                        <th>MonthlyPrincipal</th>
                        <th>RemainingAmount</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.items.map((g) => <tr ><td>{g.amount}</td><td>{g.monthlyInterest}</td><td>{g.monthlyPrincipal}</td><td>{g.remainingAmount}</td></tr>)}
                </tbody>
            </Table>

              </Modal.Body>
              <Modal.Footer>
                <Button variant="primary" onClick={this.close}>
                  Close
                </Button>
              </Modal.Footer>
            </Modal>
          </>
        )
    }
}
