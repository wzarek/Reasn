import React from "react";

const EventLayout = ({
  children,
  params,
}: {
  children: React.ReactNode;
  params: {
    tag: string;
    item: string;
  };
}) => {
  return (
    <section className="relative mx-auto flex min-h-screen w-[90%] justify-between py-10 sm:w-3/4">
      {children}
    </section>
  );
};

export default EventLayout;
