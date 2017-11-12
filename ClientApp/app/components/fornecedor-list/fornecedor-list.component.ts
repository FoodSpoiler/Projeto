import { FornecedorService } from './../../services/fornecedor.service';
import { Component, OnInit } from '@angular/core';
import { Auth } from "../../services/auth.service";

@Component({
  selector: 'app-fornecedor-list',
  templateUrl: './fornecedor-list.component.html',
  styleUrls: ['./fornecedor-list.component.css']
})
export class FornecedorListComponent implements OnInit {

  private readonly PAGE_SIZE = 3; 

  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE //Record Per Page
  };
  photo;

  constructor(private fornecedorService: FornecedorService,
              private auth: Auth) { }

  ngOnInit() {
    this.populateFornecedores();

    //this.photo= this.queryResult.items.photos[0];
  }

  private populateFornecedores() {
    this.fornecedorService.getFornecedores(this.query)
      .subscribe(result => this.queryResult = result);
  }

  //Paging
  onPageChange(page) {
    this.query.page = page; 

    this.populateFornecedores();
  }
}

