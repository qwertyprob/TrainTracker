// biome-ignore assist/source/organizeImports: <sorting>
import { IoTrainOutline } from "react-icons/io5";
import RedDot from "@/components/ui/redDot";
import RunningText from "@/components/ui/runningText";
import Link from "next/link";

export default function Header() {
  const text: string = "Soon.. Soon.. Soon.. Soon.. Soon.. Soon.. ";
  return (
    <header className="fixed w-full h-16 bg-gray-100 shadow-lg shadow-black z-50 ">
      <div className="mx-auto flex flex-col items-center p-4 gap-2 bg-gray-50 hover:bg-gray-100 transition-colors duration-300 ease-in-out">
        {/* Header-text */}
        <div className="flex flex-col sm:flex-row items-center justify-center sm:gap-4 gap-2 w-full">
          <div className="flex items-center justify-center flex-wrap sm:flex-nowrap">
            <Link
              href="/"
              className="
              inline-flex items-center gap-2
              rounded-xl px-3 py-2
              transition-all duration-200 ease-out
              hover:bg-gray-200/70
              hover:shadow-sm
              active:scale-[0.98]
              "
            >
              <span className="flex items-center cursor-pointer text-3xl sm:text-5xl md:text-6xl text-gray-500 font-bold">
                <IoTrainOutline className="bg-gray-200 rounded-2xl p-1 mr-2 shadow-sm text-gray-700" />
                Train Tracking
              </span>
            </Link>

            <span className="text-2xl ml-2 md:text-4xl bg-gray-200 p-2 rounded-2xl text-gray-400 hidden md:block">
              Service
            </span>
          </div>

          {/* LIVE UPDATES */}
          <div className="flex items-center sm:justify-start bg-red-200 rounded-2xl p-1 sm:p-2 mt-2 sm:mt-0">
            <RedDot />
            <span className="text-2x1 sm:text-sm font-medium ml-1 whitespace-nowrap text-gray-600">
              LIVE UPDATES
            </span>
          </div>
        </div>
      </div>

      {/* Info блок */}
      <div className="w-full m-0 overflow-hidden border-0  shadow-xs">
        <RunningText text={text} />
      </div>
    </header>
  );
}
