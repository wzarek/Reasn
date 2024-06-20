"use client";

import React, { useState } from "react";
import { ButtonBase } from "../../shared/form";
import { Card, CardVariant, Interest } from "../../shared";
import { QuickFilterButton } from "../main/QuickFilters";

interface SharedUserPageProps {
  username: string;
}

export const SharedUserPage = (props: SharedUserPageProps) => {
  const { username } = props;
  const isMePage = username === "memememe";

  return (
    <div className="relative flex w-full flex-col gap-10">
      <div className="flex h-fit items-center gap-10">
        <div className="relative rounded-full bg-gradient-to-r from-[#32346A] to-[#4E4F75] p-1">
          <img
            src="https://avatars.githubusercontent.com/u/63869461?v=4"
            alt="avatar"
            className="relative z-10 h-32 w-32 rounded-full"
          />
          <div className="absolute right-[-150%] top-[-175%] z-0 h-[400%] w-[400%] rounded-full bg-[#000b6d] opacity-15 blur-3xl"></div>
        </div>
        <div className="flex flex-col gap-1">
          {isMePage ? (
            <h1 className="relative z-10 text-4xl font-bold">
              Imię Nazwisko{" "}
              <span className="text-base text-[#ccc]">({username})</span>
            </h1>
          ) : (
            <h1 className="relative z-10 text-4xl font-bold">{username}</h1>
          )}
          <p className="text-sm text-[#ccc]">Wrocław, Polska</p>
        </div>
        {isMePage && (
          <ButtonBase
            text="edytuj profil"
            onClick={() => {}}
            className="ml-auto"
          />
        )}
      </div>
      {isMePage ? <MeUserContent /> : <UserContent />}
    </div>
  );
};

const MeUserContent = () => {
  const [selectedFilter, setSelectedFilter] = useState<string>("Wszystkie");
  return (
    <>
      <div className="z-10 w-full rounded-xl bg-[#1E1F29] p-4">
        <h3 className="mb-6 text-lg font-semibold">Trwające wydarzenia:</h3>
        <div className="flex flex-col gap-2">
          <Card variant={CardVariant.List} event={"a"} />
          <Card variant={CardVariant.List} event={"a"} />
        </div>
      </div>
      <div className="z-10 w-full rounded-xl bg-[#1E1F29] p-4">
        <h3 className="mb-6 text-lg font-semibold">Nadchodzące wydarzenia:</h3>
        <div className="flex w-full gap-2 pb-4">
          <QuickFilterButton
            text="Wszystkie"
            onClick={() => setSelectedFilter("Wszystkie")}
            selected={selectedFilter === "Wszystkie"}
          />
          <QuickFilterButton
            text="Biorę udział"
            onClick={() => setSelectedFilter("Biorę udział")}
            selected={selectedFilter === "Biorę udział"}
          />
          <QuickFilterButton
            text="Polubione"
            onClick={() => setSelectedFilter("Polubione")}
            selected={selectedFilter === "Polubione"}
          />
        </div>
        <div className="flex flex-col gap-2">
          <Card variant={CardVariant.List} event={"a"} />
          <Card variant={CardVariant.List} event={"a"} />
        </div>
      </div>
      <div className="z-10 w-full rounded-xl bg-[#1E1F29] p-4">
        <h3 className="mb-6 text-lg font-semibold">Twoje zainteresowania:</h3>
        <div className="flex w-full flex-row justify-start gap-2">
          <Interest text="piłka nożna" onDelete={() => {}} />
          <Interest text="pływanie" onDelete={() => {}} />
          <Interest text="gry komputerowe" onDelete={() => {}} />
          <ButtonBase text="+" onClick={() => {}} className="ml-auto w-16" />
        </div>
      </div>
    </>
  );
};

const UserContent = () => {
  return (
    <>
      <div className="z-10 w-full rounded-xl bg-[#1E1F29] p-4">
        <h3 className="mb-6 text-lg font-semibold">
          Obecne wydarzenia tego użytkownika:
        </h3>
        <div className="flex flex-col gap-2">
          <Card variant={CardVariant.List} event={"a"} />
          <Card variant={CardVariant.List} event={"a"} />
        </div>
      </div>
      <div className="z-10 w-full rounded-xl bg-[#1E1F29] p-4">
        <h3 className="mb-6 text-lg font-semibold">
          Polubione wydarzenia tego użytkownika:
        </h3>
        <div className="flex flex-col gap-2">
          <Card variant={CardVariant.List} event={"a"} />
          <Card variant={CardVariant.List} event={"a"} />
        </div>
      </div>
    </>
  );
};
