import React from 'react';

import {Input} from '../../components/core';
import {FcGoogle} from 'react-icons/fc';
import {BsFacebook} from 'react-icons/bs';

import {useNavigate}  from 'react-router-dom';


const SignUp = () => {

  const [showPassword, setShowPassword] = React.useState(false);
  const [emailAlreadyExists, setEmailAlreadyExists] = React.useState(false);
  const navigate = useNavigate();

  const [values, setValues] = React.useState({
    firstName: "",
    lastName: "",
    email: "",
    password: ""
  });

  const handleChange = (prop) => (event) => {
    setValues({ ...values, [prop]: event.target.value });
  };

  const register = async (e) => {
    e.preventDefault();
    const url = 'https://localhost:7122/api/user/sign-up';
    let user = {
      firstName: values.firstName,
      lastName: values.lastName,
      email: values.email,
      password: values.password
    }
    let response = await fetch(url,{
      method : "POST",
      body : JSON.stringify(user),
      credentials : 'include',
      headers : {
        "Content-Type": "application/json",
      }
    });

    let content = await response.json();
    if(content.success){
      setEmailAlreadyExists(false);
      let verificationToken = content.data;
      console.log(content);
    }else{
      if(content.message === "EMAIL_ALREADY_EXISTS"){
          setEmailAlreadyExists(true);
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
        <form
          onSubmit={register}
        >
          <Input 
            type='text'
            label='First Name'
            value={values.firstName}
            handleChange={handleChange('firstName')}
          />
          <Input 
            type='text'
            label='Last Name'
            value={values.lastName}
            handleChange={handleChange('lastName')}
          />
          <Input 
            type='text'
            label='Email'
            value={values.email}
            handleChange={handleChange('email')}
            emailAlreadyExists={emailAlreadyExists}
          />
          <Input 
            type='password'
            label='Password'
            showPassword={showPassword}
            setShowPassword={setShowPassword}
            value={values.password}
            handleChange={handleChange('password')}
          />
          <button
            className='font-bold sm:my-10 lg:my-3 rounded-xl bg-pink-600 w-full sm:h-28 sm:text-4xl lg:h-10 lg:text-base tracking-wide'
            type='submit'
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