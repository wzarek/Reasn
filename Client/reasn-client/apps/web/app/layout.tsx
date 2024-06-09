import { Footer, Navbar } from "@reasn/ui/src/components/shared";
import "../styles/global.css";
import "@reasn/ui/src/styles.css";

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className="min-h-screen overflow-x-clip bg-[#161618] text-white">
        <Navbar />
        {children}
        <Footer />
      </body>
    </html>
  );
}
