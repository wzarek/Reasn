import React from "react";

export const CTASection = () => {
  return (
    <div className="mx-auto flex w-[80%] flex-col flex-wrap justify-between gap-5 py-36 sm:flex-row lg:w-[55%]">
      <h3 className="w-full text-xl font-semibold">
        Nasze eventy, Twój powód do spotkań.
      </h3>
      <div className="group flex h-80 w-full cursor-pointer flex-col justify-between rounded-3xl bg-gradient-to-t from-[#2e2e3460] to-[#23232660] p-10 sm:w-[35%]">
        <div className="h-4/5 w-full rounded-xl border border-[#3E3E44] duration-500 group-hover:h-3/4"></div>
        <p className="text-lg font-semibold duration-500 group-hover:text-3xl">
          Dodaj swój event
        </p>
      </div>
      <div className="group relative flex h-80 w-full cursor-pointer flex-col justify-center overflow-clip rounded-3xl bg-gradient-to-r from-[#32346A] to-[#4E4F75] p-10 sm:w-[60%]">
        <p className="z-10 text-6xl font-semibold text-[#828FFF] duration-500 [text-shadow:_0_2px_5px_rgb(0_0_0_/_40%)] group-hover:text-7xl">
          Zobacz <br /> istniejące eventy
        </p>
        <div className="absolute right-0 top-1/2 h-4/5 w-3/5 -translate-y-1/2 rounded-l-xl bg-black duration-500 group-hover:h-full group-hover:w-2/3"></div>
      </div>
    </div>
  );
};
