import { Component, OnInit } from '@angular/core';
import { Pedido } from "../../models/fornecedor";
import { PedidoService } from "../../services/pedido.service";

@Component({
  selector: 'app-pedido-list',
  templateUrl: './pedido-list.component.html',
  styleUrls: ['./pedido-list.component.css']
})
export class PedidoListComponent implements OnInit {

  //properties
  profile: any;
  pedidos: Pedido[] = [];

  constructor(private pedidoService: PedidoService) { 
    //Para pegar as coisas do profile
    this.profile = JSON.parse(localStorage.getItem('profile'));
  }

  ngOnInit() {

    this.populatePedidos();

  }

  //methods
  getTotal(id) {
    var total = 0;

    this.pedidos.forEach(element => {
      element.itens.forEach(product => {

        if (element.pedidoId == id) {
          total += product.preco;
        }
      });
    });

    return total;
  }

  getTotalGeral() {
    var total = 0;

    this.pedidos.forEach(element => {
      element.itens.forEach(product => {

        total += product.preco;
      });
    });

    return total;
  }

  private populatePedidos(){
    this.pedidoService.getPedidosByName(this.profile.email)
    .subscribe(result => this.pedidos = result);
  }

}



