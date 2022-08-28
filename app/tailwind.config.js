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
          'background': '#040406',
          'elevation': '#202020',
        }
      },
      fontFamily: {
        raleway: ['Raleway', 'sans-serif'],
        source_code_pro: ['Source Sans Pro', 'sans-serif'],
      },
    },
  },
  plugins: [],
}
