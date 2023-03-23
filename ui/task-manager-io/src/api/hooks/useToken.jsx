import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setCredentials } from '../../redux/auth-slice';

export default function useToken() {

    const dispatch = useDispatch();

    const [loading, setLoading] = useState(true);
    const [token, setToken] = useState('');

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!!token) {
            dispatch(setCredentials({ user: "asd", accessToken: token }));
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