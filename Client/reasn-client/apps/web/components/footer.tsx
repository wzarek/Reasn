import React from "react";

export const Footer = () => {
  return (
    <footer className="fixed bottom-0 w-full rounded-t-2xl bg-black bg-bottom py-16 text-center">
      <div className="mx-auto flex w-4/5 items-center justify-between">
        <div className="mx-2 mt-1 h-1 flex-grow rounded-3xl bg-gradient-to-r from-red-400 via-purple-500 to-indigo-600"></div>
        <span className="text-3xl font-bold text-white">we are</span>
        <div className="mx-2 mt-1 h-1 flex-grow rounded-3xl bg-gradient-to-r from-indigo-600 via-purple-500 to-red-400"></div>
      </div>
      <div className="-mt-1 text-3xl font-bold text-white">the Reasn.</div>
      <div className="mx-auto -mt-8 flex w-3/4 justify-between">
        <div className="flex w-1/6 justify-between text-white">
          <a href="#" className="hover:text-[#ccc]">
            link
          </a>
          <a href="#" className="hover:text-[#ccc]">
            link
          </a>
          <a href="#" className="hover:text-[#ccc]">
            link
          </a>
        </div>
        <div className="flex w-1/6 justify-between text-white">
          <a href="#" className="hover:text-[#ccc]">
            link
          </a>
          <a href="#" className="hover:text-[#ccc]">
            link
          </a>
          <a href="#" className="hover:text-[#ccc]">
            link
          </a>
        </div>
      </div>
    </footer>
  );
};
