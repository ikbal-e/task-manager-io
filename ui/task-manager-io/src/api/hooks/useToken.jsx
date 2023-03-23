import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setCredentials } from '../../redux/auth-slice';

export default function useToken() {

    const dispatch = useDispatch();

    const [loading, setLoading] = useState(true);
    const [token, setToken] = useState('');

    useEffect(() => {
        const token = localStorage.getItem('token');
        const refreshToken = localStorage.getItem('refreshToken');
        const expiresAt= localStorage.getItem('expiresAt');
        if (!!token) {
            dispatch(setCredentials({ user: "asd", accessToken: token, refreshToken: refreshToken, expiresAt: expiresAt })); //TODO:
        }
        setToken(token);
        setLoading(false);

    }, [dispatch]);


    const saveToken = token => {
        localStorage.setItem('token', token);
        setToken(token);
    };

    return {
        token: { token, loading },
        setToken: saveToken
    }
}