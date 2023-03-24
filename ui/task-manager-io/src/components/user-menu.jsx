import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useNavigate } from 'react-router-dom';
import { NavLink as RRNavLink } from 'react-router-dom';
import {
    Collapse,
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
    NavbarText,
    Button,
} from 'reactstrap';
import { logOut, selectUserIsAdmin } from '../redux/auth-slice';

const UserMenu = (args) => {
    const [isOpen, setIsOpen] = useState(false);
    const isAdmin = useSelector(selectUserIsAdmin);

    const navigate = useNavigate();
    const dispatch = useDispatch();

    const handleLogout = () => {
        dispatch(logOut());
        localStorage.clear('token');
        localStorage.clear('refreshToken');
        localStorage.clear('expiresAt');
        navigate('/');
    }

    return (
        <div>
            <Navbar color="light" light expand="sm" >
                <NavbarBrand tag={RRNavLink} to="/">Task Manager</NavbarBrand>
                <NavbarToggler onClick={() => { setIsOpen(!isOpen) }} />
                <Collapse isOpen={isOpen} navbar>
                    <Nav navbar className="container-fluid">
                        <NavItem>
                            <NavLink tag={RRNavLink} to="/login">Login</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={RRNavLink} to="/profile">Profile</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={RRNavLink} to="/counter">Counter</NavLink>
                        </NavItem>
                        {isAdmin &&
                            <>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/users">Users</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/departments">Departments</NavLink>
                                </NavItem>
                            </>
                        }
                        <NavItem className="ms-auto">
                            <Button color='primary' onClick={() => handleLogout()}>Logout</Button>
                        </NavItem>
                    </Nav>
                </Collapse>
            </Navbar>
        </div>
    );
}

export default UserMenu;