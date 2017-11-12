import { ItemService } from './../../services/item.service';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { Location } from '@angular/common';

@Component({
  selector: 'app-item-form',
  templateUrl: './item-form.component.html',
  styleUrls: ['./item-form.component.css']
})
export class ItemFormComponent implements OnInit {

  //Properties
  item: any = {
    itemId: 0,
    nome: '',
    preco: 0,
    CardapioId: 0
  }
  idForn;
  //  @Input() idForn;

  constructor(private itemService: ItemService,
              private route: ActivatedRoute,
              private router: Router,
              private location: Location) {

      route.params.subscribe(param => this.item.CardapioId = param['id']);
      route.params.subscribe(param => this.idForn = param['fornId']);
      route.params.subscribe(param => this.item.itemId = param['itemId']);
  }

  ngOnInit() {
    if (this.item.itemId)
        this.itemService.getItem(this.item.itemId)
          .subscribe(result => this.item = result);
  }

  submit(){

    if(this.item.itemId){
      this.itemService.updade(this.item)
        .subscribe(x => console.log(x));
      
        console.log("Caiu no If");
    }

    else{
      this.itemService.create(this.item)
        .subscribe(x => console.log(x));
      
      console.log("Caiu no Else");
    }

    // this.router.navigate(['/cardapios/', this.idForn]);
    this.router.navigate(['/fornecedores/']);
  }

  delete() {
    if (confirm("Tem certeza?")) {
      this.itemService.delete(this.item.itemId)
        .subscribe(x => {
          this.router.navigate(['/cardapios/', this.idForn]);
        });
    }
  }

}

