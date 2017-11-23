import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';
import { Router } from "@angular/router";
import { AuthHttp } from "angular2-jwt/angular2-jwt";

@Injectable()
export class FornecedorService {

  constructor(public http: Http,
              private route:Router,
              private authHttp: AuthHttp      //so pra mandar o token junto com a requisição
  ) { }

 
  create(fornecedor){
    return this.authHttp.post('/api/fornecedores', fornecedor)
      .map(res => res.json())
      .map(id => this.route.navigate(['cardapios/novo/'+id]))
  }

  updade(fornecedor){
    return this.authHttp.put('/api/fornecedores/'+fornecedor.fornecedorId, fornecedor)
      .map(res => res.json());
  }

  delete(id) {
    return this.authHttp.delete('/api/fornecedores/'+id)
      .map(res => res.json());
  }
  
  getFornecedores(query) {
    return this.http.get('/api/fornecedores' + '?' + this.toQueryString(query))
      .map(res => res.json());
  }

  getFornecedor(id) {
    return this.http.get('/api/fornecedores/' + id)
      .map(res => res.json());
  }
  
  saveImage() {
    return this.http.get('/api/fornecedores/2')
      .map(res => res.json());
  }

  //Filtiring
  toQueryString(obj){
    var parts= [];
    for(var property in obj){

      var value = obj[property];
      if(value != null && value != undefined){
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value)); //para fazer o filtro   //prop=value&
      }
    }

    return parts.join('&');
  }
}
