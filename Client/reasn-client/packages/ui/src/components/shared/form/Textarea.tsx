"use client";

import clsx from "clsx";
import React, { useState } from "react";

interface InputProps {
  label?: string;
  name?: string;
  defaultValue?: string;
  className?: string;
  onFocus?: () => void;
  onBlur?: () => void;
}

export const FloatingTextarea = (props: InputProps) => {
  const { label, name, defaultValue, className, onFocus, onBlur } = props;
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
          "absolute left-3 transition-all duration-300",
          { "top-[-1.5rem] text-xs": isFocused || isFilled },
          { "top-3 text-base": !isFocused && !isFilled },
        )}
      >
        {label ?? ""}
      </label>
      <textarea
        name={name}
        id={name}
        className="h-full w-full resize-none rounded-lg bg-[#232327] p-2 focus:outline-none"
        onFocus={handleFocus}
        onBlur={handleBlur}
        onChange={(e) => setIsFilled(!!e.target.value)}
        defaultValue={defaultValue}
      />
    </div>
  );
};
