import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { styled, useTheme, Theme, CSSObject } from '@mui/material/styles';
import { Divider, IconButton, List, ListItem, ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import React from "react";
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import SearchIcon from '@mui/icons-material/Search';
import AddIcon from '@mui/icons-material/Add';
import FormatListBulletedIcon from '@mui/icons-material/FormatListBulleted';
import HomeIcon from '@mui/icons-material/Home';
import { Collapse } from '@mui/material';
import MuiDrawer from '@mui/material/Drawer';

import Box from '@mui/material/Box';
import { NavLink } from "react-router-dom";

interface SidePanelProps {

    open: boolean;
    setOpen: (open: boolean) => void;
}
const drawerWidth = 240; // Add this line to declare and assign a value to the 'drawerWidth' variable

const openedMixin = (theme: Theme): CSSObject => ({
        width: drawerWidth,
        transition: theme.transitions.create('width', {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen,
        }),
        overflowX: 'hidden',
});

  
  const closedMixin = (theme: Theme): CSSObject => ({
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: `calc(${theme.spacing(7)} + 1px)`,
    [theme.breakpoints.up('sm')]: {
      width: `calc(${theme.spacing(8)} + 1px)`,
    },
  });

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
    ({ theme, open }) => ({
      width: drawerWidth,
      flexShrink: 0,
      whiteSpace: 'nowrap',
      boxSizing: 'border-box',
      ...(open && {
        ...openedMixin(theme),
        '& .MuiDrawer-paper': openedMixin(theme),
      }),
      ...(!open && {
        ...closedMixin(theme),
        '& .MuiDrawer-paper': closedMixin(theme),
      }),
    }),
  );

export function SidePanel(props: SidePanelProps) {

    const [openSubMenu, setSubMenuOpen] = React.useState(true);

    const handleDrawerClose = () => {
        props.setOpen(false);
    };
    const handleClick = () => {
        setSubMenuOpen(!openSubMenu);
    };
    
    const theme = useTheme();

    const DrawerHeader = styled('div')(({ theme }) => ({
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'flex-end',
        padding: theme.spacing(0, 1),
        // necessary for content to be below app bar
        ...theme.mixins.toolbar,
    }));

    let menuType: [Text: string, Icon: string,  Path: string][] = [['All', 'All', '/list-applications'], ['New', 'Add', '/create']];

    const renderIcon = (param: string) => {
        switch (param) {
            case 'Add':
                return <AddIcon />;
            case 'All':
                return <SearchIcon />;
            default:
                return <InboxIcon />;
        }
    }

    return (
        <Drawer variant="permanent" open={props.open}>
            <DrawerHeader>
                <IconButton onClick={handleDrawerClose}>
                    {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
                </IconButton>
            </DrawerHeader>
            <Divider />
            <List>
                {menuType.map((item, index) => (
                    <ListItem key={item[0]} disablePadding sx={{ display: 'block' }}>
                        <NavLink to={item[2]} >
                        <ListItemButton
                            sx={{
                                minHeight: 48,
                                justifyContent: props.open ? 'initial' : 'center',
                                px: 2.5,
                            }}
                        >

                            <ListItemIcon
                                sx={{
                                    minWidth: 0,
                                    mr: props.open ? 3 : 'auto',
                                    justifyContent: 'center',
                                }}
                            >

                                
                            {renderIcon (item[1]) }
                                

                            </ListItemIcon>
                            <ListItemText primary={item[0]} sx={{ opacity: props.open ? 1 : 0 }} />

                        </ListItemButton>
                        </NavLink>
                    </ListItem>
                ))}
            </List>
            <Divider />
            <List>
                {/* {['All mail', 'Trash', 'Spam', 'dsds'].map((text, index) => (
                    <ListItem key={text} disablePadding sx={{ display: 'block' }}>
                        <ListItemButton
                            sx={{
                                minHeight: 48,
                                justifyContent: props.open ? 'initial' : 'center',
                                px: 2.5,
                            }}
                        >
                            <ListItemIcon
                                sx={{
                                    minWidth: 0,
                                    mr: props.open ? 3 : 'auto',
                                    justifyContent: 'center',
                                }}
                            >
                                {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                            </ListItemIcon>
                            <ListItemText primary={text} sx={{ opacity: props.open ? 1 : 0 }} />
                        </ListItemButton>
                    </ListItem>
                ))} */}
                <ListItem key="asdasd" disablePadding>
                    <ListItemButton onClick={handleClick}>
                        <ListItemIcon>
                            <HomeIcon />
                        </ListItemIcon>
                        <ListItemText primary="Mortages" />
                        {props.open ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                </ListItem>

                <Collapse in={props.open} timeout="auto" unmountOnExit>
                    <List component="div" disablePadding>
                        <ListItemButton sx={{ pl: 4 }}>
                            <ListItemIcon>
                                <FormatListBulletedIcon />
                            </ListItemIcon>
                            <ListItemText primary="All" />
                        </ListItemButton>
                    </List>
                </Collapse>
            </List>
        </Drawer>
    )

}