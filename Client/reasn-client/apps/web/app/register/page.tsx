"use client";

import { useRouter } from "next/navigation";
import React from "react";

const RegisterMiddleware = () => {
  const router = useRouter();

  return (
    <div className="flex h-4/5 w-full flex-col items-center justify-evenly gap-8">
      <h2 className="w-full text-5xl font-bold">kim jesteś?</h2>
      <div className="flex w-full gap-8">
        <div
          className="group flex h-80 w-[35%] cursor-pointer flex-col justify-between rounded-3xl bg-gradient-to-t from-[#2e2e3460] to-[#23232660] p-10"
          onClick={() => router.push("/register/organizer")}
        >
          <div className="h-4/5 w-full rounded-xl border border-[#3E3E44] duration-500 group-hover:h-3/4"></div>
          <p className="text-lg font-semibold duration-500 group-hover:text-3xl">
            Organizator
          </p>
        </div>
        <div
          className="group relative flex h-80 w-[60%] cursor-pointer flex-col justify-center overflow-clip rounded-3xl bg-gradient-to-r from-[#32346A] to-[#4E4F75] p-10"
          onClick={() => router.push("/register/user")}
        >
          <p className="z-10 text-6xl font-semibold text-[#828FFF] duration-500 [text-shadow:_0_2px_5px_rgb(0_0_0_/_40%)] group-hover:text-7xl">
            Uczestnik
          </p>
          <div className="absolute right-0 top-1/2 h-4/5 w-3/5 -translate-y-1/2 rounded-l-xl bg-black duration-500 group-hover:h-full group-hover:w-2/3"></div>
        </div>
      </div>
    </div>
  );
};

export default RegisterMiddleware;
