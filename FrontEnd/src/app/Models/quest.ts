import { Subquest } from "./subquest";


export interface Quest {
    id: number;
    title: string;
    description: string;
    exp: number;
    questRole: String;
    subQuests: Subquest[];
  }