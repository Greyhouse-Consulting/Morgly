import React from 'react';
import './App.css';
import { CreateItem } from './applications/CreateItem';
import { Route, Routes } from 'react-router-dom';
import { Home } from './Home';
import BasicExample from './Navbar';
import { ListItems } from './ListItems';
import { ListApplications } from './applications/ListApplications';
import CreateMortgage from './mortgage/CreateMortgage';

function App() {
  return (
    <div className="App">

      <BasicExample />
      <Routes>
        <Route path="/home" element={<Home />} />
        <Route path="/create" element={<CreateItem />} />
        <Route path="/list" element={<ListItems />} />
        <Route path="/list-applications" element={<ListApplications />} />
        <Route path="/create-mortgage/:id" element={<CreateMortgage />} />
      </Routes>

    </div>

  );
}

export default App;
