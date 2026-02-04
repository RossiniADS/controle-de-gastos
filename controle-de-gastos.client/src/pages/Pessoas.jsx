import React, { useState, useEffect } from 'react';
import { PessoaService } from '../services/api';

/**
 * Componente responsável pelo gerenciamento de Pessoas.
 * Permite:
 *  - Listar pessoas cadastradas
 *  - Criar uma nova pessoa
 *  - Editar dados de uma pessoa existente
 *  - Deletar uma pessoa (com confirmação)
 */
const Pessoas = () => {

    /**
     * Estado que armazena a lista de pessoas retornadas da API
     */
    const [pessoas, setPessoas] = useState([]);

    /**
     * Estado do formulário de cadastro/edição.
     * O mesmo formulário é reaproveitado para criar e editar.
     */
    const [form, setForm] = useState({
        nome: '',
        idade: ''
    });

    /**
     * Armazena o ID da pessoa que está sendo editada.
     * Quando for null, o formulário está em modo de criação.
     */
    const [editandoId, setEditandoId] = useState(null);

    /**
     * useEffect executado na montagem do componente.
     * Responsável por carregar a lista inicial de pessoas.
     */
    useEffect(() => {
        carregarPessoas();
    }, []);

    /**
     * Busca a lista de pessoas na API
     * e atualiza o estado para renderização na tela.
     */
    const carregarPessoas = async () => {
        const response = await PessoaService.listar();
        setPessoas(response.data);
    };

    /**
     * Evento de submit do formulário.
     * Decide se irá criar ou editar com base no estado editandoId.
     */
    const handleSubmit = async (e) => {
        e.preventDefault();

        if (editandoId) {
            // Atualiza a pessoa existente
            await PessoaService.editar(editandoId, form);
        } else {
            // Cria uma nova pessoa
            await PessoaService.criar(form);
        }

        // Reseta o formulário e volta para o modo de criação
        setForm({ nome: '', idade: '' });
        setEditandoId(null);

        // Atualiza a lista após a operação
        carregarPessoas();
    };

    /**
     * Responsável por deletar uma pessoa.
     * Exibe uma confirmação, pois a exclusão também remove
     * todas as transações associadas a essa pessoa.
     */
    const handleDeletar = async (id) => {
        const confirmar = window.confirm(
            'Ao deletar esta pessoa, todas as suas transações serão apagadas. Confirmar?'
        );

        if (confirmar) {
            await PessoaService.deletar(id);
            carregarPessoas();
        }
    };

    return (
        <div>
            <h2>Cadastro de Pessoas</h2>

            {/* Formulário de criação/edição */}
            <form onSubmit={handleSubmit}>
                <input
                    placeholder="Nome"
                    value={form.nome}
                    onChange={e =>
                        setForm({ ...form, nome: e.target.value })
                    }
                    maxLength={200}
                    required
                />

                <input
                    type="number"
                    placeholder="Idade"
                    value={form.idade}
                    onChange={e =>
                        setForm({ ...form, idade: e.target.value })
                    }
                    required
                />

                {/* Texto do botão muda conforme o modo */}
                <button type="submit">
                    {editandoId ? 'Atualizar' : 'Cadastrar'}
                </button>
            </form>

            {/* Tabela de pessoas cadastradas */}
            <table>
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Idade</th>
                        <th>Ações</th>
                    </tr>
                </thead>

                <tbody>
                    {pessoas.map(p => (
                        <tr key={p.id}>
                            <td>{p.nome}</td>
                            <td>{p.idade}</td>
                            <td>
                                {/* Ao editar, preenche o formulário e ativa o modo edição */}
                                <button onClick={() => { setForm(p); setEditandoId(p.id); }}>
                                    Editar
                                </button>

                                <button onClick={() => handleDeletar(p.id)}>
                                    Deletar
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Pessoas;
