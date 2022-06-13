import {Quest} from "./quest";
import {Role} from "./role";

export interface User {
  user: Promise<boolean>;
  id: number;
  email: string;
  username: string;
  experiencepoints: number;
  qrcode: any;
  roles: Role[];
  userQuestsByRole: Quest[];
}
