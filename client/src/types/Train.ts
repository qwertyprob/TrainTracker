export interface Train {
  id: number;
  name: string;
  number: number;
  delayTime: number;
  updateTime : number;
  nextStation: { title: string };
  incidents: string[];
}