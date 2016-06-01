import { Producto } from "./egresos.modelos"


export interface Traslado {
    fecha?: Date;
    productos?: Producto[];
}