import { Item } from './../item';
import { HttpClient  } from '@angular/common/http';
import { ListaService } from './shared/lista.service';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, combineLatest } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-lista',
  templateUrl: './lista.component.html',
  styleUrls: ['./lista.component.css'],
  providers: [ListaService]
})
export class ListaComponent implements OnInit {
  toDoListArray: any[];
  taskOptions = ['Bug', 'Epic', 'Feature', 'Issue', 'Task', 'Test Case', 'User Story'];
  taskFilterControl = new FormControl();
  itens$ = new BehaviorSubject<any[]>([]);
  filteredItens$ = new BehaviorSubject<any[]>([]);
  constructor(private listaService: ListaService, private http: HttpClient) { }

  ngOnInit() {
    this.http.get('http://localhost:53292/api/item').subscribe( (res) => {
      this.itens$.next(JSON.parse(JSON.stringify(res)));
      this.setFilters();
    });
  }
  private setFilters() {
    this.filteredItens$.next(this.itens$.value);
    combineLatest(
      this.itens$,
      this.taskFilterControl.valueChanges,
    )
    .subscribe(([itens, taskFilter]) => {
      let filtereditens = [ ... itens ];

      if (taskFilter) {
        filtereditens = filtereditens.filter(item => item.Type === taskFilter);
      }
      this.filteredItens$.next(filtereditens);
    });

    this.taskFilterControl.setValue('');
  }
  sortTable () {
    this.filteredItens$.value.sort((a,b) => {
      return -1 ;
     });
  }   
  
}
