import axios from 'axios';

/**
 * Instância base do Axios.
 * Centraliza a configuração da API
 */
const api = axios.create({
    baseURL: 'https://localhost:7167/api',
});

/**
 * Serviço responsável pelas operações relacionadas a Pessoas.
 */
export const PessoaService = {

    /**
     * Retorna a lista de pessoas cadastradas
     */
    listar: () => api.get('/pessoas'),

    /**
     * Cria uma nova pessoa
     * @param {Object} dados
     */
    criar: (dados) => api.post('/pessoas', dados),

    /**
     * Atualiza os dados de uma pessoa existente
     * @param {number} id
     * @param {Object} dados
     */
    editar: (id, dados) => api.put(`/pessoas/${id}`, dados),

    /**
     * Remove uma pessoa pelo ID
     * Importante: no backend essa operação também remove
     * as transações associadas à pessoa
     */
    deletar: (id) => api.delete(`/pessoas/${id}`),

    /**
     * Retorna os totais consolidados de receitas, despesas
     * e saldo agrupados por pessoa
     */
    obterTotais: () => api.get('/pessoas/totais'),
};

/**
 * Serviço responsável pelas operações relacionadas a Categorias.
 */
export const CategoriaService = {

    /**
     * Retorna a lista de categorias cadastradas
     */
    listar: () => api.get('/categorias'),

    /**
     * Cria uma nova categoria
     * @param {Object} dados
     */
    criar: (dados) => api.post('/categorias', dados),

    /**
     * Retorna os totais consolidados agrupados por categoria
     */
    obterTotais: () => api.get('/categorias/totais'),
};

/**
 * Serviço responsável pelas operações relacionadas a Transações.
 */
export const TransacaoService = {

    /**
     * Retorna a lista de transações cadastradas
     */
    listar: () => api.get('/transacoes'),

    /**
     * Cria uma nova transação
     * @param {Object} dados
     */
    criar: (dados) => api.post('/transacoes', dados),
};

export default api;
