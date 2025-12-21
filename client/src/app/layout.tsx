import Header from "@/components/ui/header/Header";
import ScrollToTop from "@/components/ui/ScrollToTop";
import "@/styles/globals.scss";



export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Header/>
        <div className="container max-w-full px-4 mt-5">
          {children}
            <ScrollToTop />
        </div>

      </body>
    </html>
  );
}
