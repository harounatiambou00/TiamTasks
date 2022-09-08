import React from 'react';

import {IconButton} from '@mui/material';
import {AiOutlineEye, AiOutlineEyeInvisible} from 'react-icons/ai';

const Input = ({inputId, labelId, type, label, autofocus=false, value, handleChange, showPassword, setShowPassword, emailAlreadyExists, errorMessage}) => {



    if(type === 'password'){
        return (
            <div
                className='h-full w-full flex-col sm:mb-14 lg:mb-3'
            >
                <label
                    className='flex items-center justify-between sm:text-4xl lg:text-base px-3 font-bold text-gray-300'
                    htmlFor={inputId}
                    id={labelId}
                >
                    <span>
                        {label} 
                    </span>
                    <IconButton
                        className='text-gray-300 sm:text-6xl lg:text-xl'
                    >
                        {showPassword ? <AiOutlineEyeInvisible onClick={() => setShowPassword(!showPassword)} /> : <AiOutlineEye onClick={() => setShowPassword(!showPassword)} />}
                    </IconButton>
                </label>
                <input 
                    {...autofocus && 'autoFocus'}
                    id={inputId}
                    type={showPassword? 'text' : 'password'}
                    value={value}
                    onChange={handleChange}
                    className='w-full bg-inherit border-2 border-gray-400 text-gray-400 sm:text-3xl lg:text-base pl-3 rounded-xl sm:h-24 lg:h-11 outline-none focus:border-fuchsia-700 hover:border-fuchsia-700'
                />
                <span className='text-red-400 sm:text-2xl lg:text-sm'>{errorMessage !== null && errorMessage }</span>
            </div>
        )
    }
    return (
        <div
            className='h-full w-full flex-col sm:mb-14 lg:mb-3'
        >
            <label
                className='sm:text-4xl lg:text-base pl-3 font-bold text-gray-300'
                htmlFor={inputId}
                id={labelId}
            >
                {label}
            </label>
            <input 
                {...autofocus && 'autoFocus'}
                id={inputId}
                type={type}
                value={value}
                onChange={handleChange}
                className='w-full bg-inherit border-2 border-gray-400 text-gray-400 sm:text-3xl lg:text-base pl-3 rounded-xl sm:h-24 lg:h-11 outline-none focus:border-fuchsia-700 hover:border-fuchsia-700'
            />
            <span className='text-red-400 sm:text-2xl lg:text-sm'>{errorMessage !== null && errorMessage }</span>
        </div>
    )
}

export default Input