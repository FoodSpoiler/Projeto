
import { AdminComponent } from './components/admin/admin.component';
import { ItemService } from './services/item.service';
import { CardapioService } from './services/cardapio.service';
import { FornecedorService } from './services/fornecedor.service';
import * as Raven from 'raven-js'; 
import { FormsModule } from '@angular/forms'; 
import { NgModule, ErrorHandler } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
import { UniversalModule } from 'angular2-universal';
import { BrowserModule } from '@angular/platform-browser';


import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { CardapioFormComponent } from "./components/cardapio-form/cardapio-form.component";
import { FornecedorFormComponent } from './components/fornecedor-form/fornecedor-form.component';
import { ItemFormComponent } from "./components/item-form/item-form.component";
import { FornecedorListComponent } from './components/fornecedor-list/fornecedor-list.component';
import { ItemListComponent } from './components/item-list/item-list.component';
import { PaginationComponent } from "./components/shared/pagination.component";
import { PhotoService } from "./services/photo.service";
import { ProgressService } from "./services/progress.service";
import { Auth } from "./services/auth.service";
import { AuthGuard } from "./services/auth-gaurd.service";
import { AUTH_PROVIDERS } from "angular2-jwt/angular2-jwt";
import { AdminAuthGuard } from "./services/admin-auth-guard.service";
import { AppErrorHandler } from './app.error-handler';

//para pegar os logs de erro do Sentry
Raven.config('https://b0d3ef84952f44c9b73b8161359b5167@sentry.io/237084').install();

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AdminComponent,
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        CardapioFormComponent,
        FornecedorFormComponent,
        ItemFormComponent,
        FornecedorListComponent,
        ItemListComponent,
        PaginationComponent,
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        ToastyModule.forRoot(), 
        BrowserModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'fornecedores', pathMatch: 'full' },
            { path: 'admin', component: AdminComponent, canActivate: [ AdminAuthGuard ] }, //vai ser mostrada apenas se o user tiver o Admin Role
            { path: 'fornecedores/novo', component: FornecedorFormComponent, canActivate: [ AdminAuthGuard ] },
            { path: 'fornecedores/edit/:id', component: FornecedorFormComponent, canActivate: [ AdminAuthGuard ] },
            { path: 'itens/edit/:itemId/:fornId', component: ItemFormComponent },
            { path: 'cardapios/novo/:id', component: CardapioFormComponent, canActivate: [ AdminAuthGuard ] },
            { path: 'itens/novo/:id/:fornId', component: ItemFormComponent, canActivate: [ AdminAuthGuard ] },         
            { path: 'fornecedores', component: FornecedorListComponent },
            { path: 'fornecedores/:id', component: FornecedorFormComponent },
            { path: 'itens', component: ItemListComponent },
            { path: 'itens/:id', component: ItemListComponent },
            //{ path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
      { provide: ErrorHandler, useClass: AppErrorHandler },
      Auth,
      AuthGuard,
      AUTH_PROVIDERS,
      AdminAuthGuard,
      FornecedorService,
      CardapioService,
      ItemService,
      PhotoService,
      //ProgressService DEPOIS DEVO COLOCAR ELE NO COMPONENT Q FAZ O UP DE FOTOS PRA MANDAR LEC150
    ]
})
export class AppModule {
}

