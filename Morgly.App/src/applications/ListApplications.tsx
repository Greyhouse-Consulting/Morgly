import { Component } from "react";
import { Application } from "./application";
import ApplicationService from "../services/ApplicationService";
import { Button, Container, Modal, Table } from "react-bootstrap";
import { MortagePayments } from "../mortagepayments/MortgagePayments";


export class ListApplications extends Component<{}, { show: boolean, selectedId: string, selectedName: string, items: Application[] }> {

    constructor(props: any) {
        super(props)
        this.state = { show: false, selectedId: "", selectedName: "", items: [] };
    }

    loadItems = async () => {
        ApplicationService
            .GetItems()            
            .then(data => {

                this.setState({ items: data });
            }).catch(err => console.error(err))
    }

    deleteItem = () => {
        console.log("Deleting: ", this.state.selectedId)
        ApplicationService
            .Delete(this.state.selectedId)
            .then(async () => {
                await this.loadItems().then(async () => {
                    await this.setState({ ...this.state, selectedId: "" })

                }).then(() => {
                    this.handleClose();
                });
            });

    }

    deleteConfirm = async (id: string) => {
        console.log("Confirm Deleting: ", id)

        await this.handleShow();
        this.setState({ ...this.state, selectedId: id });

    }
    handleClose = () => {
        this.setState({ ...this.state, show: false })

    }
    handleShow = () => {
        this.setState({ ...this.state, show: true })
    }

    componentDidMount(): void {
        this.loadItems();
    }

    render() {

        let s = this.state.items;

        return <Container>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Application Id</th>
                        <th>Status</th>
                        <th>Amount</th>
                    </tr>
                </thead>
                <tbody>
                    {s.map((g) => <RowItemComponent key={g.id} id={g.id} status={g.status} amount={g.amount} onDelete={() => this.deleteConfirm(g.id)} />)}
                </tbody>
            </Table>

            <Modal
                show={this.state.show}
                onHide={this.handleClose}
                backdrop="static"
                keyboard={false}
            >
                <Modal.Header closeButton>
                    <Modal.Title>Delete item?</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Delete '{this.state.selectedName}' ?
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={this.handleClose}>
                        Cancel
                    </Button>
                    <Button variant="primary" onClick={this.deleteItem}>Confirm</Button>
                </Modal.Footer>
            </Modal>
        </Container>
    }
}

interface RowItemProps {
    id: string,
    amount: number,
    status: string,
    onDelete: (id: string) => void
}

const RowItemComponent: React.FC<RowItemProps> = ({ id, status, amount, onDelete }) => {
    return (<tr ><td>{id}</td><td>{status}</td><td>{amount}</td><td><Button variant="warning" onClick={() => onDelete(id)}>Delete</Button></td> </tr>);

}





