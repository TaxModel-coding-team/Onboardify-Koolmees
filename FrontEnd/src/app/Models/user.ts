import { Quest } from "./quest";
import { Role } from "./Role";

export interface User{
    user: Promise<boolean>;
    id: number;
    email: string;
    username: string;
    experiencepoints: number;
    qrcode: any;
    userRoles: Role[];
    userQuestsByRole: Quest[];
  }