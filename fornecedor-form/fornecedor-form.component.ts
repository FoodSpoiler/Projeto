import { FornecedorService } from './../../services/fornecedor.service';
import { Component, OnInit, NgZone, ElementRef, ViewChild } from '@angular/core';
import { ProgressService, BrowserXhrWithProgress } from "../../services/progress.service";
import { PhotoService } from "../../services/photo.service";
import { BrowserXhr } from "@angular/http";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastyService } from 'ng2-toasty';

@Component({
  selector: 'app-fornecedor-form',
  templateUrl: './fornecedor-form.component.html',
  styleUrls: ['./fornecedor-form.component.css'],
  providers: [
    { provide: BrowserXhr, useClass: BrowserXhrWithProgress },
    ProgressService
  ]
})
export class FornecedorFormComponent implements OnInit {

  //Variaveis
  fornecedor: any = {
    nome: '',
    fornecedorId: 0,
    email: '',
    telefone: '',
    celular: '',
    endereco: {
      rua: '',
      numero: 0,
      cidade: '',
      bairro: '',
      cep: '',
    }
  }

  constructor(private fornecedorService: FornecedorService,
    private toastyService: ToastyService,
    private route: ActivatedRoute,
    private router: Router) {

    //Se tiver ele faz o subscribe, se nao tiver nao faz nada
    route.params.subscribe(p => {
      this.fornecedor.fornecedorId = +p['id'] || 0;
    });
  }

  ngOnInit() {
    if (this.fornecedor.fornecedorId)
      this.fornecedorService.getFornecedor(this.fornecedor.fornecedorId)
        .subscribe(result => this.fornecedor = result);
  }

  submit() {
    if (this.fornecedor.fornecedorId) {
      this.fornecedorService.updade(this.fornecedor)
        .subscribe(x => console.log(x));
    }

    else {
      this.fornecedorService.create(this.fornecedor)
        .subscribe(x => {
          this.toastyService.success({
            title: 'Success',
            msg: 'Fornecedor Cadastrado com Sucesso.',
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          })
        });
    }

    this.router.navigate(['/fornecedores/', this.fornecedor.fornecedorId])
  }

  delete() {
    if (confirm("Tem certeza?")) {
      this.fornecedorService.delete(this.fornecedor.fornecedorId)
        .subscribe(x => {
          this.router.navigate(['/fornecedores']);
        });
    }
  }

}
