"use client";

import React from "react";

interface InterestProps {
  text: string;
  onDelete: () => void;
}

export const Interest = (props: InterestProps) => {
  const { text, onDelete } = props;

  return (
    <div className="flex flex-row items-center gap-3 rounded-xl bg-[#232326] px-4 py-2">
      <p>{text}</p>
      <button onClick={onDelete} className="text-[#ccc]">
        x
      </button>
    </div>
  );
};
