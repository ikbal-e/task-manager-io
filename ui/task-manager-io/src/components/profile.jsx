import React, { useEffect, useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { decrement, increment, incrementByAmount } from '../redux/counter-slice'
import { Button, Col, Container, Form, FormGroup, Input, Label, Row } from 'reactstrap';
import { useLazyGetDepartmentsQuery } from '../api/departments-api-slice';
import { useChangePasswordMutation, useGetCurrentUserProfileQuery } from '../api/auth-api-slice';
import { useNavigate } from 'react-router-dom';

const Profile = () => {

    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [newPasswordAgain, setNewPasswordAgain] = useState('');

    const navigate = useNavigate();

    const [changePassword, { isLoading : changePasswordLoading }] = useChangePasswordMutation();

    useEffect(() => {
    }, []);

    const { data, isFetching, isLoading } = useGetCurrentUserProfileQuery(null, {
        refetchOnMountOrArgChange: true
    });

    if (!data) {
        return <>Loading</>
    }

    const handlePasswordChange = async () => {
        try {
            const result = await changePassword({ oldPassword: oldPassword, newPassword: newPassword }).unwrap();
            console.log(result); //TODO: 
            navigate(0);
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <div>
            <div>
                <div>
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
                                            <Input id="username" name="username" placeholder="" type="email" readOnly={true} value={data?.username}></Input>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Label for="name" sm={2}>
                                            Name
                                        </Label>
                                        <Col sm={10}>
                                            <Input id="name" name="name" placeholder="" type="text" readOnly={true} value={data?.fullname}></Input>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Label for="role" sm={2}>
                                            Role
                                        </Label>
                                        <Col sm={10}>
                                            <Input id="role" name="role" placeholder="" type="text" readOnly={true} value={data?.userRole}></Input>
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Label for="department" sm={2}>
                                            Department
                                        </Label>
                                        <Col sm={10}>
                                            <Input id="department" name="department" placeholder="" type="text" readOnly={true} value={data?.departmentName || ''}></Input>
                                        </Col>
                                    </FormGroup>
                                </Form>
                                <Form>
                                        <Row className="row-cols-lg-auto g-3 align-items-center">
                                            <Col>
                                                <Input
                                                    id="oldPassword"
                                                    name="oldPassword"
                                                    value={oldPassword}
                                                    onChange={(e) => setOldPassword(e.target.value)}
                                                    placeholder="Old Password"
                                                    type="password" />
                                            </Col>
                                            <Col>
                                                <Input
                                                    id="newPassword"
                                                    name="newPassword"
                                                    value={newPassword}
                                                    onChange={(e) => setNewPassword(e.target.value)}
                                                    placeholder="New Password"
                                                    type="password" />
                                            </Col>
                                            <Col>
                                                <Input
                                                    id="newPasswordAgain"
                                                    name="newPasswordAgain"
                                                    value={newPasswordAgain}
                                                    onChange={(e) => setNewPasswordAgain(e.target.value)}
                                                    placeholder="New Password"
                                                    type="password" />
                                            </Col>
                                            <Col>
                                                <Button disabled={!newPassword || !(newPassword && newPassword == newPasswordAgain)} color='primary'
                                                    onClick={() => handlePasswordChange()}>
                                                    Change Password
                                                </Button>
                                            </Col>
                                        </Row>
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

export default Profile;