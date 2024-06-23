"use client";

import {
  ButtonBase,
  FloatingInput,
} from "@reasn/ui/src/components/shared/form";
import React, { useRef, useState, ChangeEvent } from "react";
import { useFormState } from "react-dom";
import { registerAction } from "@/app/register/action";
import { UserRole } from "@reasn/common/src/enums/schemasEnums";

const RegisterUser = () => {
  const initialState = { message: null, errors: {} };
  const [currentStep, setCurrentStep] = useState(1);
  const [state, formAction] = useFormState(registerAction, initialState);
  const [formData, setFormData] = useState({
    name: "",
    surname: "",
    email: "",
    username: "",
    password: "",
    confirmPassword: "",
    phone: "",
    country: "",
    city: "",
    street: "",
    state: "",
    postcode: "",
    role: UserRole.USER,
  });

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const formRef = useRef<HTMLFormElement>(null);

  return (
    <>
      <div className="flex w-full items-center justify-center sm:w-1/2">
        <form
          className="flex w-full flex-col gap-8"
          ref={formRef}
          action={formAction}
        >
          <FloatingInput
            type={currentStep === 1 ? "text" : "hidden"}
            label="imię"
            name="name"
            defaultValue={formData.name}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 1 ? "text" : "hidden"}
            label="nazwisko"
            name="surname"
            defaultValue={formData.surname}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 1 ? "text" : "hidden"}
            label="nazwa użytkownika"
            name="username"
            defaultValue={formData.username}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 1 ? "email" : "hidden"}
            label="email"
            name="email"
            defaultValue={formData.email}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 1 ? "tel" : "hidden"}
            label="numer telefonu"
            name="phone"
            defaultValue={formData.phone}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 1 ? "password" : "hidden"}
            label="hasło"
            name="password"
            defaultValue={formData.password}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 1 ? "password" : "hidden"}
            label="powtórz hasło"
            name="confirmPassword"
            defaultValue={formData.confirmPassword}
            onChange={handleChange}
          />
          <input type="hidden" name="role" defaultValue={formData.role} />
          <FloatingInput
            type={currentStep === 2 ? "text" : "hidden"}
            label="miasto"
            name="city"
            defaultValue={formData.city}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 2 ? "text" : "hidden"}
            label="kraj"
            name="country"
            defaultValue={formData.country}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 2 ? "text" : "hidden"}
            label="ulica"
            name="street"
            defaultValue={formData.street}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 2 ? "text" : "hidden"}
            label="województwo"
            name="state"
            defaultValue={formData.state}
            onChange={handleChange}
          />
          <FloatingInput
            type={currentStep === 2 ? "text" : "hidden"}
            label="kod pocztowy"
            name="postcode"
            defaultValue={formData.postcode}
            onChange={handleChange}
          />
        </form>
      </div>
      <div className="relative z-10 flex h-fit w-full flex-col flex-wrap items-end justify-center gap-10 sm:h-full sm:w-1/3 sm:items-start sm:gap-24">
        <p className="bg-gradient-to-r from-[#FF6363] to-[#1E34FF] bg-clip-text text-right text-4xl font-bold leading-tight text-transparent sm:text-left lg:text-5xl">
          {currentStep === 1
            ? "znalazłeś już swój powód do spotkań?"
            : "gdzie powinniśmy cię szukać?"}
        </p>
        <ButtonBase
          text={currentStep === 2 ? "zarejestruj" : "kontynuuj"}
          onClick={() =>
            currentStep === 2
              ? formRef.current?.requestSubmit()
              : setCurrentStep(currentStep + 1)
          }
        />
      </div>
      <div className="absolute right-[-50%] top-0 z-0 h-full w-4/5 rounded-full bg-[#000b6d] opacity-15 blur-3xl"></div>
    </>
  );
};

export default RegisterUser;
