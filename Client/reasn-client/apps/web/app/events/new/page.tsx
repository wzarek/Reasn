"use client";

import {
  ButtonBase,
  FloatingInput,
  FloatingTextarea,
  SearchMultiDropdown,
} from "@reasn/ui/src/components/shared/form";
import { ChangeEvent, useState } from "react";
import { Clock, Location, Upload } from "@reasn/ui/src/icons";
import { useRouter } from "next/navigation";
import clsx from "clsx";
import { EventStatus } from "@reasn/common/enums/modelsEnums";
import { BaseInput } from "@reasn/ui/src/components/shared/form/Input";
import { getStatusClass } from "@reasn/common/helpers/uiHelpers";

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

const MOCK_PARAMS: { [key: string]: string } = {
  abcd: "efgh",
  ijkl: "mnop",
  qrst: "uvwx",
  yzab: "cdef",
  ghij: "ijkl",
  mnop: "qrst",
  uvwx: "yzab",
  cdef: "ghij",
};

const EventAddPage = () => {
  const [status, setStatus] = useState<string>(EventStatus.PENDING_APPROVAL);

  const [imgs, setImgs] = useState<string[]>([]);

  const [tags, setTags] = useState<string[]>([]);

  const [paramsKeys, setParamsKeys] = useState<string[]>([]);

  const handleImageUpload = (
    event: ChangeEvent<HTMLInputElement>,
    idx: number,
  ) => {
    const file = event?.target?.files?.[0];
    if (file) {
      const fileURL = URL.createObjectURL(file);
      setImgs((prevImgs) => {
        const newImgs = [...prevImgs];
        newImgs[idx] = fileURL;
        return newImgs;
      });
    }
  };

  return (
    <div className="relative flex w-full flex-col gap-5">
      <div
        className={clsx(
          "absolute bottom-[50%] right-[-50%] z-0 h-[80%] w-[200%] rounded-full blur-3xl",
          "bg-gradient-to-r to-[#4E4F75] opacity-20 duration-1000",
          getStatusClass(status as EventStatus),
        )}
      ></div>
      <div className="flex w-full flex-row flex-wrap justify-between gap-5 xl:flex-nowrap">
        <div className="flex h-max min-h-[50vh] w-full flex-col justify-between rounded-lg bg-[#1E1F296d] p-5 backdrop-blur-lg xl:w-[25vw] xl:min-w-[25vw]">
          <p
            className={clsx(
              "mb-2 font-bold uppercase",
              getStatusClass(status as EventStatus),
            )}
          >
            {status}
          </p>
          <div className="flex flex-wrap gap-2 overflow-clip text-xs text-[#cacaca]">
            <SearchMultiDropdown
              label="Wyszukaj tagi"
              options={MOCK_TAGS}
              selectedOptions={tags}
              setSelectedOptions={setTags}
            />
            <div className="mt-2 flex flex-wrap gap-2">
              <h3 className="font-semibold">Wybrane tagi:</h3>
              {!tags ||
                (tags.length === 0 && <p className="text-[#cacaca]">brak</p>)}
              {tags.map((tag, idx) => (
                <p
                  key={idx + tag}
                  className="rounded-md bg-[#4E4F75] px-[5px] py-[1px]"
                >
                  {tag}
                </p>
              ))}
            </div>
          </div>
          <div className="mt-10">
            <FloatingTextarea
              label="Tytuł"
              name="description"
              className="h-28 text-lg"
            />
          </div>
          <div className="mt-8 flex h-full flex-col gap-8 font-thin">
            <div className="flex flex-row items-center gap-2">
              <Clock className="h-5 w-5 fill-slate-400" />
              <FloatingInput
                label="Data od"
                type="date"
                name="dateFrom"
                className="grow"
              />
              <FloatingInput
                label="Czas od"
                type="time"
                name="timeFrom"
                className="grow"
              />
            </div>
            <div className="flex flex-row items-center gap-2">
              <Clock className="h-5 w-5 fill-slate-400" />
              <FloatingInput
                label="Data do"
                type="date"
                name="dateFrom"
                className="grow"
              />
              <FloatingInput
                label="Czas do"
                type="time"
                name="timeFrom"
                className="grow"
              />
            </div>
            <div className="flex flex-row items-center gap-2">
              <Location className="h-5 w-5 fill-slate-400" />
              <div className="flex w-full flex-wrap gap-8">
                <FloatingInput
                  label="Kraj"
                  type="text"
                  name="country"
                  className="w-full"
                />
                <FloatingInput
                  label="Miasto"
                  type="text"
                  name="city"
                  className="w-full"
                />
                <FloatingInput
                  label="Województwo"
                  type="text"
                  name="state"
                  className="w-full"
                />
                <FloatingInput
                  label="Ulica"
                  type="text"
                  name="street"
                  className="w-full"
                />
              </div>
            </div>
            <div>
              <h3 className="mb-1 font-semibold">Dodatkowe informacje:</h3>
              <SearchMultiDropdown
                label="Wyszukaj parametry"
                options={Object.keys(MOCK_PARAMS)}
                selectedOptions={paramsKeys}
                setSelectedOptions={setParamsKeys}
              />
              <div className="ml-5 mt-2 flex flex-col gap-1">
                <h3 className="font-semibold">Wybrane parametry:</h3>
                {!paramsKeys ||
                  (paramsKeys.length === 0 && (
                    <p className="text-[#cacaca]">brak</p>
                  ))}
                {paramsKeys?.map((key, idx) => (
                  <div
                    key={idx + key}
                    className="flex items-center justify-between"
                  >
                    <span className="mr-2 w-1/4 rounded-md bg-[#4E4F75] px-[5px] py-[1px]">
                      {key}:
                    </span>
                    <BaseInput
                      type="text"
                      defaultValue={MOCK_PARAMS[key]}
                      className="w-full"
                    />
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>
        <div className="flex w-full flex-col gap-5">
          <div className="relative z-10 grid h-[50vh] w-full grid-cols-2 grid-rows-2 gap-3 overflow-hidden rounded-lg">
            {[0, 1, 2, 3].map((idx) => (
              // eslint-disable-next-line @next/next/no-img-element
              <div
                className="group relative h-full w-full overflow-clip"
                key={idx}
              >
                <div
                  onClick={() => {
                    const element = document?.querySelector(
                      `input[name="img-${idx}"]`,
                    );
                    if (element instanceof HTMLInputElement) {
                      element.click();
                    }
                  }}
                  className="absolute z-20 flex h-full w-full cursor-pointer items-center justify-center bg-black opacity-0 duration-300 group-hover:opacity-50"
                >
                  <Upload className="h-10 w-10 fill-white" />
                </div>
                <input
                  type="file"
                  hidden
                  name={`img-${idx}`}
                  accept="image/png, image/jpeg"
                  onChange={(e) => handleImageUpload(e, idx)}
                />
                {imgs[idx] ? (
                  <img
                    src={imgs[idx]}
                    alt=""
                    className="relative z-10 h-full w-full object-cover duration-300 group-hover:blur-[2px]"
                  />
                ) : (
                  <div className="relative z-10 h-full w-full bg-[#1E1F29]"></div>
                )}
              </div>
            ))}
          </div>
          <div className="relative flex flex-col gap-8">
            <div className="mt-8">
              <FloatingTextarea label="Opis" name="comment" className="h-48" />
            </div>
            <div className="relative flex w-full flex-row justify-evenly gap-8">
              <ButtonBase text="dodaj" onClick={() => {}} className="w-full" />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EventAddPage;
