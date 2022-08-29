import React from 'react';

import {Button} from '@mui/material';
import {Input} from '../../components/core';
import {FcGoogle} from 'react-icons/fc';
import {BsFacebook} from 'react-icons/bs';

import {useNavigate}  from 'react-router-dom';


const SignUp = () => {

  const [showPassword, setShowPassword] = React.useState(false);
  const navigate = useNavigate();
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
          Sign Up
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
        <form>
          <Input 
            type='text'
            label='Name'
          />
          <Input 
            type='text'
            label='Email'
          />
          <Input 
            type='password'
            label='Password'
            showPassword={showPassword}
            setShowPassword={setShowPassword}
          />
          <button
            className='font-bold sm:my-10 lg:my-3 rounded-xl bg-pink-600 w-full sm:h-28 sm:text-4xl lg:h-10 lg:text-base tracking-wide'
          >
            Sign up
          </button>
        </form>
        <div className='flex items-center justify-center'>
          <span className='sm:text-2xl lg:text-sm text-gray-400'>
            Already registered ? 
          </span>
          <span 
            className='sm:text-2xl lg:text-sm text-gray-200 cursor-pointer hover:text-blue-400 hover:underline ml-2'
            variant='text'
            onClick={() => navigate('/sign-in')}
          >
            Login
          </span>
        </div>
      </div>
    </div>
  )
}

export default SignUp