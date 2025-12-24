"use client";

import { motion } from "motion/react";

export default function RunningText(props: any) {
  const text = props.text;
  return (
    <div className="relative overflow-hidden w-full bg-gradient-to-r from-gray-100 via-gray-50 to-gray-100 shadow-inner rounded-lg">
      <div className="absolute top-0 left-0 h-full w-8 bg-gradient-to-r from-gray-100 to-transparent pointer-events-none" />
      <div className="absolute top-0 right-0 h-full w-8 bg-gradient-to-l from-gray-100 to-transparent pointer-events-none" />

      <motion.div
        className="inline-block whitespace-nowrap px-2 py-2 text-gray-700 font-medium text-sm sm:text-base"
        animate={{ x: ["100%", "-100%"] }}
        transition={{ repeat: Infinity, duration: 20, ease: "linear" }}
      >
        {text.repeat(10)}
      </motion.div>
    </div>
  );
}
