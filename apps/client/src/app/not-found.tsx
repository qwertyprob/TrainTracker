"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";

export default function NotFound() {
  const pathName: string = usePathname();

  const segment = (() => {
    const cleanPath = pathName.startsWith("/") ? pathName.slice(1) : pathName;
    return cleanPath.split("/")[0];
  })();

  return (
    <main className="grid min-h-full place-items-center px-6 lg:px-8 text-2xl ">
      <div className="text-center mx-auto bg-light">
        <h1 className="mt-4 text-5xl font-semibold tracking-tight text-gray-900 sm:text-7xl">
          Page <span className="text-gray-400">"{segment}"</span> not found
        </h1>
        <p className="mt-6 text-lg font-medium text-gray-500 sm:text-xl/8">
          Sorry, we couldn’t find the page you’re looking for.
        </p>
        <div className="mt-10 flex items-center justify-center gap-x-6">
          <Link
            href="/"
            className="rounded-md bg-gray-600 px-3.5 py-2.5 text-sm font-semibold text-white shadow-xs hover:bg-gray-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-600 active:scale-[0.95] transition-transform duration-300 ease-in-out"
          >
            Go back dashboard
          </Link>
        </div>
      </div>
    </main>
  );
}
