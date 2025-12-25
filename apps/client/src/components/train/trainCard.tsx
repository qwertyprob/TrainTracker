"use client";

import { fetchTrainsApi } from "@/lib/api";
import type { ApiResponse } from "@/types/api";
import type { Train } from "@/types/train";
import { useState } from "react";

export default function TestApi() {
  const [response, setResponse] = useState<ApiResponse<Train[]> | null>(null);

  const handleClick = async () => {
    const res = await fetchTrainsApi();
    setResponse(res || null);
  };

  return (
    <div>
      {/** biome-ignore lint/a11y/useButtonType: <explanation> */}
      <button onClick={handleClick}>Загрузить поезда</button>
      {response && (
        <ul>
          {response.data?.map((t) => (
            <li key={t.id}>
              {t.name} — {t.delayTime} мин {t.nextStation.title}
            </li>
          ))}
        </ul>
      )}
      <h1>
        Response:
        <pre>Status:{response?.status}</pre>
        <pre>Message:{response?.message}</pre>
        <pre>Error:{response?.error}</pre>
        <pre>Data type:{typeof response?.data}</pre>
      </h1>
    </div>
  );
}
