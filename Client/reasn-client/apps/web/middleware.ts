import type { NextRequest } from "next/server";
import { NextResponse } from "next/server";
import { getSession } from "@/lib/session";
import { UserRole } from "@reasn/common/src/enums/schemasEnums";

export const middleware = (req: NextRequest) => {
  const session = getSession();
  const path = req.nextUrl.pathname;

  const isAuthPath = path.startsWith("/login") || path.startsWith("/register");

  if (path.startsWith("/user") && session.user?.role !== UserRole.ADMIN) {
    return NextResponse.redirect(new URL(path.replace("/edit", ""), req.url));
  }

  if (!session.isAuthenticated()) {
    if (isAuthPath) return NextResponse.next();
    return NextResponse.redirect(new URL("/login", req.url));
  }

  if (isAuthPath) {
    return NextResponse.redirect(new URL("/me", req.url));
  }

  if (path.startsWith("/events") && session.user?.role === UserRole.USER) {
    return NextResponse.redirect(new URL("/events", req.url));
  }
};

export const config = {
  matcher: [
    "/events/new",
    "/events/(.*)/(.*)",
    "/me",
    "/me/(.*)",
    "/user/(.*)/edit",
    "/login",
    "/register",
    "/register/(.*)",
  ],
};
