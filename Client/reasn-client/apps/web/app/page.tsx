"use client";

import {
  CTASection,
  HeroSection,
  QuickFilters,
} from "@reasn/ui/src/components/web";
import { Navbar, Footer } from "@reasn/ui/src/components/shared";

export default function Web() {
  return (
    <div className="min-h-screen bg-[#161618] text-white">
      <Navbar />
      <HeroSection />
      <QuickFilters />
      <CTASection />
      <Footer />
    </div>
  );
}
