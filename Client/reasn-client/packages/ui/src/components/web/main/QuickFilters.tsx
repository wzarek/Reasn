import clsx from "clsx";
import React, { useState } from "react";
import { Card, CardVariant } from "@reasn/ui/src/components/shared";

interface QuickFiltersButtonProps {
  title: string;
  selected?: boolean;
  onClick: () => void;
}

export const QuickFilters = () => {
  const [selectedFilter, setSelectedFilter] = useState("Today");

  return (
    <div>
      <div className="flex w-full gap-10 overflow-x-clip bg-[#1E1F29] px-10 py-8">
        <QuickFilterButton
          title="Dziś"
          onClick={() => setSelectedFilter("Today")}
          selected={selectedFilter === "Today"}
        />
        <QuickFilterButton
          title="Jutro"
          onClick={() => setSelectedFilter("Tommorow")}
          selected={selectedFilter === "Tommorow"}
        />
        <QuickFilterButton
          title="Rozpoczęte"
          onClick={() => setSelectedFilter("In Progress")}
          selected={selectedFilter === "In Progress"}
        />
        <QuickFilterButton
          title="W tym tygodniu"
          onClick={() => setSelectedFilter("This Week")}
          selected={selectedFilter === "This Week"}
        />
        <QuickFilterButton
          title="Wrocław"
          onClick={() => setSelectedFilter("Wro")}
          selected={selectedFilter === "Wro"}
        />
        <QuickFilterButton
          title="Kraków"
          onClick={() => setSelectedFilter("Krk")}
          selected={selectedFilter === "Krk"}
        />
        <QuickFilterButton
          title="Zdalne"
          onClick={() => setSelectedFilter("Remote")}
          selected={selectedFilter === "Remote"}
        />
      </div>
      <div className="flex flex-wrap gap-10 p-10">
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
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
        <Card variant={CardVariant.Tile} event="Abc" />
      </div>
      <div className="flex justify-center py-10">
        <button className="w-36 rounded-2xl bg-gradient-to-r from-[#32346A] to-[#4E4F75] px-4 py-2">
          więcej
        </button>
      </div>
    </div>
  );
};

export const QuickFilterButton = (props: QuickFiltersButtonProps) => {
  const { title, onClick, selected } = props;
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
        {title}
      </button>
    </div>
  );
};
