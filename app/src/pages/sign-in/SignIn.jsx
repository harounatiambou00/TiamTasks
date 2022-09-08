import React, {useState} from 'react';

import {Button} from '@mui/material';
import {Input} from '../../components/core';
import {FcGoogle} from 'react-icons/fc';
import {BsFacebook} from 'react-icons/bs';

import {useNavigate}  from 'react-router-dom';

const SignIn = () => {
  const [showPassword, setShowPassword] = React.useState(false);
  const [userNotVerified, setUserNotVerified] = useState(false);
  const [userNotFound, setUserNotFound] = useState(false);
  const [incorrectPassword, setIncorrectPassword] = useState(false);
  const navigate = useNavigate();
  
  const [values, setValues] = React.useState({
    email: "",
    password: ""
  });

  const handleChange = (prop) => (event) => {
    setValues({ ...values, [prop]: event.target.value });
  };

  const login =  async(e) => {
    e.preventDefault();
    const url = 'https://localhost:7122/api/user/sign-in';
    let request = {
      email: values.email,
      password: values.password,
      remenberMe: true
    };

    let response = await fetch(url,{
      method : "POST",
      body : JSON.stringify(request),
      credentials : 'include',
      headers : {
        "Content-Type": "application/json",
      }
    });

    let content = await response.json();
    console.log(content);
    if(content.success)
      navigate('/')
    else{
      if(content.message === "USER_NOT_FOUND"){
        setUserNotFound(true);
        setUserNotVerified(false);
        setIncorrectPassword(false);
      }else if(content.message === "USER_NOT_VERIFIED"){
        setUserNotFound(false);
        setUserNotVerified(true);
        setIncorrectPassword(false);
      }
      else if(content.message === "INCORRECT_PASSWORD"){
        setUserNotFound(false);
        setUserNotVerified(false);
        setIncorrectPassword(true);
      }
    }
  }

  return (
    <div
      className='h-full flex items-center justify-center'
    >
      <div
        className='sm:w-full lg:w-96 sm:px-14 lg:p-0'
      >
        <h1
          className='sm:text-8xl lg:text-4xl font-bold text-center sm:mb-16 lg:mb-10 text-gray-400'
        >
          Sign In
        </h1>
        <div
          className='flex flex-col'
        >
          <button
            className='flex items-center justify-center sm:mb-10 lg:mb-3 rounded-xl sm:h-24 sm:text-4xl lg:h-11 lg:text-base border-2 border-gray-400'
          >
            <FcGoogle className='sm:text-6xl lg:text-base mr-5'/>
            Continue with Google
          </button>
          <button
            className='flex items-center justify-center sm:mb-14 lg:mb-5 rounded-xl sm:h-24 sm:text-4xl lg:h-11 lg:text-base border-2 border-gray-400'
          >
            <BsFacebook className='sm:text-6xl lg:text-base mr-5' />
            Continue with Facebook
          </button>
        </div>
        <p 
          className='text-center sm:mb-14 lg:mb-5 text-gray-400 sm:text-5xl lg:text-base'
        >
          or
        </p>
        <form
          onSubmit={login}
        >
          {
            userNotFound && 
            <Input 
              type='text'
              label='Email'
              value={values.email}
              handleChange={handleChange('email')}
              errorMessage = "There is no account registered with this email address."
            />
          }
          {
            !userNotFound && userNotVerified &&
            <Input 
              type='text'
              label='Email'
              value={values.email}
              handleChange={handleChange('email')}
              errorMessage = "You have not verified your email yet. Checkout your email inbox and spams for the verification link."
            />
          }{
            !userNotFound && !userNotVerified &&
            <Input 
              type='text'
              label='Email'
              value={values.email}
              handleChange={handleChange('email')}
              errorMessage={null}
            />
          }
          {
            incorrectPassword ? <Input 
              type='password'
              label='Password'
              showPassword={showPassword}
              setShowPassword={setShowPassword}
              value={values.password}
              handleChange={handleChange('password')}
              errorMessage = "Incorrect Password"
            /> 
            : 
            <Input 
              type='password'
              label='Password'
              showPassword={showPassword}
              setShowPassword={setShowPassword}
              value={values.password}
              handleChange={handleChange('password')}
              errorMessage={null}
            />
          }
          <small
            className='sm:text-3xl lg:text-sm flex justify-end cursor-pointer underline hover:text-blue-400'
          >
            password forgotten ?
          </small>
          <button
            className='font-bold sm:my-10 lg:my-3 rounded-xl bg-pink-600 w-full sm:h-28 sm:text-4xl lg:h-10 lg:text-base tracking-wide'
          >
            Sign in
          </button>
        </form>
        <div className='flex items-center justify-center'>
          <span className='sm:text-2xl lg:text-sm text-gray-400'>
            Don't have an account yet ? 
          </span>
          <span 
            className='sm:text-2xl lg:text-sm text-gray-200 cursor-pointer hover:text-blue-400 hover:underline ml-2'
            onClick={() => navigate('/sign-up')}
          >
            Create an account
          </span>
        </div>
      </div>
    </div>
  )
}

export default SignIn