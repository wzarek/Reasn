import React from "react";
import { Fire } from "@reasn/ui/src/icons";
import { Card, CardVariant } from "@reasn/ui/src/components/shared";

export const HeroSection = () => {
  return (
    <section className="flex h-[50vh] w-full items-center justify-center gap-24">
      <div className="relative z-10 h-1/3 w-fit">
        <div className='absolute top-0 h-1.5 w-2/3 rounded-lg bg-gradient-to-r from-[#FF6363] to-[#1E34FF] content-[""]'></div>
        <Fire
          className="absolute right-0 top-0 h-14 w-14 translate-y-[-50%] rotate-[16deg]"
          colors={["#6E45C8", "#953A3D"]}
          gradientTransform="rotate(90)"
        />
        <h2 className="h-full content-center bg-gradient-to-r from-[#FF6363] to-[#1E34FF] bg-clip-text text-right text-5xl font-bold text-transparent">
          to jest teraz <br /> na topie
        </h2>
      </div>
      <div className="relative">
        <div className='absolute right-[-25%] top-[-50%] z-0 h-[200%] w-[150%] rounded-full bg-gradient-to-r from-[#FF6363] to-[#1E34FF] opacity-5 blur-3xl content-[""]'></div>
        <Card variant={CardVariant.Big} event={"edit"} />
      </div>
    </section>
  );
};
