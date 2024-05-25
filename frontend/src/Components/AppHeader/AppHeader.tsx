import React from 'react';
import {AppBar, Toolbar, IconButton, InputBase, Typography, Box, Button, Badge, Container} from '@mui/material';
import { Search as SearchIcon, ShoppingCartOutlined, FavoriteBorderOutlined, PersonOutlined } from '@mui/icons-material';
import { styled, alpha } from '@mui/material/styles';
import logo from '../../resources/Logo.svg'
import {Link} from "react-router-dom";

const Search = styled('div')(({ theme }) => ({
    position: 'relative',
    borderRadius: theme.shape.borderRadius,
    backgroundColor: alpha(theme.palette.common.white, 1),
    '&:hover': {
        backgroundColor: alpha(theme.palette.common.white, 0.9),
    },
    marginLeft: 0,
    width: '100%',
    [theme.breakpoints.up('sm')]: {
        marginLeft: theme.spacing(1),
        width: '50%',
    },
}));

const SearchIconWrapper = styled('div')(({ theme }) => ({
    padding: theme.spacing(0, 2),
    height: '100%',
    position: 'absolute',
    pointerEvents: 'none',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    color: '#000'
}));

const StyledInputBase = styled(InputBase)(({ theme }) => ({
    color: '#000000',
    '& .MuiInputBase-input': {
        padding: theme.spacing(1, 1, 1, 0),
        paddingLeft: `calc(1em + ${theme.spacing(4)})`,
        transition: theme.transitions.create('width'),
        width: '100%',
        [theme.breakpoints.up('sm')]: {
            width: '20ch',
        },
    },
}));

const AppHeader: React.FC = () => {
    return (
        <AppBar position="static" sx={{ bgcolor: '#1B6392', color: '#fff' }}>
            <Container>
                <Toolbar>
                    <Typography variant="h6" noWrap component="div" sx={{ flexGrow: 1 }}>
                        <Box component="span" sx={{ display: 'flex', alignItems: 'center' }}>
                            <Link to="/"><img src={logo} alt="CLICON" style={{marginRight: '8px'}}/></Link>
                        </Box>
                    </Typography>
                    <Search>
                        <SearchIconWrapper>
                            <SearchIcon />
                        </SearchIconWrapper>
                        <StyledInputBase
                            placeholder="Search for anything…"
                            inputProps={{ 'aria-label': 'search' }}
                        />
                    </Search>
                    <Box sx={{ flexGrow: 1 }} />
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <Badge badgeContent={2}
                           anchorOrigin={{
                            vertical: 'top',
                            horizontal: 'right',
                        }}
                               sx={{
                                   '& .MuiBadge-badge': {
                                       top: 8,
                                       right: 8,
                                       padding: 0.5,
                                       backgroundColor: '#fff',
                                       rounded: '50%',
                                       color: '#1B6392'
                                   },
                               }}
                        >
                        <IconButton color="inherit">
                            <ShoppingCartOutlined/>
                        </IconButton>
                        </Badge>
                        <IconButton color="inherit">
                            <FavoriteBorderOutlined />
                        </IconButton>
                        <Link to='/auth'>
                            <IconButton color="inherit">
                                <PersonOutlined />
                            </IconButton>
                        </Link>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
};

export default AppHeader;
