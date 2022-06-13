import { APP_ID, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CookieService } from 'ngx-cookie-service';
import { Role } from '../Models/role';
import { Quest } from '../Models/quest';

@Injectable({
  providedIn: 'root'
})
export class RoleServices {  
  
  //Url's fetched from enviroments.ts 
  private API_URL= environment.API_URL;
  private roleURL = this.API_URL + '/role'

  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  }

  constructor(
    private http: HttpClient,
    private cookies: CookieService){ }
  
  //GET: fetches all quests by role
  public getQuestByRole(roleid: number): Observable<Quest[]> 
  {
    return this.http.get<Quest[]>(this.roleURL + "/" + roleid);
  }

  //GET: fetches all Roles by User
  public getRolesByUser(userid: number): Observable<Role[]> 
  {
    return this.http.get<Role[]>(this.roleURL + "/" + userid);
  }
}