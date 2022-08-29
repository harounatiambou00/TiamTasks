import React from 'react';

import {Button} from '@mui/material';

import {useNavigate} from 'react-router-dom';

const Navbar = () => {

    const navigate = useNavigate();

  return (
    <div
        className='py-4 px-28 flex justify-between items-center'
    >
        <div
            className='flex items-center'
        >
            <img 
                src={process.env.PUBLIC_URL + '/assets/images/logo.png'} alt='logo' 
                className='h-10'
            /> 
            <h4
                className='text-2xl'
            >
                
            </h4>
        </div>
        <div>
            <div>
                <Button
                    className='text-gray-300'
                    color='secondary'
                    onClick={() => navigate('sign-in')}
                >
                    Login
                </Button>
                <Button
                    className='ml-3 text-gray-300 border-gray-300 rounded-lg'
                    variant='outlined'
                    onClick={() => navigate('sign-up')}
                >
                    Sign Up
                </Button>
            </div>
        </div>
    </div>
  )
}

export default Navbar