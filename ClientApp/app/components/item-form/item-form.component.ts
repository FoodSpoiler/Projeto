import { ItemService } from './../../services/item.service';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { Location } from '@angular/common';
import { ToastyService } from 'ng2-toasty';

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
              private toastyService: ToastyService,
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
      .subscribe(x => {
        this.toastyService.success({
          title: 'Success',
          msg: 'Item Alterado com Sucesso.',
          theme: 'bootstrap',
          showClose: true,
          timeout: 5000
        })
      });
    }

    else{
      this.itemService.create(this.item)
      .subscribe(x => {
        this.toastyService.success({
          title: 'Success',
          msg: 'Item Cadastrado com Sucesso.',
          theme: 'bootstrap',
          showClose: true,
          timeout: 5000
        })
      });
    }

    this.router.navigate(['/cardapios/', this.idForn]);
  }

  delete() {
    if (confirm("Tem certeza?")) {
      this.itemService.delete(this.item.itemId)
        .subscribe(x => {
          this.toastyService.success({
            title: 'Success',
            msg: 'Item Excluido com Sucesso.',
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          })
          this.router.navigate(['/cardapios/', this.idForn]);
        });
    }
  }

}
