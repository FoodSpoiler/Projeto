import { CardapioService } from './../../services/cardapio.service';
import { Component, OnInit , Input } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-cardapio-form',
  templateUrl: './cardapio-form.component.html',
  styleUrls: ['./cardapio-form.component.css']
})
export class CardapioFormComponent implements OnInit {

  cardapio : any = {
    id: 0,
    nome: '',
    FornecedorId: 0
  }


  constructor(private cardapioService: CardapioService, 
              private router: ActivatedRoute){

      router.params.subscribe(param => this.cardapio.FornecedorId = param['id'])
  }

  ngOnInit() {
    
  }

  submit(){
    this.cardapioService.create(this.cardapio, this.cardapio.FornecedorId)
      .subscribe(x => console.log(x));
  }

}

