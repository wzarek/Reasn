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
      <div className="flex w-1/2 items-center justify-center">
        <form className="flex w-full flex-col gap-8" ref={formRef}>
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
      <div className="z-10 flex h-full w-1/3 flex-col flex-wrap items-start justify-center gap-24">
        <p className="bg-gradient-to-r from-[#FF6363] to-[#1E34FF] bg-clip-text text-5xl font-bold leading-tight text-transparent">
          miło, że do nas wracasz
        </p>
        <ButtonBase text={"zaloguj"} onClick={handleFormSubmit} />
      </div>
      <div className="absolute right-[-50%] top-0 z-0 h-full w-4/5 rounded-full bg-[#000b6d] opacity-15 blur-3xl"></div>
    </>
  );
};

export default LoginPage;
