"use client";

import { motion } from "motion/react";

export default function RunningText({ text }: { text: string }) {
  return (
    <div className="relative overflow-hidden w-full bg-gray-20  h-10">
      <motion.div
        className="inline-block whitespace-nowrap px-2 py-2 text-gray-700 font-semibold text-sm sm:text-base"
        animate={{ x: ["100%", "-100%"] }}
        transition={{ repeat: Infinity, duration: 20, ease: "linear" }}
      >
        {Array(10).fill(text).join(" ")}
      </motion.div>
    </div>
  );
}
