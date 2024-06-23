"use server";

import {
  RegisterRequestSchema,
  RegisterRequestMapper,
} from "@reasn/common/src/schemas/RegisterRequest";
import { register } from "@/services/auth";
import { redirect } from "next/navigation";
import { z } from "zod";

export type RegisterFormState = {
  message?: string | null;
  errors?: Record<string, string[]>;
};

const RegisterRequestExtendedSchema = RegisterRequestSchema.extend({
  confirmPassword: z.string(),
}).refine((data) => data.password === data.confirmPassword);

export type RegisterFormData = z.infer<typeof RegisterRequestExtendedSchema>;

export const registerAction = async (
  prevState: any,
  formData: FormData,
): Promise<RegisterFormState | undefined> => {
  const result = RegisterRequestExtendedSchema.safeParse({
    name: formData.get("name"),
    surname: formData.get("surname"),
    email: formData.get("email"),
    username: formData.get("username"),
    password: formData.get("password"),
    confirmPassword: formData.get("confirmPassword"),
    phone: formData.get("phone"),
    address: {
      country: formData.get("country"),
      city: formData.get("city"),
      street: formData.get("street"),
      state: formData.get("state"),
      zipCode: formData.get("postcode"),
    },
    role: formData.get("role"),
  });

  if (!result.success) {
    console.log(result.error.flatten().fieldErrors);
    return {
      errors: result.error.flatten().fieldErrors,
      message: "Niepoprawne wartości formularza.",
    };
  }

  await register(RegisterRequestMapper.fromObject(result.data));
  redirect("/login");
};
