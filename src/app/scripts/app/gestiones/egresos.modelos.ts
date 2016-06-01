export interface EgresoResumen {
    id: number;
    bodega: string;
    fecha: Date;
}

export interface EgresoDetalle
{
    id?: number;
    bodegaId?: number;
    productos?: Producto[];
    fecha?: Date;

}

export interface Producto {
    productoId?: number;
    productoNombre?: string;
    unidadId?: number;
    unidadNombre?: string;
    cantidad?: number;
    marcaId?: number;
}