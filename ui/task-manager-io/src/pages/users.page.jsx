import React from 'react'
import CreateUser from '../components/create-user';
import Users from '../components/users';

const UsersPage = () => {
    return (
        <div style={{ margin: '5vh'}}>
            <h2>Users</h2>
            <CreateUser />
            <Users />
        </div>
    )
}

export default UsersPage;