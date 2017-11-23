import * as Raven from 'raven-js'; 
import { ToastyService } from 'ng2-toasty';
import { ErrorHandler, Inject, NgZone, isDevMode } from '@angular/core';

export class AppErrorHandler implements ErrorHandler {
  constructor(
    private ngZone: NgZone,
    @Inject(ToastyService) private toastyService: ToastyService) {
  }

  handleError(error: any): void {
    this.ngZone.run(() => {
      this.toastyService.error({
        title: 'Error',
        msg: 'Um erro inesperado aconteceu, entre em contato conosco',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      });
    });

    if (!isDevMode())
      Raven.captureException(error.originalError || error);
    else 
      throw error;
  }
}

//Uso isso para centralizar sempre que for detectado um erro no Servidor, para direcinar pra essa pagina