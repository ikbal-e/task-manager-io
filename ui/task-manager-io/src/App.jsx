import { useState } from 'react'
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import useToken from './api/hooks/useToken';
import './App.css';
import Counter from './components/counter';
import Layout from './components/layout';
import Login from './components/login';
import DepartmentsPage from './pages/departments.page';
import LoginPage from './pages/login.page';
import MainPage from './pages/main.page';
import ProfilePage from './pages/profile.page';
import UsersPage from './pages/users.page';
import { PrivateOutlet } from './private-outlet';

function App() {

  const { token, setToken } = useToken();

  if (token.loading) {
    //TODO: spinner?
    return;
  }

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<PrivateOutlet />}>
            <Route path="/" element={<Layout />}>
              <Route index element={<MainPage />} />
              <Route path="counter" element={<Counter />} />
              <Route path="profile" element={<ProfilePage />} />
              <Route path="users" element={<UsersPage />} />
              <Route path="departments" element={<DepartmentsPage /> } />
            </Route>
          </Route>
          <Route path="/login" element={<Layout />}>
            <Route index element={<LoginPage />} />
          </Route>
        </Routes>
        {/* <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="*" element={<PrivateOutlet />}>
          <Route index element={<Counter />} />
          <Route path="counter" element={<Counter />} />
        </Route>
      </Routes> */}
      </BrowserRouter>
    </>
  )
}

export default App
