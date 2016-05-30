export interface ProveedorResumen {
    id?: number;
    nombre: string;
    nombreContacto: string;
}

export interface ProveedorDetalle {
    id?: number;
    nombre: string;
    nombreDeContacto: string;
    direccion: string;
    telefonos?: ProveedorTelefono[];
}

export interface ProveedorTelefono {
    idProveedor?: number;
    telefono: number;
}
