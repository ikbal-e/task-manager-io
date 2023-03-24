import React, { useEffect, useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { Button, Col, Container, Form, FormGroup, Input, Label, Row } from 'reactstrap';
import { useRegisterUserMutation } from '../api/auth-api-slice';
import { useNavigate } from 'react-router-dom';
import { useLazyGetDepartmentsQuery } from '../api/departments-api-slice';

const CreateUser = () => {

    const [username, setUsername] = useState('');
    const [name, setName] = useState('');
    const [lastname, setLastanme] = useState('');
    const [departmentId, setDepartmentId] = useState(undefined);
    const [role, setRole] = useState(0);

    const navigate = useNavigate();

    //TODO: use form library
    //TODO edit and register in same component

    const [registerUser, { isLoading: changePasswordLoading }] = useRegisterUserMutation();
    const [getDepartments, { data: departments, isFetching }] = useLazyGetDepartmentsQuery();

    useEffect(() => {
        getDepartments();
    }, []);

    const handleRegisterUser = async () => {
        try {
            const result = await registerUser({username, name, lastname, departmentId, userRole: role, password: '123' }).unwrap();
            console.log(result); //TODO: 
            //navigate(0);
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <div>
            <div>
                <div>

                    {departmentId}
                    <Container>
                        <Row>
                            <Col lg="2"></Col>
                            <Col lg="8">
                                <Form>
                                    <FormGroup row>
                                        <Label for="username" sm={2}>
                                            Email
                                        </Label>
                                        <Col sm={10}>
                                            <Input id="username" name="username" placeholder="" type="email" value={username} onChange={(e) => setUsername(e.target.value)}></Input>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Label for="name" sm={2}>
                                            Name
                                        </Label>
                                        <Col sm={10}>
                                            <Input id="name" name="name" placeholder="" type="text" value={name} onChange={(e) => setName(e.target.value)}></Input>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Label for="name" sm={2}>
                                            Lastname
                                        </Label>
                                        <Col sm={10}>
                                            <Input id="name" name="name" placeholder="" type="text" value={lastname} onChange={(e) => setLastanme(e.target.value)}></Input>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Label for="role" sm={2}>
                                            Role
                                        </Label>
                                        <Col sm={10}>
                                            <Input id="role" name="role" type="select" value={role} onChange={e => setRole(e.target.value)}>
                                                <option value="" hidden></option>
                                                <option value={0}>User</option>
                                                <option value={1}>Admin</option>
                                            </Input>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Label for="department" sm={2}>
                                            Department
                                        </Label>
                                        <Col sm={10}>
                                            <Input id="department" name="department" type="select" value={departmentId} onChange={e => setDepartmentId(e.target.value)}>
                                            <option value="" hidden></option>
                                                {departments && departments.map((x) => (
                                                    <option key={x.id} value={x.id}>{x.name}</option>
                                                ))}
                                            </Input>
                                        </Col>
                                    </FormGroup>
                                    <Button onClick={(e) => { e.preventDefault(); handleRegisterUser() }}>Register</Button>
                                </Form>
                            </Col>
                            <Col lg="2"></Col>
                        </Row>
                    </Container>
                </div>
            </div>
        </div>
    )
}

export default CreateUser;