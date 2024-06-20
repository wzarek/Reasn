"use client";

import clsx from "clsx";
import React from "react";

interface ButtonProps {
  text: string;
  className?: string;
  onClick: () => void;
}

export const ButtonBase = (props: ButtonProps) => {
  const { text, className, onClick } = props;
  return (
    <button
      onClick={onClick}
      className={clsx(
        "w-36 rounded-2xl bg-gradient-to-r from-[#32346A] to-[#4E4F75] px-4 py-2",
        className,
      )}
    >
      {text}
    </button>
  );
};
