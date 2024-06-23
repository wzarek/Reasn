"use client";

import { EventStatus } from "@reasn/common/src/enums/schemasEnums";
import { Card, CardVariant } from "@reasn/ui/src/components/shared";
import {
  FloatingInput,
  SearchMultiDropdown,
  SingleDropdown,
} from "@reasn/ui/src/components/shared/form";
import { ArrowLeft, ArrowRight, Clock } from "@reasn/ui/src/icons";
import { useState } from "react";
import { MOCK_TAGS } from "./[slug]/edit/page";
import clsx from "clsx";

const EventsPage = () => {
  const [selectedSortBy, setSelectedSortBy] = useState<string>("tytuł");
  const [selectedSortOrder, setSelectedSortOrder] = useState<string>("rosnąco");
  const [selectedStatus, setSelectedStatus] = useState<string>("wszystkie");
  const [selectViewMode, setSelectViewMode] = useState<string>("kafelki");
  const [tags, setTags] = useState<string[]>(MOCK_TAGS.slice(0, 5));
  const [selectedPage, setSelectedPage] = useState<number>(1);
  const cardVariant =
    selectViewMode === "kafelki" ? CardVariant.Tile : CardVariant.List;

  return (
    <div className="relative flex w-full flex-col">
      <div className="absolute right-[-50%] top-[-10vh] z-0 h-[80vh] w-[200%] rounded-full bg-gradient-to-r from-[#FF6363] to-[#1E34FF] opacity-10 blur-3xl duration-1000"></div>
      <h1 className="h-[5rem] w-fit content-center bg-gradient-to-r from-[#FF6363] to-[#1E34FF] bg-clip-text text-5xl font-bold text-transparent">
        Wydarzenia
      </h1>
      <div className="flex w-full flex-col justify-center gap-10 lg:flex-row">
        <div className="flex h-full min-h-[50vh] w-full flex-col justify-start gap-8 rounded-lg bg-[#1E1F296d] p-5 backdrop-blur-lg xl:w-[25vw] xl:min-w-[25vw]">
          <h3 className="text-2xl font-semibold">Filtry:</h3>
          <div className="flex flex-row items-center gap-2">
            <FloatingInput
              label="Szukaj w tekście"
              type="text"
              name="searchText"
              className="grow"
            />
          </div>
          <div className="flex flex-row items-center gap-2">
            <Clock className="h-5 w-5 fill-slate-400" />
            <FloatingInput
              label="Data od"
              type="date"
              name="dateFrom"
              className="grow"
            />
          </div>
          <div className="flex flex-row items-center gap-2">
            <Clock className="h-5 w-5 fill-slate-400" />
            <FloatingInput
              label="Data do"
              type="date"
              name="dateTo"
              className="grow"
            />
          </div>
          <div className="flex flex-col items-start gap-2">
            <h4>Status:</h4>
            <SingleDropdown
              label="Sortowanie"
              options={["wszystkie", ...Object.values(EventStatus)]}
              selectedOption={selectedStatus}
              setSelectedOption={setSelectedStatus}
            />
          </div>
          <div className="flex flex-row items-center gap-2">
            <SearchMultiDropdown
              label="Wyszukaj tagi"
              options={MOCK_TAGS}
              selectedOptions={tags}
              setSelectedOptions={setTags}
            />
          </div>
        </div>
        <div className="flex w-full flex-col gap-5">
          <div className="flex w-full flex-row items-center justify-start gap-10">
            <div className="flex w-1/4 flex-row items-center gap-5">
              <h3>widok:</h3>
              <SingleDropdown
                label="Widok"
                options={["kafelki", "lista"]}
                selectedOption={selectViewMode}
                setSelectedOption={setSelectViewMode}
              />
            </div>
            <div className="ml-auto flex w-1/2 flex-row items-center gap-5 self-end">
              <h3>sortowanie:</h3>
              <SingleDropdown
                label="Sortowanie"
                options={["rosnąco", "malejąco"]}
                selectedOption={selectedSortOrder}
                setSelectedOption={setSelectedSortOrder}
              />
              <SingleDropdown
                label="Sortowanie"
                options={["data od", "tytuł"]}
                selectedOption={selectedSortBy}
                setSelectedOption={setSelectedSortBy}
              />
            </div>
          </div>
          <div
            className={clsx(
              {
                "xs:grid-cols-2 grid grid-cols-1 place-items-center gap-2 sm:grid-cols-3 sm:gap-10 md:grid-cols-4 lg:grid-cols-3 xl:grid-cols-4":
                  cardVariant === CardVariant.Tile,
              },
              { "flex flex-col gap-5": cardVariant === CardVariant.List },
            )}
          >
            {[0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11].map((idx) => (
              <Card variant={cardVariant} event="Abc" key={idx} />
            ))}
          </div>
          <div className="flex w-full items-center justify-center gap-5">
            {selectedPage > 1 && (
              <ArrowLeft
                className="h-10 w-10 cursor-pointer fill-slate-400 p-2"
                onClick={() => setSelectedPage(selectedPage - 1)}
              />
            )}
            {[1, 2, 3].map((idx) => (
              <p
                key={idx}
                className={clsx("cursor-pointer px-2 py-1 text-lg", {
                  "rounded-lg bg-[#4E4F75] text-black": selectedPage === idx,
                })}
              >
                {idx}
              </p>
            ))}
            {selectedPage < 3 && (
              <ArrowRight
                className="h-10 w-10 cursor-pointer fill-slate-400 p-2"
                onClick={() => setSelectedPage(selectedPage + 1)}
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default EventsPage;
