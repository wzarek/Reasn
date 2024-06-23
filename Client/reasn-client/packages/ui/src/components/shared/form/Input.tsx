"use client";

import clsx from "clsx";
import React, { useState, ChangeEvent } from "react";

interface InputProps {
  type: string;
  label?: string;
  name?: string;
  defaultValue?: string;
  className?: string;
  onChange?: (value: ChangeEvent<HTMLInputElement>) => void;
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
        "relative rounded-xl bg-[#232327]",
        {
          "bg-gradient-to-r from-[#32346A] to-[#4E4F75]": isFocused,
        },
        {
          "p-1": type !== "hidden",
        },
        {
          "hidden h-0 p-0": type === "hidden",
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
          {
            hidden: type === "hidden",
          },
        )}
      >
        {label ?? ""}
      </label>
      <input
        type={type}
        name={name}
        id={name}
        className={clsx(
          "w-full rounded-lg bg-[#232327] focus:outline-none",
          {
            "h-0 p-0": type === "hidden",
          },
          {
            "h-full p-2": type !== "hidden",
          },
        )}
        onFocus={handleFocus}
        onBlur={handleBlur}
        onChange={(e) => {
          setIsFilled(!!e.target.value);
          onChange?.(e);
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
          onChange?.(e);
        }}
        defaultValue={defaultValue}
        placeholder={label}
      />
    </div>
  );
};
