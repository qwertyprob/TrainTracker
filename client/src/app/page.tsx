"use client"

import Head from "next/head";
import { useEffect, useState } from "react";
import SpinnerLoader from "@/components/ui/loader/SpinnerLoader";

export default function Home() {
  const title = "Dashboard";
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setIsLoading(false);
    }, 2000);

    return () => clearTimeout(timer);
  }, []);

  return (
    <>
      <Head>
        <title>{title}</title>
      </Head>

      {isLoading ? (
        <SpinnerLoader />
      ) : (
        <section className="max-w-6xl mx-auto p-4 grid grid-cols-1 md:grid-cols-3 gap-6">
          <div className="bg-white shadow-lg rounded p-4">
            <h2 className="font-bold mb-2">Блок 1</h2>
            <p>Описание блока 1</p>
          </div>
          <div className="bg-white shadow-lg rounded p-4">
            <h2 className="font-bold mb-2">Блок 2</h2>
            <p>Описание блока 2</p>
          </div>
          <div className="bg-white shadow-lg rounded p-4">
            <h2 className="font-bold mb-2">Блок 3</h2>
            <p>Описание блока 3</p>
          </div>
        </section>
      )}
    </>
  );
}
