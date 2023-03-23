import { useEffect } from "react";
import { useSelector } from "react-redux";
import { Link, Outlet, useNavigate } from "react-router-dom"
import Login from "../components/login";
import { selectCurrentToken } from "../redux/auth-slice";

const LoginPage = () => {

    const token = useSelector(selectCurrentToken);
    const navigate = useNavigate();

    useEffect(() => {
        if (!!token) {
            navigate('/');
        }
    }, []);

    if (!!token) {
        return <></>;
    }

    return <>
        <h2><Login/></h2>
    </>
}

export default LoginPage