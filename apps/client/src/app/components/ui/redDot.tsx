"use client";
import { motion } from "motion/react";

export default function RedDot() {
  return (
    <motion.div
      className="w-2 h-2 sm:w-2.5 sm:h-2.5 bg-red-500 rounded-full shadow-md"
      animate={{
        scale: [1, 1.4, 1],
        opacity: [0.8, 1, 0.8],
        boxShadow: [
          "0 0 0px rgba(255,0,0,0.4)",
          "0 0 6px rgba(255,0,0,0.7)",
          "0 0 0px rgba(255,0,0,0.4)",
        ],
      }}
      transition={{
        duration: 1,
        repeat: Infinity,
        repeatType: "loop",
        ease: "easeInOut",
      }}
    />
  );
}
