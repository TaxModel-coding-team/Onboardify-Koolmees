import { role } from "./role";

export interface User{
    id: number;
    email: string;
    username: string;
    experiencepoints: number;
    userRoles: role[];
    qrcode: any;
  }