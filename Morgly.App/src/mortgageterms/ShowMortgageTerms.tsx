import { Component } from "react";
import { Button, Modal, Table } from "react-bootstrap";
import { MortgageTerm } from "./MortgageTerm";
import MortgageTermsService from "../services/MortgageTermsService";

export class ShowMortgageTerms extends Component<{id: string}, { id: string, show: boolean, previousTerms: MortgageTerm[], currentTerm : MortgageTerm | null, commingTerm: MortgageTerm| null  }> {

    constructor(props: any) {
        super(props)
      
      
        this.state = { id: props.id, show: false, previousTerms: [], currentTerm: null, commingTerm: null };
    }
    loadItems = async () => {
      MortgageTermsService
            .Get(this.state.id)            
            .then(data => {

                this.setState({ previousTerms: data });
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
                        <th>MortgageId</th>
                        <th>Length (In months)</th>
                        <th>Start date</th>
                        <th>Rate</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.previousTerms.map((g) => <tr ><td>{g.mortgageId}</td><td>{g.numberOfMonths}</td><td>{g.startDate.toDateString()}</td><td>{g.rate}</td></tr>)}
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
