import { CardapioService } from './../../services/cardapio.service';
import { Component, OnInit , Input } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { ToastyService } from 'ng2-toasty';

@Component({
  selector: 'app-cardapio-form',
  templateUrl: './cardapio-form.component.html',
  styleUrls: ['./cardapio-form.component.css']
})
export class CardapioFormComponent implements OnInit {

  cardapio : any = {
    id: 0,
    nome: '',
    FornecedorId: 0,
    cardapioId: 0
  }
  FornecedorId2;


  constructor(private cardapioService: CardapioService, 
              private toastyService: ToastyService,
              private route: Router,
              private router: ActivatedRoute){

      router.params.subscribe(param => this.cardapio.FornecedorId = param['id']);
      router.params.subscribe(param => this.cardapio.cardapioId = param['cardapioId']);
      router.params.subscribe(param => this.FornecedorId2 = param['fornId']);
  }

  ngOnInit() {
    if (this.cardapio.cardapioId){
    //console.log(this.cardapio.cardapioId);
    //console.log(this.cardapio.FornecedorId2);
    this.cardapioService.getCard(this.cardapio.cardapioId, this.FornecedorId2)
      .subscribe(result => this.cardapio = result);
    }
  }

  submit(){

    if(this.cardapio.cardapioId){
      this.cardapioService.updade(this.cardapio.cardapioId, this.FornecedorId2, this.cardapio)
      .subscribe(x => {
        this.toastyService.success({
          title: 'Success',
          msg: 'Cardapio Alterado com Sucesso.',
          theme: 'bootstrap',
          showClose: true,
          timeout: 5000
        })
      });
    }

    else{
      this.cardapioService.create(this.cardapio, this.cardapio.FornecedorId)
      .subscribe(x => {
        this.toastyService.success({
          title: 'Success',
          msg: 'Cardapio Cadastrado com Sucesso.',
          theme: 'bootstrap',
          showClose: true,
          timeout: 5000
        })
      });
    }
  }

  delete() {
    if (confirm("Tem certeza?")) {
      this.cardapioService.delete(this.cardapio.cardapioId)
        .subscribe(x => {
          this.toastyService.success({
            title: 'Success',
            msg: 'Cardapio Excluido com Sucesso.',
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          })
          this.route.navigate(['/cardapios/', this.FornecedorId2]);
        });
    }
  }

}
