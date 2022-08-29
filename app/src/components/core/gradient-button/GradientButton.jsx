import React from 'react';


const GradientButton = ({label, action}) => {
  return (
    <button
        className='h-full w-full rounded-xl font-bold'
        onClick={action}
    >
        {label}
    </button>
  )
}

export default GradientButton