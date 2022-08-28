import React from 'react';
import './app.css';

import {Routes, Route} from 'react-router-dom';

import {Account, Categories, Dashboard, Home, NotFound, SignIn, SignUp, Tasks, User} from './pages';

const App = () => {
  return (
    <div className='bg-dark-background text-white h-screen'>
      <Routes>
        <Route index path='/' element={<Home />} />
        <Route path='sign-in' element={<SignIn />} />
        <Route path='sign-up' element={<SignUp />} />
        <Route path='user' element={<User />}>
          <Route index path=':userID' element={<Tasks />} />
          <Route path=':userID/account' element={<Account />} />
          <Route path=':userID/categories' element={<Categories />} />
          <Route path=':userID/dashboard' element={<Dashboard />} />
        </Route>
        <Route path='*' element={<NotFound />} />
      </Routes>
    </div>
  )
}

export default App