import React from "react";

export const Footer = () => {
  return (
    <footer className="text-center py-16 bg-black bg-bottom fixed bottom-0 rounded-t-2xl w-full">
      <div className="flex items-center justify-between w-4/5 mx-auto">
        <div className="flex-grow h-1 rounded-3xl mx-2 mt-1 bg-gradient-to-r from-red-400 via-purple-500 to-indigo-600"></div>
        <span className="text-3xl font-bold text-white">we are</span>
        <div className="flex-grow h-1 rounded-3xl mx-2 mt-1 bg-gradient-to-r from-indigo-600 via-purple-500 to-red-400"></div>
      </div>
      <div className="text-3xl font-bold text-white -mt-1">the Reasn.</div>
      <div className="flex justify-between -mt-8 w-3/4 mx-auto">
        <div className="flex justify-between text-white w-1/6">
          <a href="#" className="hover:text-[#ccc]">link</a>
          <a href="#" className="hover:text-[#ccc]">link</a>
          <a href="#" className="hover:text-[#ccc]">link</a>
        </div>
        <div className="flex justify-between text-white w-1/6">
          <a href="#" className="hover:text-[#ccc]">link</a>
          <a href="#" className="hover:text-[#ccc]">link</a>
          <a href="#" className="hover:text-[#ccc]">link</a>
        </div>
      </div>
    </footer>
  )
}