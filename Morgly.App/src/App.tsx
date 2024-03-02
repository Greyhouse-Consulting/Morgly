import React from 'react';
import './App.css';
import { CreateItem } from './applications/CreateItem';
import { Route, Routes } from 'react-router-dom';
import { Home } from './Home';

import { ListItems } from './ListItems';
import { ListApplications } from './applications/ListApplications';
import CreateMortgage from './mortgage/CreateMortgage';
import { Box, CssBaseline } from '@mui/material';

function App() {
  return (
    <div className="App">

      <Box sx={{ display: 'flex' }}>
        <CssBaseline />
        {/* <BasicExample /> */}
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/Home" element={<Home />} />
          <Route path="/create" element={<CreateItem />} />
          <Route path="/list" element={<ListItems />} />
          <Route path="/list-applications" element={<ListApplications />} />
          <Route path="/create-mortgage/:id" element={<CreateMortgage />} />
        </Routes>

      </Box>

    </div>
  );
}

export default App;
