import { API_TIMEOUT, API_URL } from "@/constants/api";
import { ApiClient } from "./apiClient";
import { mapTrain } from "@/lib/mappers/train";
import type { Train } from "@/types/train";
import type { ApiResponse } from "@/types/api";
import { data } from "motion/react-client";

const api = new ApiClient({ baseUrl: API_URL, timeout: API_TIMEOUT });

export async function fetchTrainsApi(): Promise<ApiResponse<Train[]>> {
  console.log("[fetchTrainsApi] Called");

  const res = await api.get<Train[]>("/train");

  //mapping
  const mapped = res.data?.map(mapTrain);
  console.log("[fetchTrainsApi] Response received:", mapped);

  return {
    ...res,
    data: mapped,
  };
}
export async function fetchTrainsTitle(): Promise<string[]> {
  const res = await api.get<Train[]>("/train");

  return res.data?.map((t) => t.name) ?? [];
}
