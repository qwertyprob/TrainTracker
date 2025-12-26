"use client";

import * as React from "react";
import Autoplay from "embla-carousel-autoplay";

import { MdTrain } from "react-icons/md";

import { Card, CardContent } from "@/components/ui/card";
import {
  Carousel,
  CarouselContent,
  CarouselItem,
} from "@/components/ui/carousel";
import { FaTrain } from "react-icons/fa";
import type { ApiResponse } from "@/types/api";
import type { Train } from "@/types/train";
import { HTTP_STATUS } from "@/constants/statusCodes";
import { fetchTrainsWithZeroDelay } from "@/lib/api";
import NoData from "../noData";
import { Spinner } from "../ui/spinner";

const REFRESH_INTERVAL = 15_000;

export default function ArrivedTrainsCarousel() {
  const autoplay = React.useRef(
    Autoplay({ delay: 3000, stopOnInteraction: true })
  );

  const [response, setResponse] = React.useState<ApiResponse<Train[]> | null>(
    null
  );
  const [loading, setLoading] = React.useState(true);

  React.useEffect(() => {
    let isMounted = true;

    const fetchTrains = async () => {
      try {
        const res = await fetchTrainsWithZeroDelay();
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
      <div className="text-sm text-gray-500 p-3  h-75 flex justify-center items-center">
        <Spinner />
      </div>
    );
  }

  if (!response || response.data?.length === 0) {
    return (
      <div className="pt-10">
        <MdTrain size={80} className="text-gray-500 mb-3" />
      </div>
    );
  }

  return (
    <Carousel
      plugins={[autoplay.current]}
      className="w-full 
  max-w-[100%]       
  sm:max-w-[450px]  
  md:max-w-[380px]  
  lg:max-w-[500px]  
  xl:max-w-[600px]  
  2xl:max-w-[700px] 
  m-4 "
      onMouseEnter={autoplay.current.stop}
      onMouseLeave={autoplay.current.reset}
    >
      <CarouselContent className="flex gap-4">
        {response.data?.map((train) => (
          <CarouselItem key={train.id}>
            <Card className="flex m-0 mx-2 border-0 cursor-grab rounded-2xl shadow-md mb-4  hover:shadow-lg transition-all duration-300 ease-in-out bg-white border border-gray-200">
              <CardContent className="flex justify-between items-center p-5">
                {/* Левый блок с информацией */}
                <div className="flex flex-col space-y-3">
                  {/* Верхняя строка: Иконка + Название + ID */}
                  <div className="flex items-center space-x-3">
                    <FaTrain className="text-blue-600 text-2xl" />
                    <div className="flex flex-col">
                      <div className="text-xl font-semibold text-gray-900">
                        {train.name}
                      </div>
                      <div className="text-xs text-gray-400">
                        ID: {train.id}
                      </div>
                    </div>
                  </div>

                  {/* Номер поезда */}
                  <div className="text-sm text-gray-600">
                    Number:{" "}
                    <span className="ml-2 inline-block bg-blue-50 text-blue-700 px-3 py-0.5 rounded-lg font-medium shadow-sm">
                      #{train.number}
                    </span>
                  </div>

                  {/* Следующая станция */}
                  {train.nextStation.title && (
                    <div className="text-sm text-gray-500">
                      Next station:{" "}
                      <span className="ml-1 font-medium text-gray-700">
                        {train.nextStation.title}
                      </span>
                    </div>
                  )}
                </div>

                {/* Правый блок с временем прибытия */}
                <div className="flex flex-col items-end justify-center">
                  <span
                    className={`text-2xl font-bold ${
                      train.delayTime === 0 ? "text-green-600" : "text-gray-600"
                    }`}
                  >
                    {train.delayTime === 0 ? "Now" : `${train.delayTime} min`}
                  </span>
                  <div className="text-xs text-gray-400 mt-1">Arriving</div>
                </div>
              </CardContent>
            </Card>
          </CarouselItem>
        ))}
      </CarouselContent>
    </Carousel>
  );
}
