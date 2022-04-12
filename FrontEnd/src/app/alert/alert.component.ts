import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit {
  public showPrimary: Boolean = false;
  public showSecondary: Boolean = false;
  public showSuccess: Boolean = false;
  public showDanger: Boolean = false;
  public showWarning: Boolean = false;
  public showInfo: Boolean = false;
  public active: Boolean = false;
  public content: string = "Default.";

  constructor() {
  }

  ngOnInit(): void {
  }

  public showAlert(type: string, content: string) {
    this.active = true;
    this.content = content;
    console.log(this.active, this.content);
    switch (type) {
      case "primary":
        this.showPrimary = true;
        break;
      case "secondary" :
        this.showSecondary = true;
        break;
      case "success" :
        this.showSuccess = true;
        break;
      case "danger":
        this.showDanger = true;
        break;
      case "warning":
        this.showWarning = true;
        break;
      case "info":
        this.showInfo = true;
        break;
      default :
        this.showPrimary = false;
        this.showSecondary = false;
        this.showSuccess = false;
        this.showDanger = false;
        this.showWarning = false;
        this.showInfo = false;
    }

    setTimeout(() => {
      this.closeAlert();
    }, 5000);
  }

  public closeAlert() {
    this.active = false;
    this.content = "Default.";
    this.showPrimary = false;
    this.showSecondary = false;
    this.showSuccess = false;
    this.showDanger = false;
    this.showWarning = false;
    this.showInfo = false;
  }
}
