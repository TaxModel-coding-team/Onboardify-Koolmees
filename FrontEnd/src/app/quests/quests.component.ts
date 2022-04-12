
import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {CookieService} from 'ngx-cookie-service';
import {Subscription} from 'rxjs';
import {textChangeRangeIsUnchanged} from 'typescript';
import {Quest} from '../Models/quest';
import {QuestService} from '../Services/quest.service';
import {BarcodeFormat} from "@zxing/browser";
import {ZXingScannerComponent} from "@zxing/ngx-scanner";
import {AlertComponent} from "../alert/alert.component";
import { RoleServices } from '../Services/role.service';


@Component({
  selector: 'app-quests',
  templateUrl: './quests.component.html',
  styleUrls: ['./quests.component.css']
})
export class QuestsComponent implements OnInit, OnDestroy {

  //Fields
  public quests: Quest[] = [];
  private user : User = JSON.parse(this.cookies.get("user"))
  public greeting: String = '';
  private subscription: Subscription = new Subscription();
  public allowedFormats = [BarcodeFormat.QR_CODE];
  @ViewChild('scanner', {static: false})
  private scanner: ZXingScannerComponent | undefined;
  public showModal: boolean = false;
  public btnVariable: any;
  @ViewChild(AlertComponent, {static: false})
  private alert: AlertComponent | undefined;




  constructor(private questService: QuestService, private roleservice: RoleServices,
    private cookies: CookieService) { }


  ngOnInit(): void {
    this.getQuests()
    this.getGreeting();
  }

  //Getting all quests from API and caching to observable
  public getQuests(): void {
      this.subscription.add(this.questService.getQuests()
      .subscribe(quest => this.quests = quest)) 
      this. quests = this.user.userQuestsByRole;    
  }

  //Simple greeting based on your time of day
  public getGreeting(): void {

    const today = new Date();
    const curHr = today.getHours();

    if (curHr < 12) {
      this.greeting = 'Good morning ';
    } else if (curHr < 18) {
      this.greeting = 'Good afternoon ';
    } else {
      this.greeting = 'Good evening ';
    }
    this.greeting += JSON.parse(this.cookies.get("user")).username
  }

  public completeQuest(subquestId: number): void {
    this.btnVariable = subquestId;
    this.showScanner();
  }

  public submitQuestComplete(subquestId: number): void {
    let userId = JSON.parse(this.cookies.get("user")).id

    if (this.btnVariable == subquestId) {
      this.questService.completeQuest(userId.toString(), subquestId.toString()).subscribe(response => {
        console.log(response);
        if (response) {
          this.alert?.showAlert("success", "Deze quest heb je helemaal super afgerond! Goed gedaan!");
          this.showScanner();
        }
        let subQuest: any;
        this.quests.forEach(quest => {
          console.log(quest)
          subQuest = quest.subQuests.find(subQuest => subQuest.id = subquestId)
          if (subQuest) {
            subQuest.completed = true;
            return;
          }
        })
      })
    } else{
      console.log("Niet de goede Quest!");
      this.alert?.showAlert("danger", "Deze QR-code past niet bij de quest!");
      this.showScanner();
    }

  }

  public handleScanResult(resultString: any): void {
    this.submitQuestComplete(resultString);
  }

  public showScanner(): any {
    this.showModal = !this.showModal;
  }

  //Unsubscribe from all made subscriptions to prevent background processes and possible memory leakage.
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
