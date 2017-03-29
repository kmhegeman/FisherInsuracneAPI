import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import 'rxjs/Rx';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavBarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { ClaimsComponent } from './components/claims/claims.component';
import { QuotesComponent } from './components/quotes/quotes.component';

import { AppRouting } from './app.routing';
import { AuthService } from "./auth.service";
import { AuthHttp } from "./auth.http";


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        NavBarComponent,
        LoginComponent,
        ClaimsComponent,
        QuotesComponent
    ],
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        AppRouting
    ],
    providers: [
        AuthService,
        AuthHttp
    ],
    bootstrap: [
        AppComponent
    ],
})
export class AppModule { }