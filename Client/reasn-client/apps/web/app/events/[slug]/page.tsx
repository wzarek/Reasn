"use client";

import { MOCK_IMG_URL } from "@reasn/ui/src/components/shared/Card";
import { ButtonBase } from "@reasn/ui/src/components/shared/form";
import { Comment } from "@reasn/ui/src/components/shared/Comment";
import { useEffect, useRef, useState } from "react";
import {
  ArrowLeft,
  ArrowRight,
  Clock,
  Location,
  QuestionCircle,
  TickCircle,
} from "@reasn/ui/src/icons";
import { useRouter } from "next/navigation";

import useColorWorker from "@reasn/common/hooks/useColorWorker";

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
  "ijkl",
  "mnop",
  "qrst",
  "uvwx",
  "yzab",
  "cdef",
  "ghij",
  "ijkl",
  "mnop",
  "qrst",
  "uvwx",
  "yzab",
  "cdef",
  "ghij",
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

const EventPage = ({ params }: { params: { slug: string } }) => {
  const { slug } = params;
  const imgRef = useRef<HTMLImageElement>(null);
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const [gradient, setGradient] = useState<string>("");
  const editPage = slug === "edit";
  const router = useRouter();

  const [imgs, setImgs] = useState<string[]>(IMAGES);

  const [currentImageIdx, setCurrentImageIdx] = useState<number>(0);

  const [imageData, setImageData] = useState<Uint8ClampedArray>();

  useEffect(() => {
    const img = imgRef.current;
    const canvas = canvasRef.current;

    if (!img || !canvas) {
      return;
    }

    const ctx = canvas.getContext("2d");

    if (!ctx) {
      return;
    }

    img.crossOrigin = "Anonymous";
    img.onload = () => {
      canvas.width = img.width;
      canvas.height = img.height;

      ctx.drawImage(img, 0, 0, img.width, img.height);

      const imageData = ctx.getImageData(
        0,
        0,
        canvas.width,
        canvas.height,
      ).data;
      setImageData(imageData);
    };
  }, [imgRef.current]);

  const dominantColors = useColorWorker(imageData);

  useEffect(() => {
    if (dominantColors.length === 0) return;

    const dominantColorsRgb = dominantColors.map((color: string) => {
      const [r, g, b] = color.split(",").map(Number);
      return `rgb(${r}, ${g}, ${b})`;
    });

    setGradient(`linear-gradient(to right, ${dominantColorsRgb.join(", ")})`);
  }, [dominantColors]);

  const handleRedirect = () => {
    router.push(`/events/${slug}/edit`);
  };

  return (
    <div className="flex w-full flex-col gap-5">
      <div className="flex w-full flex-row flex-wrap justify-between gap-5 xl:flex-nowrap">
        <div
          className="absolute bottom-[50%] right-[-50%] z-0 h-[80%] w-[200%] rounded-full blur-3xl duration-1000"
          style={{ background: gradient, opacity: gradient ? "0.25" : "0" }}
        ></div>
        <div className="flex h-max min-h-[50vh] w-full flex-col justify-between rounded-lg bg-[#1E1F296d] p-5 backdrop-blur-lg xl:w-[25vw] xl:min-w-[25vw]">
          <p className="mb-2 font-bold text-orange-400">DO AKCEPTACJI</p>
          <div className="flex flex-wrap gap-2 overflow-clip text-xs text-[#cacaca]">
            {MOCK_TAGS.map((tag, idx) => (
              <p
                key={idx + tag}
                className="rounded-md bg-[#4b4e52] px-[5px] py-[1px]"
              >
                {tag}
              </p>
            ))}
          </div>
          <h1 className="mt-2 text-3xl font-semibold">
            Lorem ipsum dolor, sit amet consectetur adipisicing elit. Cumque,
            nam.
          </h1>
          <div className="mt-8 flex h-full flex-col gap-1 font-thin">
            <div className="flex flex-row items-center gap-2">
              <Clock className="h-5 w-5 fill-slate-400" />
              <p>12 grudnia 2024r. 12:00 - 13 grudnia 2024r. 23:48</p>
            </div>
            <div className="flex flex-row items-center gap-2">
              <Location className="h-5 w-5 fill-slate-400" />
              <p>Wrocław, C-16 Politechnika Wrocławska, Polska</p>
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
            <div className="mt-5">
              <h3 className="mb-1 font-semibold">Dodatkowe informacje:</h3>
              <div className="ml-5 flex flex-col gap-1">
                {MOCK_PARAMS &&
                  Object.keys(MOCK_PARAMS).map((key, idx) => (
                    <p key={idx + key}>
                      <span className="mr-2 rounded-md bg-[#4b4e52] px-[5px] py-[1px]">
                        {key}:
                      </span>
                      {MOCK_PARAMS[key]}
                    </p>
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
            <p>ostatnia edycja: 13 czerwca 2024r. 12:48</p>
          </div>
        </div>
        <div className="flex w-full flex-col gap-5">
          <div className="relative z-10 h-[50vh] w-full overflow-hidden rounded-lg bg-black">
            <div
              className="flex h-full w-full gap-0"
              style={{ transform: `translateX(-${currentImageIdx * 100}%)` }}
            >
              {imgs.map((img, idx) => (
                // eslint-disable-next-line @next/next/no-img-element
                <div className="w-full flex-shrink-0" key={idx + img}>
                  <img
                    src={img}
                    alt=""
                    className="h-full w-full object-cover"
                    ref={currentImageIdx === idx ? imgRef : null}
                  />
                </div>
              ))}
            </div>
            {currentImageIdx > 0 && (
              <ArrowLeft
                onClick={() => setCurrentImageIdx(currentImageIdx - 1)}
                className="absolute left-5 top-[50%] z-20 h-8 w-8 -translate-y-1/2 cursor-pointer rounded-lg bg-gradient-to-r from-[#32346A7d] to-[#4E4F757d] fill-white p-2"
              />
            )}
            {currentImageIdx < imgs.length - 1 && (
              <ArrowRight
                onClick={() => setCurrentImageIdx((idx) => idx + 1)}
                className="absolute right-5 top-[50%] z-20 h-8 w-8 -translate-y-1/2 cursor-pointer rounded-lg bg-gradient-to-r from-[#32346A7d] to-[#4E4F757d] fill-white p-2"
              />
            )}
            <canvas className="hidden" ref={canvasRef}></canvas>
          </div>
          <div className="relative z-10 flex flex-row justify-evenly gap-8">
            {editPage ? (
              <ButtonBase
                text="edytuj"
                onClick={handleRedirect}
                className="w-full"
              />
            ) : (
              <>
                <ButtonBase
                  text="dodaj do polubionych"
                  onClick={() => {}}
                  className="w-full font-semibold text-orange-400"
                  background="hover:bg-[#0f0f0f] bg-black"
                />
                <ButtonBase
                  text="biorę udział"
                  onClick={() => {}}
                  className="w-full font-semibold text-green-400"
                  background="hover:bg-[#0f0f0f] bg-black"
                />
              </>
            )}
          </div>
          <div className="relative">
            <h3 className="font-semibold">Opis:</h3>
            <p className="text-justify font-thin">
              Lorem ipsum dolor sit amet consectetur adipisicing elit.
              Necessitatibus aspernatur quibusdam saepe enim totam suscipit
              tempore facere aut id sint ratione placeat, magni ipsa quo
              assumenda odit atque omnis, sequi, impedit reiciendis. Provident
              nobis quaerat maxime beatae sapiente. Placeat, obcaecati
              doloremque laboriosam cumque, praesentium necessitatibus itaque
              consequuntur ex dignissimos quam atque beatae impedit temporibus
              dicta ab magnam dolorum corrupti sit enim! Ipsa, omnis nisi.
            </p>
          </div>
          <div className="relative">
            <h3 className="font-semibold">Komentarze:</h3>
            <div className="flex flex-col gap-2">
              <Comment />
              <Comment />
              <Comment />
              <Comment />
              <Comment />
              <Comment />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EventPage;
