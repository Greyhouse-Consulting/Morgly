import * as React from 'react';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import ListSubheader from '@mui/material/ListSubheader';
import DashboardIcon from '@mui/icons-material/Dashboard';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import PeopleIcon from '@mui/icons-material/People';
import CreateIcon from '@mui/icons-material/Create';
import LayersIcon from '@mui/icons-material/Layers';
import AddIcon from '@mui/icons-material/Add';
import AssignmentIcon from '@mui/icons-material/Assignment';
import { Link } from 'react-router-dom';
import ViewListIcon from '@mui/icons-material/ViewList';

export const mainListItems = (
  <React.Fragment>
    <ListSubheader component="div" inset>
      Applications
    </ListSubheader>
    <ListItemButton key='create-application'
      component={Link}
      to='/application/create'>
      <ListItemIcon>
        <CreateIcon />
      </ListItemIcon>
      <ListItemText primary="Create" />
    </ListItemButton>
    <ListItemButton key='list' component={Link}
      to='/application/list' >
      <ListItemIcon>
        <ViewListIcon />
      </ListItemIcon>
      <ListItemText primary="List" />
    </ListItemButton>

  </React.Fragment>
);

export const secondaryListItems = (
  <React.Fragment>
    <ListSubheader component="div" inset>
      Mortgages
    </ListSubheader>
    <ListItemButton key='create-mortgage'
      component={Link}
      to='/mortgages/create'>
      <ListItemIcon>
        <CreateIcon />
      </ListItemIcon>
      <ListItemText primary="Create" />
    </ListItemButton>
    <ListItemButton key='list-mortgages'
      component={Link}
      to='/mortgages/list'>
      <ListItemIcon>
        <ViewListIcon />
      </ListItemIcon>
      <ListItemText primary="List" />
    </ListItemButton>

  </React.Fragment>
);
