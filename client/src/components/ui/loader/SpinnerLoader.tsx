"use client"

import { useEffect, useState } from "react";
import loader from "@/components/ui/loader/spinnerLoader.module.scss";

export default function SPLoader() {
  const [showLoader, setLoader] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setLoader(false);
    }, 2000);

    return () => clearTimeout(timer);
  }, []);

  return (
    <>
      {showLoader
        ? <div className="flex items-center justify-center mt-[20vh]">
            <div className={`${loader.loader} shadow-lg`}></div>
        </div>
        : <div>Готово</div>
      }
    </>
  );
}
