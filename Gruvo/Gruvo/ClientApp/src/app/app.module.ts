import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ProfileComponent } from './profile/profile.component';
import { TweetComponent } from './tweet/tweet.component';
import { MenuComponent } from './menu/menu.component';
import { ProfileService } from './profile/profile.service';

@NgModule({
  declarations: [
    AppComponent,
    ProfileComponent,
    TweetComponent,
    MenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: AppComponent, pathMatch: 'full' }
    ])
  ],
  providers: [ProfileService],
  bootstrap: [AppComponent]
})
export class AppModule { }
