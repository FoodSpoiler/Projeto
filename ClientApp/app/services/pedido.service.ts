import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class PedidoService {

  constructor(private http: Http) { }

  getPedidos(){
    return this.http.get("/api/pedidos")
      .map(res => res.json())
  }

  getPedidosByName(email) {
    return this.http.get('/api/pedidos/byemail' + '?' + 'email' + '=' + email)
      .map(res => res.json());
  }

}
