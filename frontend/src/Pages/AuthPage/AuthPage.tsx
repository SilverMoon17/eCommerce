import * as React from 'react';
import Box from '@mui/material/Box';
import Tab from '@mui/material/Tab';
import TabContext from '@mui/lab/TabContext';
import TabList from '@mui/lab/TabList';
import TabPanel from '@mui/lab/TabPanel';


const AuthPage: React.FC = () => {
    const [value, setValue] = React.useState('1');

    const handleChange = (event: React.SyntheticEvent, newValue: string) => {
        setValue(newValue);
    };

    return (
        <Box sx={{ width: '30%', typography: 'body1', marginLeft: 'auto', marginRight: 'auto'}}>
            <TabContext value={value}>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <TabList onChange={handleChange} aria-label="lab API tabs example" >
                        <Tab label="Sign In" value="1" />
                        <Tab label="Sign Up" value="2" />
                    </TabList>
                </Box>
                <TabPanel value="1">Sign In</TabPanel>
                <TabPanel value="2">Sign Up</TabPanel>
            </TabContext>
        </Box>
    );
};

export default AuthPage;