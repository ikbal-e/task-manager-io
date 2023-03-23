import React, { useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useNavigate } from 'react-router-dom';
import { Button, Col, Container, Input, Label, Row, Toast, ToastHeader, ToastBody } from 'reactstrap';
import { useLoginMutation } from '../api/auth-api-slice';
import { setCredentials } from '../redux/auth-slice';

const Login = () => {

    const [user, setUser] = useState('');
    const [password, setPassword] = useState('');
    const [isErrorToastOpen, setIsErrorToastOpen] = useState(false);
    const [errorMessage, setErrorMessage] = useState('');

    const [login, { isLoading }] = useLoginMutation()
    const dispatch = useDispatch()
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        setIsErrorToastOpen(false);
        setErrorMessage('');
        try {
            const userData = await login({ username: user, password }).unwrap();
            console.log(userData); //TODO: 
            dispatch(setCredentials({ ...userData }));
            localStorage.setItem('token', userData.accessToken);
            localStorage.setItem('refreshToken', userData.refreshToken);
            localStorage.setItem('expiresAt', userData.expiresAt);
            setUser('');
            setPassword('');
            navigate('/counter');
        } catch (error) {
            console.log(error);

            setIsErrorToastOpen(true);
            setErrorMessage(error?.data?.detail);
        }
    }

    return (
        <div>
            <Container>
                <Row>
                    <Col lg="4"></Col>
                    <Col lg="4" style={{ display: 'flex', flexDirection: 'column', alignContent: 'center', justifyContent: 'center', marginTop: '25vh' }}>
                        <h2 style={{textAlign: 'center'}}>Task Manager</h2>
                        <Label for="email" style={{fontSize: 'medium'}}>
                            Username
                        </Label>
                        <Input
                            id="email"
                            name="email"
                            placeholder=""
                            type="email"
                            onChange={(e) => setUser(() => e.target.value)}
                        />
                        <Label for="password" style={{fontSize: 'medium'}}>
                            Password
                        </Label>
                        <Input
                            id="password"
                            name="password"
                            placeholder=""
                            type="password"
                            onChange={(e) => setPassword(() => e.target.value)}
                        />
                        <Button style={{ marginTop: '2%' }}
                            color="primary"
                            onClick={(e) => handleSubmit()}
                        >
                            Login
                        </Button>
                        {isErrorToastOpen ?
                            <div className="p-3 bg-danger my-2 rounded">
                                <Toast isOpen={isErrorToastOpen} >
                                    <ToastHeader>
                                        Hata
                                    </ToastHeader>
                                    <ToastBody>
                                        {errorMessage}
                                    </ToastBody>
                                </Toast>
                            </div>
                            :
                            null
                        }
                    </Col>
                    <Col lg="4"></Col>
                </Row>
            </Container>
        </div>
    )
}

export default Login;