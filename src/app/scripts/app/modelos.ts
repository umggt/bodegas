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

export interface PaginacionResultado<T> extends PaginacionBase {
    elementos?: T[]
}