export interface IBodega {
    id: number;
    nombre: string;
}

export interface IBodegaResumen {
    id: number;
    nombre: string;
}

export interface IOpcionDeMenu {
    id: number;
    titulo: string;
    ruta: string;
    opciones: IOpcionDeMenu[];
}