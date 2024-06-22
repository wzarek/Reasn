"use client";

import { MOCK_IMG_URL } from "@reasn/ui/src/components/shared/Card";
import {
  ButtonBase,
  FloatingInput,
  FloatingTextarea,
  SearchMultiDropdown,
  SingleDropdown,
} from "@reasn/ui/src/components/shared/form";
import { ChangeEvent, useState } from "react";
import { ArrowLeft, Clock, Location, Upload } from "@reasn/ui/src/icons";
import { useRouter } from "next/navigation";
import clsx from "clsx";
import { EventStatus } from "@reasn/common/src/enums/schemasEnums";
import { BaseInput } from "@reasn/ui/src/components/shared/form/Input";
import { getStatusClass } from "@reasn/common/src/helpers/uiHelpers";

const IMAGES = [
  "https://images.pexels.com/photos/19012544/pexels-photo-19012544/free-photo-of-storm.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  MOCK_IMG_URL,
  "https://images.pexels.com/photos/19867588/pexels-photo-19867588/free-photo-of-happy-pongal.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/6297150/pexels-photo-6297150.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/3225517/pexels-photo-3225517.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/1820563/pexels-photo-1820563.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/534164/pexels-photo-534164.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/624015/pexels-photo-624015.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/54332/currant-immature-bush-berry-54332.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
];

export const MOCK_TAGS = [
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

const EventEditPage = ({ params }: { params: { slug: string } }) => {
  const { slug } = params;
  const router = useRouter();
  const [status, setStatus] = useState<string>(EventStatus.REJECTED);

  const admin = slug === "edit";

  const [imgs, setImgs] = useState<string[]>(
    IMAGES.sort(() => Math.random() - 0.5).slice(0, 3),
  );

  const [tags, setTags] = useState<string[]>(MOCK_TAGS.slice(0, 5));

  const [paramsKeys, setParamsKeys] = useState<string[]>(
    Object.keys(MOCK_PARAMS).slice(0, 3),
  );

  const handleRedirect = () => {
    let conf = confirm("Czy na pewno chcesz wyjść bez zapisywania zmian?");

    if (conf) {
      router.push(`/events/${slug}`);
    }
  };

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
      <div className="relative flex w-full justify-between">
        <div
          className="flex w-[25vw] min-w-[25vw] cursor-pointer flex-row items-center gap-2 font-semibold"
          onClick={handleRedirect}
        >
          <ArrowLeft className="h-5 w-5 fill-slate-400" />
          <h3>cofnij do wydarzenia</h3>
        </div>
        <div className="relative flex w-1/4 flex-row justify-evenly gap-8">
          <ButtonBase text="zapisz" onClick={() => {}} className="w-full" />
        </div>
      </div>
      <div className="flex w-full flex-row flex-wrap justify-between gap-5 xl:flex-nowrap">
        <div className="flex h-max min-h-[50vh] w-full flex-col justify-between rounded-lg bg-[#1E1F296d] p-5 backdrop-blur-lg xl:w-[25vw] xl:min-w-[25vw]">
          {admin ? (
            <SingleDropdown
              label="Status"
              options={Object.values(EventStatus)}
              selectedOption={status}
              setSelectedOption={setStatus}
              selectedOptionClass={`font-bold uppercase ${getStatusClass(
                status as EventStatus,
              )}`}
            />
          ) : (
            <div className="flex flex-row items-center justify-between gap-[20%]">
              <p
                className={clsx(
                  "font-bold uppercase",
                  getStatusClass(status as EventStatus),
                )}
              >
                {status}
              </p>
              <ButtonBase
                text="anuluj wydarzenie"
                onClick={() => {}}
                className="grow from-red-400 to-red-700 font-semibold text-black"
              />
            </div>
          )}
          <div className="mt-8 flex flex-wrap gap-2 overflow-clip text-xs text-[#cacaca]">
            <SearchMultiDropdown
              label="Wyszukaj tagi"
              options={MOCK_TAGS}
              selectedOptions={tags}
              setSelectedOptions={setTags}
            />
            <div className="mt-2 flex flex-wrap gap-2">
              <h3 className="font-semibold">Wybrane tagi:</h3>
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
              defaultValue="Lorem ipsum dolor, sit amet consectetur adipisicing elit. Cumque,
              nam."
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
                defaultValue={"2024-12-12"}
                className="grow"
              />
              <FloatingInput
                label="Czas od"
                type="time"
                name="timeFrom"
                defaultValue={"12:00"}
                className="w-1/3"
              />
            </div>
            <div className="flex flex-row items-center gap-2">
              <Clock className="h-5 w-5 fill-slate-400" />
              <FloatingInput
                label="Data do"
                type="date"
                name="dateFrom"
                defaultValue={"2024-12-13"}
                className="grow"
              />
              <FloatingInput
                label="Czas do"
                type="time"
                name="timeFrom"
                defaultValue={"23:58"}
                className="w-1/3"
              />
            </div>
            <div className="flex flex-row items-center gap-2">
              <Location className="h-5 w-5 fill-slate-400" />
              <div className="flex w-full flex-wrap gap-8">
                <FloatingInput
                  label="Kraj"
                  type="text"
                  name="country"
                  defaultValue="Polska"
                  className="w-full"
                />
                <FloatingInput
                  label="Miasto"
                  type="text"
                  name="city"
                  defaultValue="Wrocław"
                  className="w-full"
                />
                <FloatingInput
                  label="Województwo"
                  type="text"
                  name="state"
                  defaultValue="Dolnośląskie"
                  className="w-full"
                />
                <FloatingInput
                  label="Ulica"
                  type="text"
                  name="street"
                  defaultValue="C-16 Politechnika Wrocławska"
                  className="w-full"
                />
              </div>
            </div>
            <div>
              <h3 className="mb-1font-semibold">Dodatkowe informacje:</h3>
              <div className="mt-8">
                <SearchMultiDropdown
                  label="Wyszukaj parametry"
                  options={Object.keys(MOCK_PARAMS)}
                  selectedOptions={paramsKeys}
                  setSelectedOptions={setParamsKeys}
                />
              </div>
              <div className="ml-5 mt-2 flex flex-col gap-1">
                <h3 className="font-semibold">Wybrane parametry:</h3>
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
          <div className="mt-8 flex items-center gap-2">
            <div className="relative rounded-full bg-gradient-to-r from-[#32346A] to-[#4E4F75] p-[2px]">
              <img
                src="https://avatars.githubusercontent.com/u/63869461?v=4"
                alt="avatar"
                className="relative z-10 h-10 w-10 rounded-full"
              />
            </div>
            <p>Nazwa organizatora</p>
          </div>
          <div className="mt-3 text-xs font-thin text-[#ccc]">
            <p>utworzono: 13 czerwca 2024r. 12:25</p>
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
          <div className="relative">
            <div className="mt-8">
              <FloatingTextarea
                label="Opis"
                name="comment"
                defaultValue="Lorem ipsum dolor sit amet consectetur adipisicing elit.
                Necessitatibus aspernatur quibusdam saepe enim totam suscipit
                tempore facere aut id sint ratione placeat, magni ipsa quo
                assumenda odit atque omnis, sequi, impedit reiciendis. Provident
                nobis quaerat maxime beatae sapiente. Placeat, obcaecati
                doloremque laboriosam cumque, praesentium necessitatibus itaque
                consequuntur ex dignissimos quam atque beatae impedit temporibus
                dicta ab magnam dolorum corrupti sit enim! Ipsa, omnis nisi."
                className="h-48"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EventEditPage;
