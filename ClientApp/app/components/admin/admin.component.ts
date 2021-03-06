import { Component, OnInit } from '@angular/core';
import { Pedido } from '../../models/fornecedor';
import { PedidoService } from '../../services/pedido.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  profile: any;
  pedidos: Pedido[] = [];

  constructor(private pedidoService: PedidoService) {

    //Para pegar as coisas do profile
    this.profile = JSON.parse(localStorage.getItem('profile'));
  }

  ngOnInit() {

    this.pedidoService.getPedidos()
    .subscribe(result => this.pedidos = result);

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

}
