/** @type {import('tailwindcss').Config} */
module.exports = {
  important: true,
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'light':{

        },
        'dark': {
          'background': '#181820',
          'elevation': '#21212b',
        }
      },
      fontFamily: {
        raleway: ['Raleway', 'sans-serif'],
        source_code_pro: ['Source Sans Pro', 'sans-serif'],
      },
      height:{
        landing_page_img : "40rem",
      }
    },
  },
  plugins: [],
}