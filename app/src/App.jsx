import React from 'react';
import './app.css';

import {Routes, Route} from 'react-router-dom';

import {AppContext} from './context/AppContext';

import {Account, Categories, Dashboard, Home, NotFound, SignIn, SignUp, Tasks, User, VerifyEmail} from './pages';

const App = () => {
  
  const [user, setUser] = React.useState(null);

  const getAuthenticatedUser = async() => {
    let url = "https://localhost:7122/api/user/get-user";
    let response = await fetch(url, {
      credentials : 'include',
      headers : {
        "Content-Type": "application/json",
      }
    });
    if(response.status !== 401)
    {
      const content = await response.json();
      if(content.success){
        setUser(content.data)
      }
    }
  }

  getAuthenticatedUser();

  return (
    <AppContext.Provider
      value={{
        user: user,
      }}
    >
      <div className='bg-dark-background text-white h-screen'>
        <Routes>
          <Route index path='/' element={<Home />} />
          <Route path='sign-in' element={<SignIn />} />
          <Route path='sign-up' element={<SignUp />} />
          <Route path='verify-email:verificationToken' element={<VerifyEmail />} />
          <Route path='user' element={<User />}>
            <Route index path='' element={<Tasks />} />
            <Route path='account' element={<Account />} />
            <Route path='categories' element={<Categories />} />
            <Route path='dashboard' element={<Dashboard />} />
          </Route>
          <Route path='*' element={<NotFound />} />
        </Routes>
      </div>
    </AppContext.Provider>
  )
}

export default App