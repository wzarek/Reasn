"use client";

import {
  ButtonBase,
  FloatingInput,
} from "@reasn/ui/src/components/shared/form";
import React, { useRef, useState } from "react";

const RegisterUser = () => {
  const [currentStep, setCurrentStep] = useState(1);
  const formRef = useRef<HTMLFormElement>(null);

  const handleFormSubmit = () => {
    console.log("form submitted");
    formRef.current?.submit();
  };

  return (
    <>
      <div className="flex w-1/2 items-center justify-center">
        <form className="flex w-full flex-col gap-8" ref={formRef}>
          {currentStep === 1 && (
            <>
              <FloatingInput type="text" label="imię" />
              <FloatingInput type="text" label="nazwisko" />
              <FloatingInput type="text" label="nazwa użytkownika" />
              <FloatingInput type="email" label="email" />
              <FloatingInput type="tel" label="numer telefonu" />
              <FloatingInput type="password" label="hasło" />
            </>
          )}
          {currentStep === 2 && (
            <>
              <FloatingInput type="text" label="miasto" />
              <FloatingInput type="text" label="kraj" />
              <FloatingInput type="text" label="ulica" />
              <FloatingInput type="text" label="województwo" />
              <FloatingInput type="text" label="kod pocztowy" />
            </>
          )}
        </form>
      </div>
      <div className="z-10 flex h-full w-1/3 flex-col flex-wrap items-start justify-center gap-24">
        {currentStep === 1 && (
          <p className="bg-gradient-to-r from-[#FF6363] to-[#1E34FF] bg-clip-text text-5xl font-bold leading-tight text-transparent">
            to jak, zorganizujesz <br /> nam coś?
          </p>
        )}
        {currentStep === 2 && (
          <p className="bg-gradient-to-r from-[#FF6363] to-[#1E34FF] bg-clip-text text-5xl font-bold leading-tight text-transparent">
            gdzie możemy cię <br /> znaleźć?
          </p>
        )}
        <ButtonBase
          text={currentStep === 2 ? "zarejestruj" : "kontynuuj"}
          onClick={() =>
            currentStep === 2
              ? handleFormSubmit()
              : setCurrentStep(currentStep + 1)
          }
        />
      </div>
      <div className="absolute right-[-50%] top-0 z-0 h-full w-4/5 rounded-full bg-[#000b6d] opacity-15 blur-3xl"></div>
    </>
  );
};

export default RegisterUser;
