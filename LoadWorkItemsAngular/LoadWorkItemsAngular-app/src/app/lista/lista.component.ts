import { HttpClient  } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, combineLatest } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
import { PageChangedEvent } from 'ngx-bootstrap';

@Component({
  selector: 'app-lista',
  templateUrl: './lista.component.html',
  styleUrls: ['./lista.component.css'],
})
export class ListaComponent implements OnInit {
  taskOptions = ['Bug', 'Epic', 'Feature', 'Issue', 'Task', 'Test Case', 'User Story'];
  itens$ = new BehaviorSubject<any[]>([]);
  filteredItens$ = new BehaviorSubject<any[]>([]);
  totalItems = 0;
  currentPage = 1;
  type = '';

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getItemsPages();
  }

  sortTable() {
    this.itens$.value.sort((a, b) => {
      return -1 ;
     });
  }
  getItemsPages(){
    this.http.get('http://localhost:53292/api/item?&pageNumber=' + this.currentPage + '&type=' + this.type).subscribe( (res) => {
      this.totalItems = res['m_Item2'];
      this.itens$.next(JSON.parse(JSON.stringify(res['m_Item1'])));
    });
  }

  setPage(pageNo: number): void {
    this.currentPage = pageNo;
    this.getItemsPages();
  }
  pageChanged(event: PageChangedEvent): void {
      this.currentPage = event.page;
      this.getItemsPages();
  }

  clearType() {
    this.type = '';
    this.getItemsPages();
  }
}
