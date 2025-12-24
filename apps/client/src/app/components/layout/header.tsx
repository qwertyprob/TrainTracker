import RedDot from "@/app/components/ui/redDot";
import RunningText from "@/app/components/ui/runningText";
import { IoTrainOutline } from "react-icons/io5";

export default function Header() {
  const text: string = "Это бегущая строка через motion/react — ";
  return (
    <header className="bg-gray-100 w-full">
      <div className="mx-auto flex flex-col items-center p-4 gap-2 bg-gray-50">
        {/* Header-text */}
        <div className="flex flex-col sm:flex-row items-center justify-center sm:gap-4 gap-2 w-full">
          <div className="flex items-center justify-center flex-wrap sm:flex-nowrap">
            <span className="text-3xl sm:text-5xl md:text-6xl text-gray-500 font-bold flex">
              <IoTrainOutline
                className="bg-gray-200 rounded-2xl p-1 mr-2 shadow-sm"
                color="black"
              />
              Train Tracking
            </span>
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
      <div className="text-gray-300 rounded w-full m-0 overflow-hidden">
        <RunningText text={text} />
      </div>
    </header>
  );
}
