import type { Incident } from "./incident";
import type { NextStation } from "./nextStation";

export type Train = {
  id: number;
  name: string;
  number: number;
  delayTime: number;
  nextStation: NextStation;
  createdAt: Date;
  lastDelayUpdateAt: Date;
  incidents: Incident[];
  isActive: boolean;
};
