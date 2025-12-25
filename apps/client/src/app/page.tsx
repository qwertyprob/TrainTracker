import ArrivingTrains from "@/components/train/arrivingCard";
import DataTable from "@/components/train/dataTable";

export default function Home() {
  return (
    <div
      className="container mx-auto bg-gray-50 rounded-2xl min-h-[60vh] p-3 md:p-5
    hover:bg-gray-100 transition-colors duration-300 ease-in-out"
    >
      <div className="flex flex-col md:flex-row gap-4 md:gap-3 h-full">
        <div className="bg-white w-full md:w-[30%] rounded-2xl p-4 flex flex-col gap-4">
          <div className="flex-1 min-h-0">
            <span className="text-gray-500">Today:</span>
            <DataTable />
          </div>
          <div
            className="
    flex-1 min-h-[180px] sm:min-h-[220px] md:min-h-0
    bg-gray-50 rounded-2xl
    hover:bg-gray-100
    transition-colors duration-300 ease-in-out
  "
          >
            <span className="text-gray-500 ml-2 pt-2">Arriving table:</span>
            <ArrivingTrains />
          </div>
        </div>

        <div className="flex flex-col w-full md:w-[80%] gap-4 flex-1">
          <div className="bg-white rounded-2xl p-4 flex-1 flex flex-col">
            <div className="flex-1 min-h-full">
              <DataTable />
            </div>
          </div>

          <div className="bg-white rounded-2xl p-4 flex-1 flex flex-col">
            <div className="flex-1 min-h-full">
              <DataTable />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
