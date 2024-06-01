import React from "react";

const Nav = () => {
return(

    <nav
      className="flex flex-wrap items-center justify-between w-full h-16 text-base text-white bg-black rounded-b-2xl"
    >
      <div className="flex w-3/4 justify-between ml-56 ">
        <div>
          <a href="#" className="font-bold text-2xl">Reasn.</a>
        </div>
        <div className="flex items-center gap-8">
          <a href="#">logowanie</a>
          <a href="#" className="px-6 py-1.5 border-2 border-white rounded-2xl">
            rejestracja
          </a>
        </div>
      </div>
    </nav>

)
}
export default Nav