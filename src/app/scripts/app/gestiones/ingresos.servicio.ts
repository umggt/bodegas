﻿import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { HttpServicio } from "../http.servicio"
import { IngresoDetalle, IngresoProductoCaracteristica, IngresoResumen } from "./ingresos.modelos"
import { ProductosServicio } from "../mantenimientos/productos.servicio"
import { MarcasServicio } from "../mantenimientos/marcas.servicio"
import { UnidadesDeMedidaServicio } from "../mantenimientos/unidades-de-medida.servicio"
import { ListasServicio } from "../mantenimientos/listas.servicio"
import { ProveedoresServicio } from "../mantenimientos/proveedores.servicio"
import { BodegasServicio } from "../mantenimientos/bodegas.servicio"
import { PaginacionResultado, PaginacionParametros } from "../modelos"

@Injectable()
export class IngresosServicio {

    private url = "http://localhost:5002/api/core/ingresos/";

    constructor(private http: HttpServicio, private productos: ProductosServicio, private marcas: MarcasServicio, private unidadesDeMedida: UnidadesDeMedidaServicio, private listas: ListasServicio, private proveedores: ProveedoresServicio, private bodegas: BodegasServicio) {
    }

    public obtenerProductos() {
        return this.productos.obtenerTodos();
    }

    public obtenerMarcas(productoId: number) {
        return this.marcas.obtenerPorProducto(productoId);
    }

    public obtenerUnidades(productoId: number) {
        return this.unidadesDeMedida.obtenerPorProducto(productoId);
    }

    public obtenerProveedores() {
        return this.proveedores.obtenerTodos();
    }

    public obtenerBodegas() {
        return this.bodegas.obtenerTodas();
    }

    public obtenerCaracteristicas(productoId: number): Observable<IngresoProductoCaracteristica[]> {
        let url = `http://localhost:5002/api/core/productos/${productoId}/caracteristicas`;
        return this.http.get(url).map(x => x.json() as IngresoProductoCaracteristica[]);
    }

    public guardar(ingreso: IngresoDetalle) : Observable<IngresoDetalle> {
        return this.http.post(this.url, ingreso).map(x => x.json() as IngresoDetalle);
    }

    public obtenerTodos(paginacion?: PaginacionParametros): Observable<PaginacionResultado<IngresoResumen>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, { search: params }).map(x => x.json() as PaginacionResultado<IngresoResumen>);
    }
}