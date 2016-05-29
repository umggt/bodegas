export interface ProductoResumen {
    id: number;
    nombre: string;
    descripcion: string;
}

export interface Producto {
    id?: number;
    nombre: string;
    descripcion: string;
    caracteristicas?: Caracteristica[]
}

export interface TipoCaracteristica {
    id: number;
    nombre: string;
}

export interface Caracteristica {
    id?: number;
    nombre: string;
    tipo: number;
    tipoNombre: string;
    requerido: boolean;
    listaId: number;
    minimo: number;
    maximo: number;
    expresion: string;
}