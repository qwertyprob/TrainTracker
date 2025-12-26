"use client";
import { useEffect, useState } from "react";
import { HTTP_STATUS } from "@/constants/statusCodes";
import { fetchTrainsApi } from "@/lib/api";
import type { ApiResponse } from "@/types/api";
import type { Train } from "@/types/train";
import NoData from "../noData";

const REFRESH_INTERVAL = 15_000;

import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Spinner } from "../ui/spinner";

export default function DataTable() {
  const [response, setResponse] = useState<ApiResponse<Train[]> | null>(null);
  const [loading, setLoading] = useState(true);
  let counter = 0;
  useEffect(() => {
    let isMounted = true;

    const fetchTrains = async () => {
      try {
        const res = await fetchTrainsApi();
        if (isMounted && res.status === HTTP_STATUS.OK) {
          setResponse(res);
        }
      } finally {
        if (isMounted) setLoading(false);
      }
    };

    // initial fetch
    fetchTrains();

    // repeat every 15sec
    const intervalId = setInterval(fetchTrains, REFRESH_INTERVAL);

    // cleanup
    return () => {
      isMounted = false;
      clearInterval(intervalId);
    };
  }, []);

  if (loading) {
    return (
      <div className="text-sm text-gray-500 p-3  h-75 flex justify-center items-center">
        <Spinner />
      </div>
    );
  }

  if (!response || response.data?.length === 0) {
    return (
      <div className="pt-10">
        <NoData />
      </div>
    );
  }

  return (
    <div className="bg-white rounded-2xl shrink-0 overflow-auto h-75 cursor-pointer">
      <Table>
        <TableHeader className="bg-gray-100  hover:bg-gray-50 duration-300 ease-in">
          <TableRow className="text-center font-bold">
            <TableHead>N</TableHead>
            <TableHead>Number</TableHead>
            <TableHead>Train</TableHead>
            <TableHead>Arriving</TableHead>
            <TableHead>Next station</TableHead>
          </TableRow>
        </TableHeader>

        <TableBody>
          {response.data?.map((row) => (
            <TableRow key={row.id}>
              <TableCell>{++counter}</TableCell>
              <TableCell>{row.number}</TableCell>
              <TableCell>{row.name}</TableCell>
              <TableCell
                className={`font-semibold ${
                  row.delayTime === 0 ? "text-green-600" : "text-gray-600"
                }`}
              >
                {row.delayTime === 0 ? "Now" : `${row.delayTime} min`}
              </TableCell>
              <TableCell>{row.nextStation.title}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}
