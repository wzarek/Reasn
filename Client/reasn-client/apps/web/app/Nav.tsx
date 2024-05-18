import React from "react";
import styles from './Nav.module.css';


type Link = {
    label: string
    href: string
};

const Nav = () => {
return(
<body>
  <header>
    <nav
      className="
        flex flex-wrap
        items-center
        justify-between
        w-full
        h-20
        text-lg text-white
        bg-black
        rounded-b-2xl
      "
    >
    <div>
        <a className="block text-white font-bold text-3xl mx-60">Reasn.</a>
    </div>

    <div className="flex space-x-4 items-center mx-60"> 
      <a className="block text-white mr-8">logowanie</a>   
      <a className="px-8 py-1.5 text-white border-2 border-white rounded-full">rejestracja</a> 
    </div>
    </nav>
  </header>
</body>
)
}
export default Nav