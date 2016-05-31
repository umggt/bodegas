﻿import { ListaValor } from "../mantenimientos/listas.modelos"

export interface IngresoResumen {
    
}

export interface IngresoDetalle {
    fecha: Date;
    proveedorId?: number;
    productos?: IngresoProducto[];
}

export interface IngresoProducto {
    productoId?: number;
    nombre?: string;
    unidadId?: number;
    unidadNombre?: string;
    cantidad?: number;
    precio?: number;
    marcaId?: number;
    serie?: string;
}

export interface IngresoProductoCaracteristica {
    nombre: string;
    listaValorId: number;
    esLista: boolean;
    valores: ListaValor[];
    valor: any;
    minimo: number;
    maximo: number;
    esNumero: boolean;
    requerido: boolean;
    esMoneda: boolean;
    esTextoCorto: boolean;
    esTextoLargo: boolean;
    esBooleano: boolean;
}