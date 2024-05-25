import {createTheme, ThemeOptions} from "@mui/material/styles";
import { PaletteOptions } from '@mui/material/styles/createPalette';

interface CustomPaletteOptions extends PaletteOptions {
    tabTextColor?: {
        main: string;
    };
    tabIndicatorColor?: {
        main: string;
    };
}


const theme = createTheme({
    typography: {
        fontFamily: 'Public Sans, sans-serif', // ваш шрифт в теме
    },
    palette: {
        tabTextColor: {
            main: '#ff9800', // your custom text color
        },
        tabIndicatorColor: {
            main: '#000', // your custom indicator color
        },
    },
} as ThemeOptions & CustomPaletteOptions);

export default theme;