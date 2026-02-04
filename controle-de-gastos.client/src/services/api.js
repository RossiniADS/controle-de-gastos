import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7167/api',
});

export const PessoaService = {
    listar: () => api.get('/pessoas'),
    criar: (dados) => api.post('/pessoas', dados),
    editar: (id, dados) => api.put(`/pessoas/${id}`, dados),
    deletar: (id) => api.delete(`/pessoas/${id}`),
    obterTotais: () => api.get('/pessoas/totais'),
};

export const CategoriaService = {
    listar: () => api.get('/categorias'),
    criar: (dados) => api.post('/categorias', dados),
    obterTotais: () => api.get('/categorias/totais'),
};

export const TransacaoService = {
    listar: () => api.get('/transacoes'),
    criar: (dados) => api.post('/transacoes', dados),
};

export default api;
