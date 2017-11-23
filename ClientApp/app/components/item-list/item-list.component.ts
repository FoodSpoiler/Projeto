import { ItemService } from './../../services/item.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {

  itens: any[];
  idCardapio;

  constructor(private itemservice: ItemService, 
              private router: ActivatedRoute){

      router.params.subscribe(param => this.idCardapio = param['id'])
  }

  ngOnInit() {

    this.itemservice.getItens(this.idCardapio)
      .subscribe(result => this.itens = result);
  }

}
