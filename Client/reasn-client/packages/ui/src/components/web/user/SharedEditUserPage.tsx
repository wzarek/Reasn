"use client";

import React, { ChangeEvent, useRef, useState } from "react";
import {
  ButtonBase,
  FloatingInput,
  SearchMultiDropdown,
  SingleDropdown,
} from "../../shared/form";
import { UserRole } from "@reasn/common/src/enums/schemasEnums";
import { Upload } from "../../../icons";

const MOCK_TAGS = [
  "abcd",
  "efgh",
  "ijkl",
  "mnop",
  "qrst",
  "uvwx",
  "yzab",
  "cdef",
  "ghij",
  "dada",
  "vvvv",
  "bgbfgb",
  "nfnfnf",
  "mmjmjm",
  ",lkyt",
  "t554",
  "fsdfs",
  "hhhhh",
  "fsf",
  "u234ghhvwx",
  "nh",
  "sdfsf4",
  "ses5",
];

interface SharedEditUserPageProps {
  username: string;
}

export const SharedEditUserPage = (props: SharedEditUserPageProps) => {
  const { username } = props;
  const [tags, setTags] = useState<string[]>(MOCK_TAGS.slice(0, 5));
  const [selectedRole, setSelectedRole] = useState<string>(UserRole.USER);
  const imageInputRef = useRef<HTMLInputElement>(null);
  const [img, setImg] = useState<string>(
    "https://avatars.githubusercontent.com/u/63869461?v=4",
  );

  const isAdmin = username === "memememe";

  const handleImageUpload = (event: ChangeEvent<HTMLInputElement>) => {
    const file = event?.target?.files?.[0];
    if (file) {
      const fileURL = URL.createObjectURL(file);
      setImg(fileURL);
    }
  };

  return (
    <form className="flex w-full flex-col gap-24">
      <div className="flex h-fit w-full flex-col items-center gap-10 md:flex-row">
        <div className="group relative h-32 w-32 rounded-full bg-gradient-to-r from-[#32346A] to-[#4E4F75] p-1">
          <img
            src={img}
            alt="avatar"
            className="pointer-events-none relative z-10 h-full w-full rounded-full object-cover"
          />
          <div className="pointer-events-none absolute right-[-150%] top-[-175%] z-0 h-[400%] w-[400%] rounded-full bg-green-400 opacity-15 blur-3xl"></div>
          <div
            onClick={() => {
              const element = imageInputRef.current;
              if (element instanceof HTMLInputElement) {
                element.click();
              }
            }}
            className="absolute left-0 top-0 z-20 flex h-full w-full cursor-pointer items-center justify-center rounded-full bg-black opacity-0 duration-300 group-hover:opacity-50"
          >
            <Upload className="h-10 w-10 fill-white" />
          </div>
          <input
            type="file"
            hidden
            name="img"
            accept="image/png, image/jpeg"
            onChange={handleImageUpload}
            ref={imageInputRef}
          />
        </div>
        <FloatingInput
          type="text"
          label="nazwa użytkownika"
          name="username"
          defaultValue={username}
        />
        {isAdmin && (
          <div className="mx-auto w-1/2 md:ml-auto md:mr-0 md:w-1/5">
            <SingleDropdown
              label="Rola"
              options={Object.values(UserRole)}
              selectedOption={selectedRole}
              setSelectedOption={setSelectedRole}
            />
          </div>
        )}
        <ButtonBase text="zapisz" />
      </div>
      <div className="flex w-full flex-col gap-24">
        <div className="flex w-full flex-row flex-wrap gap-8">
          <h2 className="w-full text-3xl font-semibold">Dane kontaktowe</h2>
          <FloatingInput
            type="text"
            label="imię"
            name="name"
            defaultValue="Jan"
            className="grow sm:w-1/2 sm:grow-0"
          />
          <FloatingInput
            type="text"
            label="nazwisko"
            name="surname"
            defaultValue="Kowalski"
            className="grow"
          />
          <FloatingInput
            type="email"
            label="email"
            name="email"
            defaultValue="jan@kowalski.pl"
            className="grow sm:w-1/2 sm:grow-0"
          />
          <FloatingInput
            type="tel"
            label="numer telefonu"
            name="phone"
            defaultValue="123456789"
            className="grow"
          />
        </div>
        <input type="hidden" name="role" defaultValue="USER" />
        <div className="flex w-full flex-row flex-wrap gap-8">
          <h2 className="w-full text-3xl font-semibold">Dane adresowe</h2>
          <FloatingInput
            type="text"
            label="miasto"
            name="city"
            defaultValue="Warszawa"
            className="grow"
          />
          <FloatingInput
            type="text"
            label="kraj"
            name="country"
            defaultValue="Polska"
            className="grow"
          />
          <FloatingInput
            type="text"
            label="ulica"
            name="street"
            defaultValue="Kwiatowa"
            className="w-full"
          />
          <FloatingInput
            type="text"
            label="województwo"
            name="state"
            defaultValue="Mazowieckie"
            className="grow"
          />
          <FloatingInput
            type="text"
            label="kod pocztowy"
            name="postcode"
            defaultValue="00-000"
            className="grow"
          />
        </div>
        <div className="flex w-full flex-row flex-wrap gap-8">
          <h2 className="w-full text-3xl font-semibold">Zainteresowania</h2>
          <SearchMultiDropdown
            label="Wyszukaj zainteresowania"
            options={MOCK_TAGS}
            selectedOptions={tags}
            setSelectedOptions={setTags}
          />
        </div>
      </div>
    </form>
  );
};
