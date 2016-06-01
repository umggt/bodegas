import { ListaValor } from "../mantenimientos/listas.modelos"

export interface IngresoResumen {
    
}

export interface IngresoDetalle {
    id?: number;
    fecha: Date;
    proveedorId?: number;
    bodegaId?: number;
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
    caracteristicas?: IngresoProductoCaracteristica[]
}

export interface IngresoProductoCaracteristica {
    id: number;
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