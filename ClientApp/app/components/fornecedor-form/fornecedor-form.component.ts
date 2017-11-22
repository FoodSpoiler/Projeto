import { Fornecedor } from './../../models/fornecedor';
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
    foto: '',
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
    console.log(this.fornecedor)
    if (this.fornecedor.fornecedorId) {
      this.fornecedorService.updade(this.fornecedor)
      .subscribe(x => {
        this.toastyService.success({
          title: 'Success',
          msg: 'Fornecedor Alterado com Sucesso.',
          theme: 'bootstrap',
          showClose: true,
          timeout: 5000
        })
      });
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

    this.router.navigate(['/fornecedores/']);
  }
  uploadPhoto(fileInput: any) {
    var teste='';
    var reader = new FileReader();
    reader.readAsDataURL(fileInput.target.files[0]);
    reader.onload = (a:any) => {
      this.fornecedor.foto  = reader.result
    };
    // var formData = new FormData();
    // formData.append('file', fileInput.target.files[0]);

    // return this.fornecedorService.http.post(`/api/fornecedores/2`, formData )
    //   .subscribe(a => {
    //     this.toastyService.success({
    //       title: 'Success',
    //       msg: 'Imagem com sucesso.',
    //       theme: 'bootstrap',
    //       showClose: true,
    //       timeout: 5000
    //     })
    //   });
  }
  delete() {
    if (confirm("Tem certeza?")) {
      this.fornecedorService.delete(this.fornecedor.fornecedorId)
        .subscribe(x => {
          this.toastyService.success({
            title: 'Success',
            msg: 'Fornecedor Excluido com Sucesso.',
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          })
          this.router.navigate(['/fornecedores']);
        });
    }
  }

}
