"use client";

import {
  ButtonBase,
  FloatingInput,
} from "@reasn/ui/src/components/shared/form";
import React, { useRef, useState } from "react";

const RegisterOrganizer = () => {
  const [currentStep, setCurrentStep] = useState(1);
  const formRef = useRef<HTMLFormElement>(null);

  const handleFormSubmit = () => {
    console.log("form submitted");
    formRef.current?.submit();
  };

  return (
    <>
      <div className="relative z-10 flex w-full items-center justify-center sm:w-1/2">
        <form className="flex w-full flex-col gap-8" ref={formRef}>
          {currentStep === 1 && (
            <>
              <FloatingInput type="text" label="imię" name="name" />
              <FloatingInput type="text" label="nazwisko" name="surname" />
              <FloatingInput
                type="text"
                label="nazwa użytkownika"
                name="username"
              />
              <FloatingInput type="email" label="email" name="email" />
              <FloatingInput type="tel" label="numer telefonu" name="phone" />
              <FloatingInput type="password" label="hasło" name="password" />
              <FloatingInput
                type="password"
                label="powtórz hasło"
                name="confirm-password"
              />
            </>
          )}
          {currentStep === 2 && (
            <>
              <FloatingInput type="text" label="miasto" name="city" />
              <FloatingInput type="text" label="kraj" name="country" />
              <FloatingInput type="text" label="ulica" name="street" />
              <FloatingInput type="text" label="województwo" name="state" />
              <FloatingInput type="text" label="kod pocztowy" name="postcode" />
            </>
          )}
        </form>
      </div>
      <div className="relative z-10 flex h-fit w-full flex-col flex-wrap items-end justify-center gap-10 sm:h-full sm:w-1/3 sm:items-start sm:gap-24">
        {currentStep === 1 && (
          <p className="bg-gradient-to-r from-[#FF6363] to-[#1E34FF] bg-clip-text text-right text-4xl font-bold leading-tight text-transparent sm:text-left lg:text-5xl">
            to jak, zorganizujesz nam coś?
          </p>
        )}
        {currentStep === 2 && (
          <p className="bg-gradient-to-r from-[#FF6363] to-[#1E34FF] bg-clip-text text-right text-4xl font-bold leading-tight text-transparent sm:text-left lg:text-5xl">
            gdzie możemy cię znaleźć?
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

export default RegisterOrganizer;
