"use client";

import { useEffect, useState } from "react";
import { HTTP_STATUS } from "@/constants/statusCodes";
import { fetchTrainsWithShortDelay } from "@/lib/api";
import type { ApiResponse } from "@/types/api";
import type { Train } from "@/types/train";

const REFRESH_INTERVAL = 15_000;

export default function ArrivingTrains() {
  const [response, setResponse] = useState<ApiResponse<Train[]> | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let isMounted = true;

    const fetchTrains = async () => {
      try {
        const res = await fetchTrainsWithShortDelay();
        if (isMounted && res.status === HTTP_STATUS.OK) {
          setResponse(res);
        }
      } finally {
        if (isMounted) setLoading(false);
      }
    };

    // initial fetch
    fetchTrains();

    // repeat every minute
    const intervalId = setInterval(fetchTrains, REFRESH_INTERVAL);

    // cleanup
    return () => {
      isMounted = false;
      clearInterval(intervalId);
    };
  }, []);

  if (loading) {
    return (
      <div className="text-sm text-gray-500 p-3 text-center">
        Loading arriving trains…
      </div>
    );
  }

  if (!response || response.data?.length === 0) {
    return (
      <div className="text-sm text-gray-500 p-3 text-center">
        No arriving trains
      </div>
    );
  }

  return (
    <div className="flex flex-col gap-2 p-3">
      {response.data?.slice(0, 4).map((train) => (
        <div
          key={train.id}
          className="
            flex items-center justify-between
            bg-white rounded-xl px-3 py-2
            shadow-sm
            border-l-4
          "
        >
          <div>
            <p className="text-xs text-gray-500 leading-none">
              #{train.number}
            </p>
            <p className="text-sm font-semibold text-gray-800">{train.name}</p>
          </div>

          <div className="text-right">
            <p className="text-xs text-gray-500">Arriving</p>
            <p
              className={`text-sm font-semibold ${
                train.delayTime === 0 ? "text-green-600" : "text-gray-700"
              }`}
            >
              {train.delayTime === 0
                ? "Now"
                : train.delayTime > 1
                ? `${train.delayTime} minutes`
                : `${train.delayTime} minute`}
            </p>
          </div>
        </div>
      ))}
    </div>
  );
}
