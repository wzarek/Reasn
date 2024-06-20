"use client";

import clsx from "clsx";
import React from "react";

interface ButtonProps {
  text: string;
  className?: string;
  background?: string;
  onClick: () => void;
}

export const ButtonBase = (props: ButtonProps) => {
  const { text, className, background, onClick } = props;
  return (
    <button
      onClick={onClick}
      className={clsx(
        className,
        "w-36 rounded-2xl px-4 py-2",
        { [background as string]: background },
        { "bg-gradient-to-r from-[#32346A] to-[#4E4F75]": !background },
      )}
    >
      {text}
    </button>
  );
};
