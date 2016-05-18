export interface Dictionary<U> {
    [index: string]: U
}

export interface PaginacionBase {
    pagina?: number,
    totalPaginas?: number,
    totalElementos?: number,
    cantidadElementos?: number,
    elementosPorPagina?: number,
    paginaAnterior?: number,
    paginaSiguiente?: number,
    paginas?: number[]
}

export interface PaginacionParametros {
    pagina?: number,
    elementosPorPagina?: number,
    ordenamiento?: Dictionary<boolean>
}

export interface PaginacionResultado<T> extends PaginacionBase {
    elementos?: T[]
}