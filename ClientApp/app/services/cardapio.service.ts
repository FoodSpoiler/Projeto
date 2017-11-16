import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';
import { Router } from "@angular/router";
import { AuthHttp } from "angular2-jwt/angular2-jwt";

@Injectable()
export class CardapioService {

  constructor(private http: Http,
              private route:Router,
              private authHttp: AuthHttp
  ) {}

  getCardapios() {
    return this.http.get('/api/cardapios')
      .map(res => res.json());
  }

  getCardapio(fornId) {
    return this.http.get('/api/cardapios/' + fornId)
      .map(res => res.json())
  }

  getCard(cardId, fornId) {
    return this.http.get('/api/cardapios/'+cardId+'/'+fornId)
      .map(res => res.json())
  }

  create(cardapio, FornecedorId){
    return this.authHttp.post('/api/cardapios', cardapio)
      .map(res => res.json())
      .map(id => this.route.navigate(['itens/novo/'+id+'/'+FornecedorId]));
  }

  updade(cardId, fornId, cadapio){
    return this.authHttp.put('/api/cardapios/'+cardId+'/'+fornId, cadapio)
      .map(res => res.json())
      .map(id => this.route.navigate(['cardapios/'+fornId]));
  }

  delete(id) {
    return this.authHttp.delete('/api/cardapios/' + id)
      .map(res => res.json());
  }

  deleteItem(id){
    return this.http.delete('/api/itens/'+id)
      .map(res => res.json());
  }


  //Enviar o Pedido
  postPedido(pedido){
    return this.http.post('/api/pedidos', pedido)
        .map(res => res.json());
  }

}
