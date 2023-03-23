import { useState } from 'react'
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import useToken from './api/hooks/useToken';
import './App.css';
import Counter from './components/counter';
import Layout from './components/layout';
import Login from './components/login';
import { PrivateOutlet } from './private-outlet';

function App() {

  const { token, setToken } = useToken();

  if(token.loading) {
    //TODO: spinner?
    return;
  }

  return (
    <>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          {/* <Route index element={<Public />} /> */}
          <Route path="login" element={<Login />} />

          <Route element={<PrivateOutlet />}>
            <Route path="counter" element={<Counter />} />
          </Route>

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
