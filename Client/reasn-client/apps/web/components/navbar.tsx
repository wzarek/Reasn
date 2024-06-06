import React from "react";

export const Navbar = () => {
  return (
    <nav
      className="flex flex-wrap items-center justify-between w-full h-16 text-base text-white bg-black rounded-b-2xl"
    >
      <div className="flex w-4/5 justify-between mx-auto">
        <div>
          <a href="#" className="font-bold text-2xl hover:text-[#ccc]">Reasn.</a>
        </div>
        <div className="flex items-center gap-8">
          <a href="#" className="hover:text-[#ccc]">logowanie</a>
          <a href="#" className="px-6 py-1.5 border-2 border-white rounded-2xl hover:bg-white hover:text-black">
            rejestracja
          </a>
        </div>
      </div>
    </nav>
  )
}