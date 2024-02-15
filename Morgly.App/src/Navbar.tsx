import { Container, Nav, NavDropdown, Navbar } from "react-bootstrap";
import { Link } from "react-router-dom";

function TopMenu() {
    return (
      <Navbar expand="lg" className="bg-body-tertiary">
        <Container>
          <Navbar.Brand as={Link} to="/home">Morgly</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link as={Link} to="/home">Home</Nav.Link>
              <Nav.Link as={Link} to="/create">New (Application)</Nav.Link>
              <Nav.Link as={Link} to="/list-applications">List (Applications)</Nav.Link>
              <Nav.Link as={Link} to="/list">List (Mortages)</Nav.Link>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    );
  }
  
  export default TopMenu;