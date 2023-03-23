
import { Link, Outlet } from "react-router-dom"

const Layout = () => {

    return <>
        {/* <h1>This is the home page</h1> //TODO:
        <Link to="counter">Click to view our about page</Link>
        <Link to="login">Click to view our contact page</Link> */}
        <Outlet />
    </>
}

export default Layout