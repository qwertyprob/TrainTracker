import type { Train } from "@/types/train";

export function mapTrain(raw: any): Train {
  return {
    id: raw.id,
    name: raw.name,
    number: raw.number,
    delayTime: raw.delayTime,
    nextStation: raw.nextStation,
    createdAt: raw.createdAt,
    lastDelayUpdateAt: raw.lastDelayUpdateAt,
    incidents: raw.incidents ?? [],
    isActive: raw.isActive,
  };
}
