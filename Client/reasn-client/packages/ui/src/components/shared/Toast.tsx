import React from "react";

interface ToastProps {
  message?: string | null;
  errors?: string;
}

export const Toast = (props: ToastProps) => {
  const { message, errors } = props;
  console.log(errors);
  return (
    <div className="min-h-10 max-w-[25rem] rounded-lg border-2 border-[#FF6363] bg-black px-6 py-3">
      {message && <div className="text-base font-semibold">{message}</div>}
      {errors && <div className="text-sm text-red-500">{errors}</div>}
    </div>
  );
};
