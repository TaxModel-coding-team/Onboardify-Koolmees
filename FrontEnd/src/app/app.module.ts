import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import {GlassModule} from 'angular-glass';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {QuestsComponent} from './quests/quests.component';
import {ProgressBarModule} from 'angular-progress-bar';
import {MsalModule, MsalService, MSAL_INSTANCE} from '@azure/msal-angular';
import {IPublicClientApplication, PublicClientApplication} from '@azure/msal-browser';
import {MicrosoftLoginComponent} from './microsoft-login/microsoft-login.component';
import {RegistrationComponent} from './Registration/registration.component';
import {FormsModule} from '@angular/forms';
import {ProfilePageComponent} from './profile-page/profile-page.component';
import {HeaderComponent} from './header/header.component';
import {BadgesComponent} from './badges/badges.component';
import {AchievementsComponent} from './achievements/achievements.component';
import {ProfileDetailsComponent} from './profile-details/profile-details.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {RouterModule} from '@angular/router';
import {ZXingScannerModule} from '@zxing/ngx-scanner';
import {QRCodeModule} from "angularx-qrcode";
import { AlertComponent } from './alert/alert.component';


export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: '455b6cce-e291-46f6-a7d8-1dfecbe011da',
      redirectUri: 'http://localhost:4200'
    }
  })
}

@NgModule({
  declarations: [
    AppComponent,
    QuestsComponent,
    MicrosoftLoginComponent,
    RegistrationComponent,
    ProfilePageComponent,
    HeaderComponent,
    BadgesComponent,
    AchievementsComponent,
    ProfileDetailsComponent,
    AlertComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ProgressBarModule,
    MsalModule,
    FormsModule,
    GlassModule,
    NgbModule,
    RouterModule,
    ZXingScannerModule,
    QRCodeModule,
  ],
  providers: [
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory
    },
    MsalService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
