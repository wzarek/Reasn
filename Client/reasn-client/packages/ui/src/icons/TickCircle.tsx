import React from "react";

export const TickCircle = (props: IconProps) => {
  const { className, colors, gradientTransform } = props;

  return (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      width="16"
      height="16"
      className={className}
      viewBox="0 0 24 24"
      fill="url(#gradient1)"
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
      <path d="M12 2c5.523 0 10 4.477 10 10s-4.477 10-10 10S2 17.523 2 12 6.477 2 12 2Zm3.22 6.97-4.47 4.47-1.97-1.97a.75.75 0 0 0-1.06 1.06l2.5 2.5a.75.75 0 0 0 1.06 0l5-5a.75.75 0 1 0-1.06-1.06Z" />
    </svg>
  );
};
