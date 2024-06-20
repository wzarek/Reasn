"use client";

import { MOCK_IMG_URL } from "@reasn/ui/src/components/shared/Card";
import { ButtonBase } from "@reasn/ui/src/components/shared/form";

const EventPage = ({ params }: { params: { slug: string } }) => {
  const { slug } = params;
  const editPage = slug === "edit";

  return (
    <div className="flex w-full flex-col gap-5">
      <div className="flex w-full flex-row justify-between gap-5">
        <div className="absolute bottom-[50%] right-[-50%] z-0 h-[80%] w-[200%] rounded-full bg-gradient-to-r from-[#FF6363] to-[#1E34FF] opacity-15 blur-3xl"></div>
        <div className="flex min-h-[50vh] w-1/3 flex-col justify-between rounded-lg bg-[#1E1F296d] p-5 backdrop-blur-lg">
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
            <img src={MOCK_IMG_URL} alt="" className="object-contain" />
          </div>
          <div className="flex flex-row justify-evenly gap-8">
            {editPage ? (
              <ButtonBase text="edytuj" onClick={() => {}} className="w-full" />
            ) : (
              <>
                <ButtonBase
                  text="zainteresowany"
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
          <p className="relative text-justify font-thin">
            Lorem ipsum dolor sit amet consectetur adipisicing elit.
            Necessitatibus aspernatur quibusdam saepe enim totam suscipit
            tempore facere aut id sint ratione placeat, magni ipsa quo assumenda
            odit atque omnis, sequi, impedit reiciendis. Provident nobis quaerat
            maxime beatae sapiente. Placeat, obcaecati doloremque laboriosam
            cumque, praesentium necessitatibus itaque consequuntur ex
            dignissimos quam atque beatae impedit temporibus dicta ab magnam
            dolorum corrupti sit enim! Ipsa, omnis nisi.
          </p>
        </div>
      </div>
    </div>
  );
};

export default EventPage;
