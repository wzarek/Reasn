import React from "react";
import Link from "next/link";

export const Navbar = () => {
  return (
    <nav className="sticky top-0 z-[50] flex h-16 w-full flex-wrap items-center justify-between rounded-b-2xl bg-black text-base text-white">
      <div className="mx-auto flex w-4/5 justify-between">
        <div>
          <Link href="/" className="text-2xl font-bold hover:text-[#ccc]">
            Reasn.
          </Link>
        </div>
        <div className="flex items-center gap-8">
          <Link href="/login" className="hover:text-[#ccc]">
            logowanie
          </Link>
          <Link
            href="/register"
            className="rounded-2xl border-2 border-white px-6 py-1.5 hover:bg-white hover:text-black"
          >
            rejestracja
          </Link>
        </div>
      </div>
    </nav>
  );
};
