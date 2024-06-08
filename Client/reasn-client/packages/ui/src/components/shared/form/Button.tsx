import React from "react";

interface ButtonProps {
  text: string;
  onClick: () => void;
}

export const ButtonBase = (props: ButtonProps) => {
  const { text, onClick } = props;
  return (
    <button
      onClick={onClick}
      className="w-36 rounded-2xl bg-gradient-to-r from-[#32346A] to-[#4E4F75] px-4 py-2"
    >
      {text}
    </button>
  );
};
