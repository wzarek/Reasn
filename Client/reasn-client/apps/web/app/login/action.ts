"use server";

import { LoginRequestSchema } from "@reasn/common/src/schemas/LoginRequest";
import { login } from "@/services/auth";
import { setToken } from "@/lib/token";
import { redirect } from "next/navigation";
import {
  formatZodError,
  handleErrorMessage,
} from "@reasn/common/src/helpers/errorHelpers";

export type LoginFormState = {
  message?: string | null;
  errors?: string | {};
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
      errors: formatZodError(result.error),
      message: "Niepoprawne wartości formularza.",
    };
  }

  try {
    const payload = await login(result.data);
    setToken(payload);
    redirect("/");
  } catch (e) {
    return handleErrorMessage(e);
  }
};
