import { MdSignalWifiOff } from "react-icons/md"; // иконка "нет интернета"
import { FiInbox } from "react-icons/fi"; // иконка "пустой ящик"

export default function NoData() {
  return (
    <div className="flex flex-col items-center justify-center p-6 text-gray-500">
      <MdSignalWifiOff size={80} className="mb-2" />
      <span className="text-lg">No trains arrived!</span>
    </div>
  );
}
