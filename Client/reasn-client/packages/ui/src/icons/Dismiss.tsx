import React from "react";
import { IconProps } from "./IconProps";

export const Dismiss = (props: IconProps) => {
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
      <path d="m4.21 4.387.083-.094a1 1 0 0 1 1.32-.083l.094.083L12 10.585l6.293-6.292a1 1 0 1 1 1.414 1.414L13.415 12l6.292 6.293a1 1 0 0 1 .083 1.32l-.083.094a1 1 0 0 1-1.32.083l-.094-.083L12 13.415l-6.293 6.292a1 1 0 0 1-1.414-1.414L10.585 12 4.293 5.707a1 1 0 0 1-.083-1.32l.083-.094-.083.094Z" />
    </svg>
  );
};
