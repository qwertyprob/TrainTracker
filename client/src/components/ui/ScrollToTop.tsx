"use client"; // нужно для интерактивности

import { useState, useEffect } from "react";
import { FaArrowUp } from "react-icons/fa";

export default function ScrollToTop() {
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      if (window.scrollY > 300) {
        setVisible(true);
      } else {
        setVisible(false);
      }
    };
    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };

  return (
    <>
      {visible && (
        <button
          onClick={scrollToTop}
          className="fixed bottom-8 right-8 text-white p-3 rounded-full shadow-lg cursor-pointer"
          aria-label="Scroll to top"
          style={{
            backgroundColor: "var(--bg-blue)",
            transition: "all 0.5s ease",
          }}
        >
          <FaArrowUp />
        </button>
      )}
    </>
  );
}
