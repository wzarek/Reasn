import React from "react";
import { IconProps } from "./IconProps";

export const Add = (props: IconProps) => {
  const { className, colors, gradientTransform, onClick } = props;

  return (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      width="16"
      height="16"
      className={className}
      viewBox="0 0 24 24"
      fill="url(#gradient1)"
      onClick={onClick}
    >
      <defs>
        <linearGradient
          id="gradient1"
          x1="0%"
          y1="0%"
          x2="100%"
          y2="0%"
          gradientTransform={gradientTransform}
        >
          {colors?.map((color, index) => (
            <stop
              key={index}
              offset={`${
                index + 1 < colors.length ? index * (100 / colors.length) : 100
              }%`}
              style={{ stopColor: color, stopOpacity: 1 }}
            />
          ))}
        </linearGradient>
      </defs>
      <path d="M11.883 3.007 12 3a1 1 0 0 1 .993.883L13 4v7h7a1 1 0 0 1 .993.883L21 12a1 1 0 0 1-.883.993L20 13h-7v7a1 1 0 0 1-.883.993L12 21a1 1 0 0 1-.993-.883L11 20v-7H4a1 1 0 0 1-.993-.883L3 12a1 1 0 0 1 .883-.993L4 11h7V4a1 1 0 0 1 .883-.993L12 3l-.117.007Z" />
    </svg>
  );
};
