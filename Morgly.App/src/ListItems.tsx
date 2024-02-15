import React, { Component } from "react";
import { Button, Container, Modal, Table } from "react-bootstrap";
import MortgageService from "./services/ItemService";
import { Item2 } from "./Item";
import { MortagePayments } from "./mortagepayments/MortgagePayments";


export class ListItems extends Component<{}, {  show: boolean, selectedId:string, selectedName:string, items: Item2[] }> {

    constructor(props:any){
        super(props)     
        this.state = { show: false, selectedId:"", selectedName:"", items : []}; 
    }
    
    loadItems = async () => {
        MortgageService
        .GetItems()
        .then(data => {

            this.setState( { items : data});
        }).catch(err => console.error(err))
    }


    deleteItem = ( ) => {
        console.log("Deleting: ", this.state.selectedId)

        MortgageService
        .Delete(this.state.selectedId)
        .then(async () => {
            await this.loadItems().then(async () => {
                await this.setState({ ...this.state, selectedId:"" })
                
            }).then(() => {
                this.handleClose();
            });
            });        

    }

    deleteConfirm = async (id:string, name:string ) => {
        console.log("Confirm Deleting: ", id)

        await this.handleShow();
        this.setState({ ...this.state, selectedId : id, selectedName: name });

    }
    handleClose = () => {
        this.setState ({...this.state, show:false  })

    }
    handleShow  = () => {
        this.setState ({...this.state, show:true  })
    }

componentDidMount(): void {
    this.loadItems();
}
    render() {
        
        let s = this.state.items;

        return  <Container>
        <Table striped bordered hover>
    <thead>
    <tr>
        <th>Status</th>
        <th>Loan Id</th>        
        <th>Amount</th>        
        <th>Original Amount</th>        
        <th></th>
        <th></th>        
            </tr>
    </thead>
    <tbody>        
        {s.map((g) => <RowItemComponent key={g.id} id={g.id}  name={g.name} originalAmount={g.originalAmount} amount={g.amount} onDelete={() => this.deleteConfirm(g.id, g.name)}  /> )}
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
    name: string,
    amount: number,
    originalAmount: number,
    onDelete : (id:string)  => void    
}

const RowItemComponent : React.FC<RowItemProps> = ({ id, name, amount, originalAmount, onDelete }) => {
    return (<tr ><td></td><td>{id}</td><td>{amount}</td><td>{originalAmount}</td><td><MortagePayments id={id}/></td><td><Button variant="warning" onClick={() => onDelete(id)}>Delete</Button></td></tr>);

}
