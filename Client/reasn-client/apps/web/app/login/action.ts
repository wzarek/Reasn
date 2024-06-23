"use server";

import { LoginRequestSchema } from "@reasn/common/src/schemas/LoginRequest";
import { login } from "@/services/auth";
import { setToken } from "@/lib/token";
import { redirect } from "next/navigation";

export type LoginFormState = {
  message?: string | null;
  errors?: Record<string, string[]>;
};

export const loginAction = async (
  prevState: any,
  formData: FormData,
): Promise<LoginFormState | undefined> => {
  const result = LoginRequestSchema.safeParse({
    email: formData.get("email"),
    password: formData.get("password"),
  });

  if (!result.success) {
    return {
      errors: result.error.flatten().fieldErrors,
      message: "Niepoprawne wartości formularza.",
    };
  }

  const payload = await login(result.data);
  setToken(payload);
  redirect("/");
};
