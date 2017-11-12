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

  
  create(cardapio, FornecedorId){
    return this.authHttp.post('/api/cardapios', cardapio)
      .map(res => res.json())
      .map(id => this.route.navigate(['itens/novo/'+id+'/'+FornecedorId]));
  }

 
}

