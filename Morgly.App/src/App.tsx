import React from 'react';
import './App.css';
import { CreateItem } from './CreateItem';
import { Route, Routes } from 'react-router-dom';
import { Home } from './Home';
import { Container } from 'react-bootstrap';
import BasicExample from './Navbar';
import { ListItems } from './ListItems';

function App() {
  return (
    <div className="App">

          <BasicExample />
      <Routes>
        <Route path="/home" element={<Home />} />
        <Route path="/create" element={<CreateItem />} />
        <Route path="/list" element={<ListItems />} />
      </Routes>

    </div>

  );
}

export default App;
