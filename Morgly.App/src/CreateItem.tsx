import axios from "axios";
import { Component } from "react";
import { Button, Col, Container, Form, Overlay, OverlayTrigger, Popover, Row } from 'react-bootstrap';
import DatePicker from "react-datepicker"
import "react-datepicker/dist/react-datepicker.module.css"
const popoverClick = (
  <Popover id="popover-trigger-click" title="Popover bottom">
    <strong>Holy guacamole!</strong> Check this info.
  </Popover>
);

export class CreateItem extends Component {


  state = { count: 0, show: false, target: null, name: "", amount: 0, valid: true, startDate: null }

  Clicked = async (evt: any) => {
    console.log("heloooo!")
    console.log(this.state);
    const form = evt.currentTarget;

    try {

      const response = await axios.post('https://localhost:7158/MortgageApplication', { amount: this.state.amount, startDate: this.state.startDate, purpose: this.state.name })
        .finally(() => {
          this.setState({ show: true, target: evt.target, name: "", valid: true, amount: 0 });
        });
      console.log(response.data);
    } catch (error) {
      console.error(error);
    }
  }


  nameChanged = (evt: any) => {

    this.setState({
      count: this.state.count + 1,
      name: evt.target.value
    })
  }
  amountChanged = (evt: any) => {

    this.setState({
      count: this.state.count + 1,
      amount: evt.target.value
    })
  }
  setStartDate = (date: Date | null) => {
    this.setState({ ...this.state, startDate: date });

  }
  render() {
    return <Container>
      <Form validated={this.state.valid}>
        <Row>
          <Col>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Purpose</Form.Label>
              <Form.Control type="text" value={this.state.name} placeholder="Purpose" onChange={this.nameChanged} required />
              <Form.Control.Feedback type="invalid">
                Please provide a valid name.
              </Form.Control.Feedback>
            </Form.Group>
          </Col>
          <Col>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Amount</Form.Label>
              <Form.Control type="number" value={this.state.amount} placeholder="Amount" onChange={this.amountChanged} required />
              <Form.Control.Feedback type="invalid">
                Please provide a valid name.
              </Form.Control.Feedback>
            </Form.Group>
          </Col>
        </Row>
        <Row>
          <Col>
            <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
              <Form.Label>Description</Form.Label>
              <Form.Control as="textarea" rows={3} />
            </Form.Group>
          </Col>
          <Col></Col>
        </Row>
        <Overlay
          show={this.state.show}
          target={this.state.target}
          placement="bottom"
          containerPadding={20}  >
          <Popover id="popover-contained" title="Popover bottom">
            <strong>Item saved!</strong>
          </Popover>
        </Overlay>
        <DatePicker selected={this.state.startDate} onChange={(date) => this.setStartDate(date)} />

        <OverlayTrigger trigger="click" placement="bottom" overlay={popoverClick}>
          <Button>Click</Button>
        </OverlayTrigger>
        <Button variant="primary" onClick={this.Clicked} >Save</Button>{' '}
      </Form>


    </Container>

  }
}