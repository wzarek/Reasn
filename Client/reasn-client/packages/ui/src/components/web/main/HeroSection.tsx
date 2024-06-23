import React, { useState } from "react";
import { ArrowLeft, ArrowRight, Fire } from "@reasn/ui/src/icons";
import { Card, CardVariant } from "@reasn/ui/src/components/shared";

export const HeroSection = () => {
  const [currentCardIdx, setCurrentCardIdx] = useState(0);
  const [cards, setCards] = useState<number[]>([0, 1, 2, 3, 4, 5]);

  return (
    <section className="flex h-[50vh] w-full flex-col items-center justify-center gap-[10%] lg:flex-row">
      <div className="relative z-10 hidden h-1/3 w-fit lg:block">
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
      <div className="relative h-[30vh] w-[30vw] ">
        <div className='absolute right-[-25%] top-[-50%] z-0 h-[200%] w-[150%] rounded-full bg-gradient-to-r from-[#FF6363] to-[#1E34FF] opacity-5 blur-3xl content-[""]'></div>
        <div className="w-full h-full overflow-hidden">
          <div
            className="flex h-full w-full gap-0 duration-300"
            style={{ transform: `translateX(-${currentCardIdx * 100}%)` }}
          >
            {cards.map((card, idx) => (
              <Card key={card} variant={CardVariant.Big} event={"edit"} />
            ))}
          </div>
        </div>
        {currentCardIdx > 0 && (
          <ArrowLeft
            onClick={() => setCurrentCardIdx(currentCardIdx - 1)}
            className="absolute left-[-3rem] top-[50%] z-20 h-8 w-8 -translate-y-1/2 cursor-pointer rounded-lg bg-gradient-to-r from-[#32346A7d] to-[#4E4F757d] fill-white p-2"
          />
        )}
        {currentCardIdx < cards.length - 1 && (
          <ArrowRight
            onClick={() => setCurrentCardIdx((idx) => idx + 1)}
            className="absolute right-[-3rem] top-[50%] z-20 h-8 w-8 -translate-y-1/2 cursor-pointer rounded-lg bg-gradient-to-r from-[#32346A7d] to-[#4E4F757d] fill-white p-2"
          />
        )}
      </div>
    </section>
  );
};
