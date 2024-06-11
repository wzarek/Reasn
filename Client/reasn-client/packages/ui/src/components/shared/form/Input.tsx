"use client";

import clsx from "clsx";
import React, { useState } from "react";

interface InputProps {
  type: string;
  label?: string;
  name?: string;
  onFocus?: () => void;
  onBlur?: () => void;
}

export const FloatingInput = (props: InputProps) => {
  const [isFocused, setIsFocused] = useState(false);
  const [isFilled, setIsFilled] = useState(false);
  const { label, type, name, onFocus, onBlur } = props;

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
      className={clsx("relative rounded-xl bg-[#232327] p-1", {
        "bg-gradient-to-r from-[#32346A] to-[#4E4F75]": isFocused,
      })}
    >
      <label
        className={clsx(
          "absolute left-3 transition-all duration-300",
          { "top-[-50%] text-xs": isFocused || isFilled },
          { "top-3 text-base": !isFocused && !isFilled },
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
        onChange={(e) => setIsFilled(!!e.target.value)}
      />
    </div>
  );
};
