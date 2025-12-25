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
import React from "react";

const trainIcons = [MdTrain, MdDirectionsRailway, MdCommute, MdTram];

export default function RunningText() {
  const [trains, setTrains] = useState<string[]>([]);

  useEffect(() => {
    const fetchTitles = async () => {
      const res = await fetchTrainsTitle();
      setTrains(res && res.length > 0 ? res : []);
    };

    fetchTitles();
    const interval = setInterval(fetchTitles, 60000);
    return () => clearInterval(interval);
  }, []);

  const displayTrains = trains.length > 0 ? [...trains] : Array(16).fill(" ");

  const trainElements = displayTrains.flatMap((name, index) => {
    const Icon = trainIcons[index % trainIcons.length];
    return [
      <Icon
        key={`icon-${index}`}
        className="inline-block w-6 h-6 mx-1 text-gray-700"
      />,
      <span key={`text-${index}`} className="mx-1 font-semibold text-gray-700">
        {name || "\u00A0"}
      </span>,
    ];
  });

  // Дублируем элементы с уникальными ключами
  const repeatedElements = [
    ...trainElements,
    ...trainElements.map((el) =>
      React.cloneElement(el, { key: `dup-${el.key}` })
    ),
  ];

  const baseDuration = 35;
  const extraPerItem = 1;
  const duration = baseDuration + displayTrains.length * extraPerItem;

  return (
    <div className="relative overflow-hidden w-full h-12 flex items-center bg-white hover:bg-gray-100 duration-300 ease-in rounded-lg shadow-sm">
      <motion.div
        className="inline-flex"
        animate={{ x: ["100%", "-100%"] }}
        transition={{ repeat: Infinity, duration: duration, ease: "linear" }}
      >
        <pre> {repeatedElements} </pre>
      </motion.div>
    </div>
  );
}
