import React from 'react'

export const Fire = (props: IconProps) => {
    const { className, colors, gradientTransform } = props

  return (
    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" className={className} viewBox="0 0 16 16" fill="url(#gradient1)">
        <defs>
            <linearGradient id="gradient1" x1="0%" y1="0%" x2="100%" y2="0%" gradientTransform={gradientTransform}>
              {colors?.map((color, index) => (
                <stop key={index} offset={`${index+1 < colors.length ? index * (100/colors.length) : 100}%`} style={{stopColor: color, stopOpacity: 1}} />
              ))}
            </linearGradient>
          </defs>
        <path d="M8 16c3.314 0 6-2 6-5.5 0-1.5-.5-4-2.5-6 .25 1.5-1.25 2-1.25 2C11 4 9 .5 6 0c.357 2 .5 4-2 6-1.25 1-2 2.729-2 4.5C2 14 4.686 16 8 16m0-1c-1.657 0-3-1-3-2.75 0-.75.25-2 1.25-3C6.125 10 7 10.5 7 10.5c-.375-1.25.5-3.25 2-3.5-.179 1-.25 2 1 3 .625.5 1 1.364 1 2.25C11 14 9.657 15 8 15"/>
    </svg>
  )
}
