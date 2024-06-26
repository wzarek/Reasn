"use client";

import React from "react";
import { useRouter } from "next/navigation";

export const MOCK_IMG_URL =
  "https://res.klook.com/images/fl_lossy.progressive,q_65/c_fill,w_1200,h_630/w_80,x_15,y_15,g_south_west,l_Klook_water_br_trans_yhcmh3/activities/tsah7c9evnal289z5fig/IMG%20Worlds%20of%20Adventure%20Admission%20Ticket%20in%20Dubai%20-%20Klook.jpg";

export enum CardVariant {
  Big,
  Tile,
  List,
  Swiping,
}

export interface CardProps {
  variant: CardVariant;
  event: string; // todo: add event after RSN-9
}

export const Card = (props: Readonly<CardProps>) => {
  const { variant, event } = props;
  const router = useRouter();

  const handleCardClick = () => {
    console.log("clicked");
    router.push(`/events/${event}`);
  };

  return (
    <div onClick={handleCardClick}>
      {variant === CardVariant.Big && <CardBig event={event} />}
      {variant === CardVariant.Tile && <CardTile event={event} />}
      {variant === CardVariant.List && <CardList event={event} />}
      {variant === CardVariant.Swiping && <CardSwiping event={event} />}
    </div>
  );
};

const CardBig = ({ event }: { event: string }) => {
  return (
    <div className="relative min-h-[30vh] min-w-[30vw] cursor-pointer overflow-hidden rounded-3xl">
      <img
        src={MOCK_IMG_URL}
        alt=""
        className="absolute left-0 top-0 h-full w-full object-cover"
      />
      <div className="relative flex h-[45%] w-full flex-col gap-2 bg-[#232326ee] p-4 text-[#F7F8F8]">
        <div className="flex gap-2 text-xs text-[#cacaca]">
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
        </div>
        <h2 className="w-1/2 break-words text-2xl font-semibold">
          Koncert fagaty na PWR w C-16
        </h2>
      </div>
      <div className="absolute right-4 top-0 flex h-[45%] flex-col justify-center gap-2 py-4">
        <span
          onClick={(e) => e.stopPropagation()}
          className="flex h-8 w-8 cursor-pointer items-center justify-center rounded-full bg-[#0f0f0f80] p-2 font-semibold text-green-500 duration-300 hover:bg-[#0f0f0f]"
        >
          +
        </span>
        <span
          onClick={(e) => e.stopPropagation()}
          className="flex h-8 w-8 cursor-pointer items-center justify-center rounded-full bg-[#0f0f0f80] p-2 font-semibold text-blue-400 duration-300 hover:bg-[#0f0f0f]"
        >
          ?
        </span>
      </div>
    </div>
  );
};

const CardTile = ({ event }: { event: string }) => {
  return (
    <div className="group relative h-64 w-48 cursor-pointer overflow-clip rounded-3xl">
      <img
        src={MOCK_IMG_URL}
        alt=""
        className="absolute left-0 top-0 h-full object-cover"
      />
      <div className="relative flex h-1/2 w-full flex-col gap-2 bg-[#232326ee] p-4 text-[#F7F8F8]">
        <div className="flex flex-wrap gap-1 text-xs text-[#cacaca]">
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
        </div>
        <h2 className="w-4/5 break-words text-base font-semibold">
          Koncert fagaty na PWR w C-16
        </h2>
      </div>
      <div className="absolute bottom-0 left-0 hidden w-full flex-row justify-center gap-2 bg-[#232326ee] py-2 group-hover:flex">
        <span
          onClick={(e) => e.stopPropagation()}
          className="flex h-6 w-6 cursor-pointer items-center justify-center rounded-full bg-[#0f0f0f80] p-1 text-sm font-semibold text-green-500 duration-300 hover:bg-[#0f0f0f]"
        >
          +
        </span>
        <span
          onClick={(e) => e.stopPropagation()}
          className="flex h-6 w-6 cursor-pointer items-center justify-center rounded-full bg-[#0f0f0f80] p-1 text-sm font-semibold text-blue-400 duration-300 hover:bg-[#0f0f0f]"
        >
          ?
        </span>
      </div>
    </div>
  );
};

const CardList = ({ event }: { event: string }) => {
  return (
    <div className="group relative h-36 w-full cursor-pointer overflow-hidden rounded-3xl">
      <img src={MOCK_IMG_URL} alt="" className="absolute left-0 top-0 w-full" />
      <div className="relative flex h-full w-2/3 flex-col gap-2 bg-[#232326ee] p-4 text-[#F7F8F8]">
        <div className="flex gap-2 text-xs text-[#cacaca]">
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
        </div>
        <h2 className="w-4/5 break-words text-2xl font-semibold">
          Koncert fagaty na PWR w C-16
        </h2>
        <p className="text-sm text-[#cacaca]">
          Lorem, ipsum dolor sit amet consectetur adipisicing elit. Architecto
          et saepe, iste sequi deleniti ducimus est
        </p>
      </div>
      <div className="absolute right-0 top-0 hidden h-full flex-col justify-center gap-2 bg-[#232326ee] px-4 group-hover:flex">
        <span
          onClick={(e) => e.stopPropagation()}
          className="flex h-8 w-8 cursor-pointer items-center justify-center rounded-full bg-[#0f0f0f80] p-2 font-semibold text-green-500 duration-300 hover:bg-[#0f0f0f]"
        >
          +
        </span>
        <span
          onClick={(e) => e.stopPropagation()}
          className="flex h-8 w-8 cursor-pointer items-center justify-center rounded-full bg-[#0f0f0f80] p-2 font-semibold text-blue-400 duration-300 hover:bg-[#0f0f0f]"
        >
          ?
        </span>
      </div>
    </div>
  );
};

const CardSwiping = ({ event }: { event: string }) => {
  return (
    <div className="group relative h-[45vh] w-[20vw] min-w-[15em] cursor-pointer overflow-clip rounded-3xl md:h-[50vh] md:min-h-[10em] md:min-w-[20em] lg:min-h-[25em] lg:min-w-[25em]">
      <img
        src={MOCK_IMG_URL}
        alt=""
        className="absolute left-0 top-0 h-full object-cover"
      />
      <div className="relative flex h-2/3 w-full flex-col gap-2 bg-[#232326ee] p-4 text-[#F7F8F8]">
        <div className="flex flex-wrap gap-1 text-xs text-[#cacaca]">
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
        </div>
        <h2 className="w-4/5 break-words text-3xl font-semibold">
          Koncert fagaty na PWR w C-16
        </h2>
        <p className="hidden text-base text-[#cacaca] md:block">
          Lorem ipsum, dolor sit amet consectetur adipisicing elit. Unde
          quisquam maiores doloremque molestiae ducimus, distinctio accusamus
          sed voluptatem eius itaque.
        </p>
      </div>
    </div>
  );
};
