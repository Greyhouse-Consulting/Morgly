import { Button, Container, FormControl, Grid, Paper, TextField } from "@mui/material";
import Chart from "../Chart";
import Deposits from "../Deposits";
import Orders from "../Orders";


export function Test() {
    const handleSubmit = (event: { preventDefault: () => void; }) => {
        event.preventDefault();
        alert("Form Submitted");
    }
    return (
        <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
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
                        <form onSubmit={handleSubmit}>
                            <FormControl>
                                <TextField id="standard-basic" label="Name" variant="outlined" />
                                <TextField id ="asdasd" label="Your message" />
                                <Button type="submit">Submit</Button>
                            </FormControl>
                        </form>
                    </Paper>
                </Grid>

            </Grid>
        </Container>
    )
}