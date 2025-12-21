"use client";

import Head from "next/head";
import { useEffect, useState } from "react";
import SpinnerLoader from "@/components/ui/loader/SpinnerLoader";
import {Train} from "@/types/Train";

export default function Home() {
  const title = "Dashboard";
  const [trains, setTrains] = useState<Train[]>([]);
  const [isLoading, setIsLoading] = useState(true);


  const fetchTrains = () => {
    fetch("http://localhost:8080/api/train")
      .then((res) => res.json())
      .then((data: any) => {
        setTrains(data.data);
        setIsLoading(false);
      })
      .catch((err) => {
        console.error(err);
        setIsLoading(false);
      });
  };

  useEffect(() => {
    fetchTrains(); 

    const interval = setInterval(() => {
      fetchTrains() // 15 сек
    }, 15000); 

    return () => clearInterval(interval); 
  }, []);

  if (isLoading) return <SpinnerLoader />;

  return (
    <>
      <Head>
        <title>{title}</title>
      </Head>

      {trains.map((train) => (
        <div
          key={train.id}
          className="container mx-auto px-auto border rounded-lg shadow-md p-4 mb-4 bg-white hover:shadow-xl transition-shadow"
        >
          <div className="flex justify-between items-center mb-2">
            <h2 className="text-lg font-semibold">{train.name}</h2>
            <span className="text-gray-500 font-mono">#{train.number}</span>
          </div>

          <div className="flex justify-between text-sm text-gray-600">
            <span>Next station: {train.nextStation.title}</span>
            <span
              className={`font-semibold ${
                train.delayTime > 0 ? "text-red-500" : "text-green-500"
              }`}
            >
              Delay: {train.delayTime} min
            </span>
          </div>

          {train.incidents.length > 0 && (
            <div className="mt-2 text-red-600 text-sm">
              Incidents: {train.incidents.join(", ")}
            </div>
          )}
        </div>
      ))}
    </>
  );
}
