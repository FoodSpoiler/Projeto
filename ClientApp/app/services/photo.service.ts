import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class PhotoService {

    constructor(private http: Http) { }

    upload(fornecedorId, photo){
        var formData = new FormData();
        formData.append('file', photo);

        return this.http.post(`/api/fornecedores/${fornecedorId}/photos`, formData) //para evitar ficar fazendo concatenação, Tipo: '/api/fornecedores/+vehicleId+/photos'
            .map(res => res.json());
    }

    getPhotos(fornecedorId){
        return this.http.get(`/api/fornecedores/${fornecedorId}/photos`)
            .map(res => res.json());
    }
}