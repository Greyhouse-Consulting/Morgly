import { SidePanel } from "./components/SidePanel";
import { Box } from "@mui/material";
import React from "react";

import { styled } from '@mui/material/styles';
import { TopMenu } from "./components/TopMenu";



const drawerWidth = 240;


export function Home() {


    const [open, setOpen] = React.useState(false);
    const handleDrawerOpen = () => {
        setOpen(true);
    };

    const DrawerHeader = styled('div')(({ theme }) => ({
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'flex-end',
        padding: theme.spacing(0, 1),
        // necessary for content to be below app bar
        ...theme.mixins.toolbar,
    }));

    return (
        <div>

            <SidePanel open={open} setOpen={setOpen} />
            <TopMenu open={open} handleDrawerOpen={handleDrawerOpen} />
            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                <DrawerHeader />
                <React.Fragment>Hello world</React.Fragment>
            </Box>
        </div>
    )
}