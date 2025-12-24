// biome-ignore assist/source/organizeImports: <no exp>
import "@/app/globals.css";
import Header from "@/components/layout/header";
import Footer from "@/components/layout/footer";

//font
import { Inter } from "next/font/google";
export const inter = Inter({
  subsets: ["latin"],
  variable: "--font-inter",
  display: "swap",
});

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`font-sans ${inter.variable} antialiased`}>
        <Header />
        <main className="pt-[22vh] sm:pt-[20vh]  rounded-2xl">{children}</main>
        <Footer />
      </body>
    </html>
  );
}
