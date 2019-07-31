import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ListaService {
  toDoList = ['Windstorm', 'Bombasto', 'Magneta', 'Tornado'];
  constructor() { }

  getList(){
    return this.toDoList;
  }
}
