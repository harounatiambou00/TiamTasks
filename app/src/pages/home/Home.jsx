import React, {useContext} from 'react';

import { Navbar } from '../../components/home-page-components';

import {BsExclamationLg} from 'react-icons/bs';

import {Button} from '@mui/material';

import {useNavigate} from 'react-router-dom';

import { AppContext } from '../../context/AppContext';

const Home = () => {

  const navigate = useNavigate();

  const {user} = useContext(AppContext);
  if(user !== null)
    navigate("/user")

  return (
    <div
        className='h-full overflow-y-hidden'
    >
      <Navbar />
      <div
        className='flex flex-col items-center justify-center'
      >
        <div
          className='flex flex-col items-center mt-20'
        >
          <span
            className='text-4xl font-bold font-source_code_pro'
          >
            TiamTasks, Get it done
            <BsExclamationLg 
              className='text-pink-500 inline pb-2'
            />
          </span>
          <span className='mt-3 text-lg'>
            Keep track  of the daily tasks in life and <br />
            get that satisfaction upon completion.
          </span>
          <div
            className='flex justify-between items-center mt-5'
          >
            <Button
              variant='contained'
              className='bg-pink-700 mr-10 font-raleway'
              onClick={() => navigate('sign-up')}
            >
              Get Started
            </Button>
            <Button
              variant='contained'
              className='bg-dark-elevation ml-10 font-raleway'
            >
              Learn more
            </Button>
          </div>
        </div>
        <img 
          src={process.env.PUBLIC_URL + '/assets/images/landing-page-image.png'} alt='landing-page' 
          className='h-landing_page_img'
        />
      </div>
    </div>
  )
}

export default Home