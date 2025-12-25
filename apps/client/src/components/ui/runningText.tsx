"use client";

import { fetchTrainsTitle } from "@/lib/api";
import { motion } from "motion/react";
import { useEffect, useState } from "react";
import {
  MdTrain,
  MdDirectionsRailway,
  MdCommute,
  MdTram,
} from "react-icons/md";

// Массив иконок
const trainIcons = [MdTrain, MdDirectionsRailway, MdCommute, MdTram];

export default function RunningText() {
  const [trains, setTrains] = useState<string[] | null>(null);

  useEffect(() => {
    const fetchTitles = async () => {
      const res = await fetchTrainsTitle();
      if (res && res.length > 0) {
        setTrains(res);
      } else {
        setTrains(Array(16).fill(" "));
      }
    };

    fetchTitles();
    const interval = setInterval(fetchTitles, 60000); // обновляем каждые 60 сек
    return () => clearInterval(interval);
  }, []);

  // Генерируем элементы бегущей строки
  const trainElements = trains
    ? trains.flatMap((name, index) => {
        const Icon = trainIcons[index % trainIcons.length]; // детерминированно
        return [
          <Icon
            key={`icon-${index}`}
            className="inline-block w-6 h-6 mx-1 text-gray-700"
          />,
          <span
            key={`text-${index}`}
            className="mx-1 font-semibold text-gray-700"
          >
            {name}
          </span>,
        ];
      })
    : Array(16)
        .fill(null)
        .flatMap((_, index) => {
          const Icon = trainIcons[index % trainIcons.length]; // детерминированно
          return [
            <Icon
              key={`icon-${index}`}
              className="inline-block w-6 h-6 mx-1 text-gray-700"
            />,
            <span
              key={`text-${index}`}
              className="mx-1 font-semibold text-gray-700"
            >
              &nbsp;
            </span>,
          ];
        });

  return (
    <div className="relative overflow-hidden w-full h-12 flex items-center hover:bg-gray-50 duration-300 ease-in">
      <motion.div
        className="inline-flex"
        animate={{ x: ["100%", "-100%"] }}
        transition={{ repeat: Infinity, duration: 35, ease: "linear" }}
      >
        <pre>
          {trainElements}
          {trainElements}
        </pre>
      </motion.div>
    </div>
  );
}
