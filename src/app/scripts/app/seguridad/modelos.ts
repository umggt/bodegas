export interface UsuarioResumen {
    id: number;
    login: string;
    nombre: string;
    correo: string;
}

export interface Usuario {
    id?: number;
    login?: string;
    nombre?: string;
    correo?: string;
}