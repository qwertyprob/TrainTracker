import { API_TIMEOUT, API_URL } from "@/constants/api";
import { ApiClient } from "./apiClient";
import { mapTrain } from "@/lib/mappers/train";
import type { Train } from "@/types/train";
import type { Incident } from "@/types/incident";

import type { ApiResponse } from "@/types/api";

const api = new ApiClient({ baseUrl: API_URL, timeout: API_TIMEOUT });

export async function fetchTrainsApi(): Promise<ApiResponse<Train[]>> {
  const res = await api.get<Train[]>("/train");

  return {
    ...res,
  };
}
export async function fetchTrainsTitle(): Promise<string[]> {
  const res = await api.get<Train[]>("/train");

  return res.data?.map((t) => t.name) ?? [];
}
export async function fetchTrainsWithShortDelay(): Promise<
  ApiResponse<Train[]>
> {
  const res = await api.get<Train[]>("/train");

  return {
    ...res,
    data: res.data?.filter((train) => train.delayTime <= 5) ?? [],
  };
}

export async function fetchTrainsWithZeroDelay(): Promise<
  ApiResponse<Train[]>
> {
  const res = await api.get<Train[]>("/train");

  return {
    ...res,
    data: res.data?.filter((train) => train.delayTime <= 1) ?? [],
  };
}

export async function fetchIncidents(): Promise<ApiResponse<Incident[]>> {
  const res = await api.get<Incident[]>("/incident");

  return {
    ...res,
    data: res.data ?? [],
  };
}
