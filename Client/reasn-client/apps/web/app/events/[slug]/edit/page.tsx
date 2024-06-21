"use client";

import { MOCK_IMG_URL } from "@reasn/ui/src/components/shared/Card";
import {
  ButtonBase,
  FloatingInput,
  FloatingTextarea,
} from "@reasn/ui/src/components/shared/form";
import { useRef, useState } from "react";
import {
  ArrowLeft,
  Clock,
  Location,
  QuestionCircle,
  TickCircle,
} from "@reasn/ui/src/icons";
import { useRouter } from "next/navigation";

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

const EventEditPage = ({ params }: { params: { slug: string } }) => {
  const { slug } = params;
  const imgRef = useRef<HTMLImageElement>(null);
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const router = useRouter();

  const [img, setImg] = useState<string>(
    IMAGES[Math.floor(Math.random() * IMAGES.length)],
  );

  const handleRedirect = () => {
    router.push(`/events/${slug}`);
  };

  return (
    <div className="flex w-full flex-col gap-5">
      <div
        className="flex w-1/3 cursor-pointer flex-row items-center gap-2 font-semibold"
        onClick={handleRedirect}
      >
        <ArrowLeft className="h-5 w-5 fill-slate-400" />
        <h3>cofnij do wydarzenia</h3>
      </div>
      <div className="flex w-full flex-row justify-between gap-5">
        <div className="flex h-max min-h-[50vh] w-1/3 flex-col justify-between rounded-lg bg-[#1E1F296d] p-5 backdrop-blur-lg">
          <p className="mb-2 font-bold text-orange-400">DO AKCEPTACJI</p>
          <div className="flex max-h-5 gap-2 overflow-clip text-xs text-[#cacaca]">
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
            <p className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]">#abcd</p>
          </div>
          <div className="mt-10">
            <FloatingTextarea
              label="Tytuł"
              name="description"
              defaultValue="Lorem ipsum dolor, sit amet consectetur adipisicing elit. Cumque,
              nam."
              className="text-lg"
            />
          </div>
          <div className="mt-8 flex h-full flex-col gap-8 font-thin">
            <div className="flex flex-row items-center gap-2">
              <Clock className="h-5 w-5 fill-slate-400" />
              <FloatingInput
                label="Data od"
                type="date"
                name="dateFrom"
                defaultValue={"12.12.2024"}
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
                defaultValue={"12.12.2024"}
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
              <FloatingInput
                label="Lokalizacja"
                type="text"
                name="location"
                defaultValue="Wrocław, C-16 Politechnika Wrocławska, Polska"
                className="w-full"
              />
            </div>
            <div className="flex flex-row gap-5">
              <div className="flex flex-row items-center gap-2">
                <QuestionCircle className="h-5 w-5 fill-orange-400" />
                <span>60 zainteresowanych</span>
              </div>
              <div className="flex flex-row items-center gap-2">
                <TickCircle className="h-5 w-5 fill-green-400" />
                <span>20 bierze udział</span>
              </div>
            </div>
            <div>
              <h3 className="mb-1 font-semibold">Dodatkowe informacje:</h3>
              <div className="ml-5 flex flex-col gap-1">
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
                <p>
                  <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                    abcd:
                  </span>
                  efgh
                </p>
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
        <div className="flex h-full w-full flex-col gap-5">
          <div className="relative z-10 h-[50vh] w-full overflow-hidden rounded-lg bg-black">
            <img
              src={img}
              alt=""
              className="h-full w-full object-cover"
              ref={imgRef}
            />
            <canvas className="hidden" ref={canvasRef}></canvas>
          </div>
          <div className="relative flex flex-row justify-evenly gap-8">
            <ButtonBase text="zapisz" onClick={() => {}} className="w-full" />
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
                className="h-32"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EventEditPage;
