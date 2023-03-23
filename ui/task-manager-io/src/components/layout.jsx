
import { Link, Outlet } from "react-router-dom"
import UserMenu from "./user-menu"

const Layout = () => {

    return <>
        <UserMenu />
        <Outlet />
    </>
}

export default Layout