import React from "react";

export const Navbar = () => {
  return (
    <nav className="flex h-16 w-full flex-wrap items-center justify-between rounded-b-2xl bg-black text-base text-white">
      <div className="mx-auto flex w-4/5 justify-between">
        <div>
          <a href="#" className="text-2xl font-bold hover:text-[#ccc]">
            Reasn.
          </a>
        </div>
        <div className="flex items-center gap-8">
          <a href="#" className="hover:text-[#ccc]">
            logowanie
          </a>
          <a
            href="#"
            className="rounded-2xl border-2 border-white px-6 py-1.5 hover:bg-white hover:text-black"
          >
            rejestracja
          </a>
        </div>
      </div>
    </nav>
  );
};
