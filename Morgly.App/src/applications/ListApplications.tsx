import { Component } from "react";
import { Application } from "./application";
import ApplicationService from "../services/ApplicationService";
import { MortagePayments } from "../mortagepayments/MortgagePayments";
import { Link } from "react-router-dom";
import { Box, Button, Dialog, DialogActions, DialogContentText, DialogTitle, Table, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { SidePanel } from "../components/SidePanel";
import { TopMenu } from "../components/TopMenu";


export class ListApplications extends Component<{}, { show: boolean, selectedId: string, selectedName: string, items: Application[], open: boolean }> {

    constructor(props: any) {
        super(props)
        this.state = { show: false, selectedId: "", selectedName: "", items: [], open: false};
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

    setOpen = () => {
        this.setState({ ...this.state, open: true })
    }
    setClose = () => {
        this.setState({ ...this.state, open: false })
    }

    componentDidMount(): void {
        this.loadItems();
    }

    render() {

        let s = this.state.items;

        return      <div> <SidePanel open={this.state.open} setOpen={this.setOpen} />
        <TopMenu open={this.state.open} handleDrawerOpen={this.setOpen} />
        <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
            <TableContainer>
             <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Application Id</TableCell>
                        <TableCell>Status</TableCell>
                        <TableCell>Amount</TableCell>
                    </TableRow>
                </TableHead>
                <tbody>
                    {s.map((g) => <RowItemComponent key={g.id} id={g.id} status={g.status} amount={g.amount} onDelete={() => this.deleteConfirm(g.id)} />)}
                </tbody>
            </Table>

            <Dialog
                open={this.state.show}
                onClose={this.handleClose}
            >
                <DialogTitle>
                    Delete item?
                </DialogTitle>
                <DialogContentText>
                    Delete '{this.state.selectedName}' ?
                </DialogContentText>
                <DialogActions>
                    <Button  onClick={this.handleClose}>
                        Cancel
                    </Button>
                    <Button  onClick={this.deleteItem}>Confirm</Button>
                </DialogActions>
            </Dialog>
            </TableContainer>
        </Box></div>
    }
}

interface RowItemProps {
    id: string,
    amount: number,
    status: string,
    onDelete: (id: string) => void
}

const RowItemComponent: React.FC<RowItemProps> = ({ id, status, amount, onDelete }) => {
    return (<TableRow ><TableCell >{id}</TableCell ><TableCell >{status}</TableCell ><TableCell >{amount}</TableCell ><TableCell ><Button onClick={() => onDelete(id)}>Delete</Button></TableCell ><TableCell ><Link to={`/create-mortgage/${id}`}>Create</Link></TableCell > </TableRow>);
}





