"use client";

import { useEffect, useState } from "react";
import { HTTP_STATUS } from "@/constants/statusCodes";
import { fetchIncidents, fetchTrainsApi } from "@/lib/api";
import type { ApiResponse } from "@/types/api";

import type { Incident } from "@/types/incident";

import { Spinner } from "../ui/spinner";
import { FiInbox } from "react-icons/fi";
import { PiWarning } from "react-icons/pi";
import { Train } from "@/types/train";

const REFRESH_INTERVAL = 15_000;

export default function IncidentCard() {
  const [response, setResponse] = useState<ApiResponse<Incident[]> | null>(
    null
  );
  const [trains, setTrains] = useState<ApiResponse<Train[]> | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let isMounted = true;

    const fetchIncidentsAndTrains = async () => {
      try {
        const res = await fetchIncidents();
        const trainRes = await fetchTrainsApi();
        if (isMounted && res.status === HTTP_STATUS.OK) {
          setResponse(res);
          setTrains(trainRes);
        }
      } finally {
        if (isMounted) setLoading(false);
      }
    };

    fetchIncidentsAndTrains();
    const intervalId = setInterval(fetchIncidentsAndTrains, REFRESH_INTERVAL);

    return () => {
      isMounted = false;
      clearInterval(intervalId);
    };
  }, []);

  /* ---------- loading ---------- */
  if (loading) {
    return (
      <div className="h-64 flex justify-center items-center text-gray-500">
        <Spinner />
      </div>
    );
  }

  /* ---------- empty state ---------- */
  if (response?.data?.length === 0) {
    return (
      <div className="h-64 flex flex-col justify-center items-center gap-3 text-gray-400">
        <FiInbox size={72} />
      </div>
    );
  }

  /* ---------- incidents ---------- */
  return (
    <div className="flex flex-col gap-4 items-center  mt-5">
      {response?.data?.map((incident) => (
        <div
          key={incident.id}
          className="
      w-full
      flex items-center gap-4
      p-2 rounded-xl
      bg-red-50 border border-orange-100
      shadow-sm
      w-full
      max-w-[100%]       
      sm:max-w-[450px]  
      md:max-w-[380px]  
      lg:max-w-[500px]  
      xl:max-w-[600px]  
      2xl:max-w-[700px]

      transform transition-transform duration-200 ease-in-out
      hover:scale-105
      hover:shadow-lg
    "
        >
          <PiWarning size={36} className="text-red-700 shrink-0" />

          <div className="flex flex-col">
            <span className="text-sm font-semibold text-red-700">
              Incident: {incident.reason} on train:{" "}
              {trains?.data?.find((x) => x.id === incident.trainId)?.name}
            </span>
            <span className="text-gray-800">
              Created by: {incident.username}
            </span>
            <span className="text-gray-800">Comment: {incident.comment}</span>
          </div>
        </div>
      ))}
    </div>
  );
}
