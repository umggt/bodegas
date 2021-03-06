﻿import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Headers, RequestMethod, URLSearchParams } from "@angular/http"
import { ProductoResumen, Producto, TipoCaracteristica } from "./productos.modelos"
import { PaginacionResultado, PaginacionParametros } from "../modelos"
import { HttpServicio } from "../http.servicio"
import { MarcasServicio } from "./marcas.servicio"
import { UnidadesDeMedidaServicio } from "./unidades-de-medida.servicio"
import { ListasServicio } from "./listas.servicio"

@Injectable()
export class ProductosServicio {

    private url = "http://localhost:5002/api/core/productos/";
    private urlCaracteristicas = "http://localhost:5002/api/core/tiposdecaracteristicas";

    constructor(private http: HttpServicio, private marcas: MarcasServicio, private unidadesDeMedida: UnidadesDeMedidaServicio, private listas: ListasServicio) {
    }

    public obtenerTodos(paginacion?: PaginacionParametros): Observable<PaginacionResultado<ProductoResumen>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, { search: params }).map(x => x.json() as PaginacionResultado<ProductoResumen>);
    }

    public obtenerUnico(id: number): Observable<Producto> {
        return this.http.get(this.url + id).map(x => x.json() as Producto);
    }

    public guardar(producto: Producto): Observable<Producto> {
        const body = JSON.stringify(producto);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (producto.id) {
            url = this.url + producto.id;
            method = RequestMethod.Put;
        }

        return this.http.request(url, { body: body, method: method }).map(x => x.json() as Producto);
    }

    public eliminar(productoId: number) {
        let url = this.url + productoId;
        return this.http.delete(url);
    }

    public obtenerMarcas() {
        return this.marcas.obtenerTodos();
    }

    public obtenerUnidadesDeMedida() {
        return this.unidadesDeMedida.obtenerTodos();
    }

    public obtenerCaracteristicas() : Observable<TipoCaracteristica[]> {
        return this.http.get(this.urlCaracteristicas).map(x => x.json() as TipoCaracteristica[]);
    }

    public obtenerListas() {
        return this.listas.obtenerTodas();
    }

}