import { Dictionary } from "../modelos"

export interface ListaResumen {
    id: number;  
    nombre: string;
}

export interface Lista {
    id?: number;
    nombre?: string;    
    valores?: ListaValor[];    
}

export interface ListaValor {
    id?: number;
    valor: string;
}