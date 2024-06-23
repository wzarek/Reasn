"use client";

import { Card, CardVariant } from "@reasn/ui/src/components/shared";
import { Add, Dismiss, Question } from "@reasn/ui/src/icons";
import clsx from "clsx";
import React, { MouseEvent, useState } from "react";

const EventsSuggestionsPage = () => {
  const [events, setEvents] = useState<number[]>([
    0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
  ]);
  const currentCardRef = React.useRef<HTMLDivElement>(null);
  const nextCardRef = React.useRef<HTMLDivElement>(null);
  const [isAnimating, setIsAnimating] = useState(false);
  const [action, setAction] = useState<"like" | "dislike" | "participate" | "">(
    "",
  );

  const handleDismissEvent = (e: MouseEvent<HTMLElement>) => {
    currentCardRef.current?.classList.add("animate-fadeOutLeft");
    setIsAnimating(true);
    setAction("dislike");
    setTimeout(() => {
      currentCardRef.current?.classList.remove("animate-fadeOutLeft");
      setEvents((prev) => prev.slice(1));
      setIsAnimating(false);
      setAction("");
    }, 500);
    e.stopPropagation();
  };

  const handleLikeEvent = (e: MouseEvent<HTMLElement>) => {
    currentCardRef.current?.classList.add("animate-fadeOutDown");
    setIsAnimating(true);
    setAction("like");
    setTimeout(() => {
      currentCardRef.current?.classList.remove("animate-fadeOutDown");
      setEvents((prev) => prev.slice(1));
      setIsAnimating(false);
      setAction("");
    }, 500);
    e.stopPropagation();
  };

  const handleParticipateInEvent = (e: MouseEvent<HTMLElement>) => {
    currentCardRef.current?.classList.add("animate-fadeOutRight");
    setIsAnimating(true);
    setAction("participate");
    setTimeout(() => {
      currentCardRef.current?.classList.remove("animate-fadeOutRight");
      setEvents((prev) => prev.slice(1));
      setIsAnimating(false);
      setAction("");
    }, 500);
    e.stopPropagation();
  };

  return (
    <div className="relative flex w-full flex-col items-center justify-center">
      <div
        className={clsx(
          "absolute right-[-50%] top-[-25%] z-0 h-[50%] w-[200%] rounded-full blur-3xl",
          "bg-blue-400 duration-1000",
          { "opacity-30": action === "like" },
          { "opacity-10": action !== "like" },
        )}
      ></div>
      <div
        className={clsx(
          "absolute left-[-50%] top-[-25%] z-0 h-[100%] w-[100%] rounded-full blur-3xl",
          "bg-red-400 duration-1000",
          { "opacity-30": action === "dislike" },
          { "opacity-10": action !== "dislike" },
        )}
      ></div>
      <div
        className={clsx(
          "absolute right-[-50%] top-[-25%] z-0 h-[100%] w-[100%] rounded-full blur-3xl",
          "bg-green-400 duration-1000",
          { "opacity-30": action === "participate" },
          { "opacity-10": action !== "participate" },
        )}
      ></div>
      <div className="relative h-[60vh] w-full items-center justify-center">
        {events.map((val, idx) => (
          <div
            key={val + idx}
            className={clsx(
              "absolute left-1/2 duration-500",
              { "pointer-events-none": idx !== 0 },
              { "opacity-5 blur-sm": idx > 1 || (idx === 1 && !isAnimating) },
              { "opacity-1 blur-none": idx === 1 && isAnimating },
              { "opacity-1 pointer-events-auto": idx === 0 },
            )}
            style={{
              transform: `translate(-50%, ${idx * 1.5}%)`,
              zIndex: `${11 - idx}`,
            }}
            ref={idx === 0 ? currentCardRef : idx === 1 ? nextCardRef : null}
          >
            <Card variant={CardVariant.Swiping} event="Abc" />
          </div>
        ))}
      </div>
      <div className="flex w-full items-center justify-center gap-10 lg:gap-24">
        <div
          onClick={handleDismissEvent}
          className={clsx(
            "flex h-12 w-28 cursor-pointer items-center justify-center rounded-full",
            "bg-red-500 bg-opacity-40 p-2 text-2xl font-semibold",
            "group duration-300 hover:bg-red-700",
          )}
        >
          <Dismiss className="h-5 w-5 fill-red-500 duration-300 group-hover:fill-black" />
        </div>
        <div
          onClick={handleLikeEvent}
          className={clsx(
            "flex h-12 w-16 cursor-pointer items-center justify-center rounded-full",
            "bg-blue-400 bg-opacity-40 p-2 text-2xl font-semibold",
            "group duration-300 hover:bg-blue-700",
          )}
        >
          <Question className="h-5 w-5 fill-blue-400 duration-300 group-hover:fill-black" />
        </div>
        <div
          onClick={handleParticipateInEvent}
          className={clsx(
            "flex h-12 w-28 cursor-pointer items-center justify-center rounded-full",
            "bg-green-500 bg-opacity-40 p-2 text-2xl font-semibold",
            "group duration-300 hover:bg-green-700",
          )}
        >
          <Add className="h-5 w-5 fill-green-500 duration-300 group-hover:fill-black" />
        </div>
      </div>
    </div>
  );
};

export default EventsSuggestionsPage;
