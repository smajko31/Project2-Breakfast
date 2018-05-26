import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from "@angular/router";
import { WeatherComponent } from './weather/weather.component';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';

const appRoutes: Routes = [
  { path: "weather", component: WeatherComponent }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes)
  ],
  declarations: []
})
export class AppRoutingModule { }