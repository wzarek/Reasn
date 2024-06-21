"use client";

import { MOCK_IMG_URL } from "@reasn/ui/src/components/shared/Card";
import { ButtonBase } from "@reasn/ui/src/components/shared/form";
import { Comment } from "@reasn/ui/src/components/shared/Comment";
import { useEffect, useRef, useState } from "react";

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

const EventPage = ({ params }: { params: { slug: string } }) => {
  const { slug } = params;
  const imgRef = useRef<HTMLImageElement>(null);
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const [gradient, setGradient] = useState<string>("");
  const editPage = slug === "edit";

  const [img, setImg] = useState<string>(
    IMAGES[Math.floor(Math.random() * IMAGES.length)],
  );

  const colorDistance = (color1: string, color2: string) => {
    const [r1, g1, b1] = color1.split(",").map(Number);
    const [r2, g2, b2] = color2.split(",").map(Number);
    return Math.sqrt((r1 - r2) ** 2 + (g1 - g2) ** 2 + (b1 - b2) ** 2);
  };

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

      const imageData = ctx.getImageData(0, 0, canvas.width, canvas.height);
      const data = imageData.data;

      const colorCount: { [key: string]: number } = {};
      for (let i = 0; i < data.length; i += 20) {
        const r = data[i];
        const g = data[i + 1];
        const b = data[i + 2];

        const color = `${r},${g},${b}`;
        if (colorCount[color]) {
          colorCount[color]++;
        } else {
          colorCount[color] = 1;
        }
      }

      let sortedColors = Object.entries(colorCount).sort(
        ([, countA], [, countB]) => countB - countA,
      );

      sortedColors = sortedColors.filter(([color]) => {
        const [r, g, b] = color.split(",").map(Number);
        return (r > 5 || g > 5 || b > 5) && (r < 250 || g < 250 || b < 250);
      });

      sortedColors = sortedColors.reduce(
        (acc, [color, count]) => {
          const similarColor = acc.find(
            ([accColor]) => colorDistance(color, accColor) < 10,
          );

          if (!similarColor) {
            acc.push([color, count]);
          } else {
            similarColor[1] += count;
          }

          return acc;
        },
        [] as [string, number][],
      );

      const dominantColors = sortedColors.slice(0, 10).map(([color]) => color);
      console.log(sortedColors.slice(0, 10));

      const dominantColorsRgb = dominantColors.map((color) => {
        const [r, g, b] = color.split(",").map(Number);
        return `rgb(${r}, ${g}, ${b})`;
      });

      setGradient(`linear-gradient(to right, ${dominantColorsRgb.join(", ")})`);
    };
  }, []);

  return (
    <div className="flex w-full flex-col gap-5">
      <div className="flex w-full flex-row justify-between gap-5">
        <div
          className="absolute bottom-[50%] right-[-50%] z-0 h-[80%] w-[200%] rounded-full blur-3xl duration-1000"
          style={{ background: gradient, opacity: gradient ? "0.25" : "0" }}
        ></div>
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
          <h1 className="mt-2 text-3xl font-semibold">
            Lorem ipsum dolor, sit amet consectetur adipisicing elit. Cumque,
            nam.
          </h1>
          <div className="mt-8 flex h-full flex-col gap-1 font-thin">
            <p>czas: 12 grudnia 2024r. 12:00 - 13 grudnia 2024r. 23:48</p>
            <p>adres: Wrocław, C-16 Politechnika Wrocławska, Polska</p>
            <p className="space-x-5">
              <span>biorący udział: 20</span>
              <span>zainteresowani: 200</span>
            </p>
            <div className="mt-5">
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
            <p>ostatnia edycja: 13 czerwca 2024r. 12:48</p>
          </div>
        </div>
        <div className="flex h-full flex-col gap-5">
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
            {editPage ? (
              <ButtonBase text="edytuj" onClick={() => {}} className="w-full" />
            ) : (
              <>
                <ButtonBase
                  text="dodaj do polubionych"
                  onClick={() => {}}
                  className="w-full font-semibold text-green-500"
                  background="hover:bg-[#0f0f0f] bg-black"
                />
                <ButtonBase
                  text="biorę udział"
                  onClick={() => {}}
                  className="w-full font-semibold text-blue-400"
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
