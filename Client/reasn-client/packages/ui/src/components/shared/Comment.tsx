import React from "react";
import {
  CommentDto,
  CommentDtoMapper,
} from "@reasn/common/src/schemas/CommentDto";
import { ButtonBase, FloatingTextarea } from "./form";

interface CommentProps {
  comment?: CommentDto;
}

export const Comment = (props: CommentProps) => {
  const comment = CommentDtoMapper.fromObject({
    EventId: 1,
    Content:
      "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Soluta quidem sapiente odit maxime. Officiis, error laborum. Voluptatem quibusdam mollitia possimus?",
    CreatedAt: new Date(),
    UserId: 1,
  });

  return (
    <div className="flex flex-col gap-4 rounded-md bg-[#4b4e526d] p-2">
      <p className="font-thin">{comment.Content}</p>
      <div className="flex items-center gap-2">
        <div className="relative rounded-full bg-gradient-to-r from-[#32346A] to-[#4E4F75] p-[2px]">
          <img
            src="https://avatars.githubusercontent.com/u/63869461?v=4"
            alt="avatar"
            className="relative z-10 h-6 w-6 rounded-full"
          />
        </div>
        <p>username</p>
        <p className="ml-auto text-xs font-thin text-[#ccc]">
          13 czerwca 2024r. 12:25
        </p>
      </div>
    </div>
  );
};

export const NewComment = () => {
  return (
    <div className="mb-8 flex flex-col gap-4 rounded-md bg-[#4b4e526d] p-2">
      <FloatingTextarea
        label="Treść komentarza"
        name="newComment"
        className="mt-8 h-32"
      />
      <ButtonBase className="ml-auto" text="Dodaj" />
    </div>
  );
};
