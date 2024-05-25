import React from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import './App.css';
import AppHeader from "../Components/AppHeader/AppHeader";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from "../Pages/Home/Home";
import AuthPage from "../Pages/AuthPage/AuthPage";
import theme from "../config/CustomTheme";




const App: React.FC = () => {
    return (
        <ThemeProvider theme={theme}>
            <Router>
                <AppHeader />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/auth" element={<AuthPage />}></Route>
                </Routes>
            </Router>
        </ThemeProvider>

    );
};

export default App;
