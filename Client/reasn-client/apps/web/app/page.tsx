"use client";

import { HeroSection, QuickFilters } from "@reasn/ui/src";
import { Navbar } from "../components/Navbar";
import { Footer } from "../components/Footer";

export default function Web() {
  return (
    <div className="min-h-screen bg-[#161618] text-white">
      <Navbar />
      <HeroSection />
      <QuickFilters />
      <Footer />
    </div>
  );
}
