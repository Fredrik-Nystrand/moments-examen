/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    colors: {
      "grey-dark": "#202226",
      "grey-mid-1": "#2F3135",
      "grey-mid-2": "#363A3F",
      "grey-light": "#E9E9F0",
      "purple-dark": "#6F21E6",
      "purple-light": "#9E5EFF",
      "black-half": "rgb(0, 0, 0, 0.45)",
    },
    backgroundImage: ({ theme }) => ({
      gradient: "linear-gradient(114deg, #6F21E6 0%, #9E5EFF 100%)",
      gradientRev: "linear-gradient(114deg, #9E5EFF 0%, #6F21E6 100%)",
    }),
    container: {
      center: true,
    },
    extend: {},
  },
  plugins: [require("@tailwindcss/forms")],
}
