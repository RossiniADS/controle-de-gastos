import React, { useState, useEffect } from 'react';
import { CategoriaService } from '../services/api';

/**
 * Componente responsável pelo gerenciamento de Categorias.
 * Aqui é possível:
 *  - Listar todas as categorias cadastradas
 *  - Criar uma nova categoria informando descrição e finalidade
 */
const Categorias = () => {

    /**
     * Estado que armazena a lista de categorias retornadas da API
     */
    const [categorias, setCategorias] = useState([]);

    /**
     * Estado do formulário de cadastro.
     * - descricao: texto digitado pelo usuário
     * - finalidade: tipo da categoria (Despesa, Receita ou Ambas)
     */
    const [form, setForm] = useState({
        descricao: '',
        finalidade: 'Ambas'
    });

    /**
     * useEffect executado apenas uma vez na montagem do componente.
     * Ele é responsável por carregar as categorias já cadastradas.
     */
    useEffect(() => {
        carregarCategorias();
    }, []);

    /**
     * Busca a lista de categorias na API
     * e atualiza o estado para renderização na tela.
     */
    const carregarCategorias = async () => {
        const response = await CategoriaService.listar();
        setCategorias(response.data);
    };

    /**
     * Mapeamento utilizado para converter o valor textual
     * selecionado no formulário para o valor numérico esperado pelo backend.
     */
    const finalidadeMap = {
        Despesa: 0,
        Receita: 1,
        Ambas: 2
    };

    /**
     * Mapeamento inverso usado apenas para exibição na tela.
     * Converte o valor numérico vindo da API para um texto amigável.
     */
    const finalidadeLabel = {
        0: 'Despesa',
        1: 'Receita',
        2: 'Ambas'
    };

    /**
     * Evento disparado ao submeter o formulário.
     * Responsável por montar o payload, enviar para a API
     * e atualizar a listagem após o cadastro.
     */
    const handleSubmit = async (e) => {
        e.preventDefault();

        // Objeto no formato esperado pela API
        const payload = {
            descricao: form.descricao,
            finalidade: finalidadeMap[form.finalidade]
        };

        // Chamada para criação da categoria
        await CategoriaService.criar(payload);

        // Limpa o formulário após o cadastro
        setForm({ descricao: '', finalidade: 'Ambas' });

        // Recarrega a lista para refletir o novo registro
        carregarCategorias();
    };

    return (
        <div>
            <h2>Cadastro de Categorias</h2>

            {/* Formulário de cadastro */}
            <form onSubmit={handleSubmit}>
                <input
                    placeholder="Descrição"
                    value={form.descricao}
                    onChange={e =>
                        setForm({ ...form, descricao: e.target.value })
                    }
                    maxLength={400}
                    required
                />

                <select value={form.finalidade} onChange={e => setForm({ ...form, finalidade: e.target.value })}>
                    <option value="Despesa">Despesa</option>
                    <option value="Receita">Receita</option>
                    <option value="Ambas">Ambas</option>
                </select>

                <button type="submit">Cadastrar</button>
            </form>

            {/* Listagem das categorias cadastradas */}
            <ul>
                {
                    categorias.map(c => (
                        <li key={c.id}>
                            {c.descricao} ({finalidadeLabel[c.finalidade]})
                        </li>
                    ))
                }
            </ul>
        </div>
    );
};

export default Categorias;
