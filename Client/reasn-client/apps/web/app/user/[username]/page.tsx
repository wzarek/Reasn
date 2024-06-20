"use client";
import { SharedUserPage } from "@reasn/ui/src/components/web";
import React from "react";

const UserPage = ({ params }: { params: { username: string } }) => {
  const { username } = params;
  return <SharedUserPage username={username} />;
};

export default UserPage;
