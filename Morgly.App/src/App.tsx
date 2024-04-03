import * as React from 'react';
import Typography from '@mui/material/Typography';
import Link from '@mui/material/Link';
import Router from './routes';
import Dashboard from './Dashboard';
import ProTip from './ProTip';
import { BrowserRouter } from 'react-router-dom';

function Copyright() {
  return (
    <Typography variant="body2" color="text.secondary" align="center">
      {'Copyright © '}
      <Link color="inherit" href="https://mui.com/">
        Your Website
      </Link>{' '}
      {new Date().getFullYear()}.
    </Typography>
  );
}
export default function App() {
  return (
    <BrowserRouter>
    <Router />
    </BrowserRouter>
  );
}
