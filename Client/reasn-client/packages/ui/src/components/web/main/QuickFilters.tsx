import clsx from "clsx";
import React, { useState } from "react";
import { Card, CardVariant } from "@reasn/ui/src/components/shared";
import { ButtonBase } from "@reasn/ui/src/components/shared/form";

interface QuickFiltersButtonProps {
  text: string;
  selected?: boolean;
  onClick: () => void;
}

export const QuickFilters = () => {
  const [selectedFilter, setSelectedFilter] = useState("Today");

  return (
    <div>
      <div className="flex w-full gap-10 overflow-x-auto bg-[#1E1F29] px-10 py-8">
        <QuickFilterButton
          text="Dziś"
          onClick={() => setSelectedFilter("Today")}
          selected={selectedFilter === "Today"}
        />
        <QuickFilterButton
          text="Jutro"
          onClick={() => setSelectedFilter("Tommorow")}
          selected={selectedFilter === "Tommorow"}
        />
        <QuickFilterButton
          text="Rozpoczęte"
          onClick={() => setSelectedFilter("In Progress")}
          selected={selectedFilter === "In Progress"}
        />
        <QuickFilterButton
          text="W tym tygodniu"
          onClick={() => setSelectedFilter("This Week")}
          selected={selectedFilter === "This Week"}
        />
        <QuickFilterButton
          text="Wrocław"
          onClick={() => setSelectedFilter("Wro")}
          selected={selectedFilter === "Wro"}
        />
        <QuickFilterButton
          text="Kraków"
          onClick={() => setSelectedFilter("Krk")}
          selected={selectedFilter === "Krk"}
        />
        <QuickFilterButton
          text="Zdalne"
          onClick={() => setSelectedFilter("Remote")}
          selected={selectedFilter === "Remote"}
        />
      </div>
      <div className="xs:grid-cols-2 grid grid-cols-1 place-items-center gap-2 p-10 sm:gap-10 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-6">
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
      </div>
      <div className="flex justify-center py-10">
        <ButtonBase text="więcej" onClick={() => console.log("wiecej")} />
      </div>
    </div>
  );
};

export const QuickFilterButton = (props: QuickFiltersButtonProps) => {
  const { text, onClick, selected } = props;
  return (
    <div className="min-w-36 rounded-2xl bg-gradient-to-r from-[#ff6363] to-[#1e35ff] p-px text-white">
      <button
        onClick={onClick}
        className={clsx(
          "h-full w-full rounded-2xl px-4 py-2",
          { "bg-gradient-to-r from-[#914343] to-[#0c1db7]": selected },
          { "bg-[#1E1F29] hover:bg-[#2E2F3E]": !selected },
        )}
      >
        {text}
      </button>
    </div>
  );
};
