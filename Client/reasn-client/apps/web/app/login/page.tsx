"use client";

import {
  ButtonBase,
  FloatingInput,
} from "@reasn/ui/src/components/shared/form";
import Link from "next/link";
import React, { useRef } from "react";

const LoginPage = () => {
  const formRef = useRef<HTMLFormElement>(null);

  const handleFormSubmit = () => {
    console.log("form submitted");
    formRef.current?.submit();
  };

  return (
    <>
      <div className="relative z-10 flex w-full items-center justify-center sm:w-1/2">
        <form className="flex w-full flex-col gap-2 sm:gap-8" ref={formRef}>
          <FloatingInput type="email" label="email" name="email" />
          <FloatingInput type="password" label="hasło" name="password" />
          <div className="flex justify-end gap-2 text-sm">
            <p>nie masz konta?</p>
            <Link href="/register" className="hover:text-[#ccc]">
              zarejestruj się
            </Link>
          </div>
        </form>
      </div>
      <div className="relative z-10 flex h-fit w-full flex-col flex-wrap items-end justify-center gap-10 sm:h-full sm:w-1/3 sm:items-start sm:gap-24">
        <p className="bg-gradient-to-r from-[#FF6363] to-[#1E34FF] bg-clip-text text-right text-4xl font-bold leading-tight text-transparent sm:text-left lg:text-5xl">
          miło, że do nas wracasz
        </p>
        <ButtonBase text={"zaloguj"} onClick={handleFormSubmit} />
      </div>
      <div className="absolute right-[-50%] top-0 z-0 h-full w-4/5 rounded-full bg-[#000b6d] opacity-15 blur-3xl"></div>
    </>
  );
};

export default LoginPage;
