export interface EgresoResumen {
    id: number;
    bodega: string;
    fecha: Date;
}

export interface EgresoDetalle
{
    id?: number;
    bodegaId?: number;
    productos: Producto;

}

export interface Producto {
    id?: number;
    nombre?: string;
    descripcion?: string;
    unidadDeMedidaId?: number;
    cantidad?: number;
}