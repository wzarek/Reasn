"use client";

import clsx from "clsx";
import React, { useState } from "react";

interface InputProps {
  type: string;
  label?: string;
  name?: string;
  defaultValue?: string;
  className?: string;
  onChange?: (value: string) => void;
  onFocus?: () => void;
  onBlur?: () => void;
}

export const FloatingInput = (props: InputProps) => {
  const {
    label,
    type,
    name,
    defaultValue,
    className,
    onFocus,
    onBlur,
    onChange,
  } = props;
  const [isFocused, setIsFocused] = useState(false);
  const [isFilled, setIsFilled] = useState(!!defaultValue);

  const handleFocus = () => {
    onFocus?.();
    setIsFocused(true);
  };

  const handleBlur = () => {
    onBlur?.();
    setIsFocused(false);
  };

  return (
    <div
      className={clsx(
        "relative rounded-xl bg-[#232327] p-1",
        {
          "bg-gradient-to-r from-[#32346A] to-[#4E4F75]": isFocused,
        },
        className,
      )}
    >
      <label
        className={clsx(
          "pointer-events-none absolute left-3 transition-all duration-300",
          {
            "top-[-1.5rem] text-xs":
              isFocused || isFilled || ["date", "time"].includes(type),
          },
          {
            "top-[20%] text-base":
              !isFocused && !isFilled && !["date", "time"].includes(type),
          },
        )}
      >
        {label ?? ""}
      </label>
      <input
        type={type}
        name={name}
        id={name}
        className="h-full w-full rounded-lg bg-[#232327] p-2 focus:outline-none"
        onFocus={handleFocus}
        onBlur={handleBlur}
        onChange={(e) => {
          setIsFilled(!!e.target.value);
          onChange?.(e.target.value);
        }}
        defaultValue={defaultValue}
      />
    </div>
  );
};

export const BaseInput = (props: InputProps) => {
  const {
    label,
    type,
    name,
    defaultValue,
    className,
    onFocus,
    onBlur,
    onChange,
  } = props;

  return (
    <div className={className}>
      <input
        type={type}
        name={name}
        id={name}
        className="h-full w-full rounded-lg bg-[#232327] p-2 focus:outline-none"
        onFocus={() => onFocus?.()}
        onBlur={() => onBlur?.()}
        onChange={(e) => {
          onChange?.(e.target.value);
        }}
        defaultValue={defaultValue}
        placeholder={label}
      />
    </div>
  );
};
