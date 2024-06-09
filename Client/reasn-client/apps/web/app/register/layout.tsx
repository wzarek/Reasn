import React from "react";

const RegisterLayout = ({
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
    <section className="relative mx-auto flex h-screen w-3/4 justify-between py-10">
      {children}
    </section>
  );
};

export default RegisterLayout;
