/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./app/**/*.{js,jsx,ts,tsx}",
    "../../packages/ui/src/**/*.{js,jsx,ts,tsx}",
    "./app/**/*.{js,jsx,ts,tsx}",
    "./components/**/*.{js,jsx,ts,tsx}",
    "../../packages/ui/src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      screens: {
        xs: "460px",
      },
      keyframes: {
        fadeOutRight: {
          "0%": { transform: "translateX(-50%) rotate(0deg)", opacity: 1 },
          "100%": { transform: "translateX(100%) rotate(30deg)", opacity: 0 },
        },
        fadeOutLeft: {
          "0%": { transform: "translateX(-50%) rotate(0deg)", opacity: 1 },
          "100%": { transform: "translateX(-150%) rotate(-30deg)", opacity: 0 },
        },
        fadeOutDown: {
          "0%": { transform: "translateX(-50%) translateY(0%)", opacity: 1 },
          "100%": {
            transform: "translateX(-50%) translateY(-100%)",
            opacity: 0,
          },
        },
      },
      animation: {
        fadeOutRight: "fadeOutRight 0.5s ease-in-out forwards",
        fadeOutLeft: "fadeOutLeft 0.5s ease-in-out forwards",
        fadeOutDown: "fadeOutDown 0.5s ease-in-out forwards",
      },
    },
  },
  plugins: [],
};
