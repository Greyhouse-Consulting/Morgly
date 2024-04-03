import axios from "axios";
import { Component } from "react";

import { Box, Button, Container, FormControl, FormLabel, Grid, Paper, Popover, Stack, TextField, Typography } from "@mui/material";
import React from "react";
import { styled } from '@mui/material/styles';

import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { BASE_APPLICATIONS_URL } from "../Constats";


// const popoverClick = (
//   <Popover id="popover-trigger-click" title="Popover bottom">
//     <strong>Holy guacamole!</strong> Check this info.
//   </Popover>
// );
const DrawerHeader = styled('div')(({ theme }) => ({
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'flex-end',
  padding: theme.spacing(0, 1),
  // necessary for content to be below app bar
  ...theme.mixins.toolbar,
}));

export class CreateItem extends Component {


  state = { count: 0, show: false, target: null, propertyId: "NACKA TOLLARE 1:234", amount: 0, valid: true, startDate: null, open: false, anchorEl: null, id: undefined, amountInvalid: false, nameInvalid: false }

  handleDrawerOpen = () => {
    this.setState({ ...this.state, open: true });
  };

  handleDrawerClose = () => {
    this.setState({ ...this.state, open: false });
  };

  handlePopOverClose = () => {
    this.setState({ ...this.state, show: false });
  };
  Clicked = async (evt: React.MouseEvent<HTMLButtonElement>) => {
    console.log("heloooo!")
    console.log(this.state);
    const form = evt.currentTarget;

    try {

      const response = await axios.post(BASE_APPLICATIONS_URL, { amount: this.state.amount, startDate: this.state.startDate, propertyId: this.state.propertyId })
        .finally(() => {
          this.setState({ show: true, target: evt.target, propertyId: "", valid: true, amount: 0, startDate: null, anchorEl: evt.currentTarget, id: 'simple-popover', amountInvalid: false, nameInvalid: false });
        });
      console.log(response.data);
    } catch (error) {
      console.error(error);
    }
  }


  nameChanged = (evt: any) => {

    if (evt.target.value.length < 3) {
      this.setState({
        nameInvalid: true,
        propertyId: evt.target.value
      });
    }
    else
      this.setState({
        count: this.state.count + 1,
        propertyId: evt.target.value,
        nameInvalid: false
      })
  }
  amountChanged = (evt: any) => {

    console.log(evt.target.value);
    if (evt.target.value < 0) {
      console.error("Amount is invalid");
      this.setState({ amountInvalid: true });
    }
    else {
      console.log("Amount is valid");
      this.setState({
        count: this.state.count + 1,
        amoutInvalid: false,
        amount: evt.target.value
      })
    }
  }
  setStartDate = (date: Date | null) => {
    this.setState({ ...this.state, startDate: date });

  }


  render() {
    return <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Grid container spacing={3}>
        {/* Chart */}
        <Grid item xs={10} md={8} lg={19}>
          <Paper
            sx={{
              p: 2,
              display: 'flex',
              flexDirection: 'column',
              height: 240,
            }}
          >
            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <React.Fragment>
                <FormControl>
                  <Stack spacing={2} direction="row" sx={{ marginBottom: 4 }}>
                    <TextField type="text" variant='outlined' label="Property ID"  value={this.state.propertyId} helperText="Kommun Område 1:234" onChange={this.nameChanged} error={this.state.nameInvalid} required />
                    <TextField type="number" variant='outlined' defaultValue="" label="Amount" value={this.state.amount} onChange={this.amountChanged} error={this.state.amountInvalid} required />
                  </Stack>
                  <DateTimePicker label="Basic date time picker" ampm={false} views={['year', 'month', 'day']} format="YYYY-MM-DD" value={this.state.startDate} onChange={this.setStartDate} />
                  <Button aria-describedby={this.state.id} variant="contained" onClick={this.Clicked} >Save</Button>
                  <Popover
                    id={this.state.id}
                    open={this.state.show}
                    anchorEl={this.state.anchorEl}
                    onClose={this.handlePopOverClose}
                    anchorOrigin={{
                      vertical: 'bottom',
                      horizontal: 'left',
                    }}
                  >
                    <Typography sx={{ p: 2 }}>Saved!</Typography>
                  </Popover>
                </FormControl>
              </React.Fragment>
            </LocalizationProvider>
          </Paper>
        </Grid>
      </Grid>
    </Container>

    // <Container>
    //   <Form validated={this.state.valid}>e
    //     <Row>
    //       <Col>
    //         <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
    //           <Form.Label>Purpose</Form.Label>
    //           <Form.Control type="text" value={this.state.name} placeholder="Purpose" onChange={this.nameChanged} required />
    //           <Form.Control.Feedback type="invalid">
    //             Please provide a valid name.
    //           </Form.Control.Feedback>
    //         </Form.Group>
    //       </Col>
    //       <Col>
    //         <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
    //           <Form.Label>Amount</Form.Label>
    //           <Form.Control type="number" value={this.state.amount} placeholder="Amount" onChange={this.amountChanged} required />
    //           <Form.Control.Feedback type="invalid">
    //             Please provide a valid name.
    //           </Form.Control.Feedback>
    //         </Form.Group>
    //       </Col>
    //     </Row>
    //     <Row>
    //       <Col>
    //         <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
    //           <Form.Label>Description</Form.Label>
    //           <Form.Control as="textarea" rows={3} />
    //         </Form.Group>
    //       </Col>
    //       <Col></Col>
    //     </Row>
    //     <Overlay
    //       show={this.state.show}
    //       target={this.state.target}
    //       placement="bottom"
    //       containerPadding={20}  >
    //       <Popover id="popover-contained" title="Popover bottom">
    //         <strong>Item saved!</strong>
    //       </Popover>
    //     </Overlay>

    //     <OverlayTrigger trigger="click" placement="bottom" overlay={popoverClick}>
    //       <Button>Click</Button>
    //     </OverlayTrigger>
    //     <Button variant="primary" onClick={this.Clicked} >Save</Button>{' '}
    //   </Form>


    // </Container>

  }
}