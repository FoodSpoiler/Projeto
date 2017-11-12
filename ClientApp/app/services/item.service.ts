import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { Router } from "@angular/router";
import { AuthHttp } from "angular2-jwt/angular2-jwt";

@Injectable()
export class ItemService {

  constructor(private http: Http,
              private route:Router,
              private authHttp: AuthHttp
  ) { }

  create(item){
    return this.authHttp.post('/api/itens', item)
      .map(res => res.json());
  }

  getItens(cardId) {
    return this.http.get('/api/itens/' + cardId)
      .map(res => res.json())
  }

  updade(item){
    return this.authHttp.put('/api/itens/'+item.itemId, item) 
      .map(res => res.json());
  }

  delete(id) {
    return this.authHttp.delete('/api/itens/' + id)
      .map(res => res.json());
  }

  getItem(id) {
    return this.http.get('/api/itens/'+id+'/'+7) //nesse caso eu coloquei o 7 apenas pra quando chegar na controler, ela saber q e pra chamar o Action que tiver alem do parametro id tem um outro paraamentro, so pra separar da outra Achtion
      .map(res => res.json());
  }

}

