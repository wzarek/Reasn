import ApiAuthorizationError from "@reasn/common/src/errors/ApiAuthorizationError";
import ApiConnectionError from "@reasn/common/src/errors/ApiConnectionError";
import { ZodError, ZodIssue } from "zod";

export const handleErrorMessage = (e: any) => {
  if (e instanceof ApiAuthorizationError) {
    return {
      message: "Niepoprawne dane logowania." + e.message,
    };
  }
  if (e instanceof ApiConnectionError) {
    return {
      message: "Wystąpił błąd podczas logowania: " + e.message,
    };
  }

  if (e instanceof Error) {
    return {
      message: "Wystąpił błąd podczas logowania: " + e.message,
    };
  }

  return {
    message: "Wystąpił błąd podczas logowania: ",
  };
};

const formatZodIssue = (issue: ZodIssue): string => {
  const { path, message } = issue;
  const pathString = path.join(".");

  return `${pathString}: ${message}`;
};

export const formatZodError = (error: ZodError): string => {
  const { issues } = error;

  if (issues.length) {
    const currentIssue = issues[0];

    return formatZodIssue(currentIssue);
  }

  return "Niepoprawne wartości formularza.";
};
