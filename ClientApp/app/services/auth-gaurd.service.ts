import { CanActivate } from '@angular/router';
import { Auth } from './auth.service';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthGuard implements CanActivate { //canActivate é o bloqueia as rotas

  constructor(protected auth: Auth) { }

  canActivate() {                   //isso para verificar se esta logado ou nao
    if (this.auth.authenticated())
      return true;

    //para direcionar para uma url externa usamos o window.locaito.heref
    window.location.href = 'https://agilfood.auth0.com/login?client=2cWXmseW19yZ246Wbkh51XASarvolj2g'; //se nao estiver logado, vai direcionar pra pagina de login
    return false;
  }
}
