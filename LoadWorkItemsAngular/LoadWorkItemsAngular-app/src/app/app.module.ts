
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AlertModule} from 'ngx-bootstrap';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { AppComponent } from './app.component';
import { ListaComponent } from './lista/lista.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    ListaComponent
  ],
  imports: [
    BrowserModule,
    PaginationModule.forRoot(),
    AlertModule.forRoot(),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
