import React, { useEffect, useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { Button, Col, Container, Form, FormGroup, Input, Label, Row, Table } from 'reactstrap';
import { useGetUserProfilesQuery, useRegisterUserMutation } from '../api/auth-api-slice';
import { useNavigate } from 'react-router-dom';
import { useLazyGetDepartmentsQuery } from '../api/departments-api-slice';

const Users = () => {

    const [username, setUsername] = useState('');
    const [name, setName] = useState('');
    const [lastname, setLastanme] = useState('');
    const [departmentId, setDepartmentId] = useState(undefined);
    const [role, setRole] = useState(0);

    const navigate = useNavigate();

    const { data: users, isFetching, isLoading } = useGetUserProfilesQuery(null, {
        refetchOnMountOrArgChange: true
    });

    return (
        <div>
            <Table hover responsive size="sm">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Username  </th>
                        <th>Name</th>
                        <th>Department</th>
                        <th>Role</th>
                    </tr>
                </thead>
                <tbody>
                    {users && users.map((x) => (
                        <tr key={x.id}>
                            <td>{x.id}</td>
                            <td>{x.username}</td>
                            <td>{x.fullname}</td>
                            <td>{x.departmentName}</td>
                            <td>{x.userRole}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>

        </div>
    )
}

export default Users;