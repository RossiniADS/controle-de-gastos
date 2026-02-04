import React, { useState, useEffect } from 'react';
import { TransacaoService, PessoaService, CategoriaService } from '../services/api';

/**
 * Componente responsável pelo gerenciamento de Transações.
 */
const Transacoes = () => {

    /**
     * Lista de transações cadastradas
     */
    const [transacoes, setTransacoes] = useState([]);

    /**
     * Lista de pessoas, utilizada para seleção no formulário
     * e exibição do nome na listagem
     */
    const [pessoas, setPessoas] = useState([]);

    /**
     * Lista de categorias disponíveis para associação à transação
     */
    const [categorias, setCategorias] = useState([]);

    /**
     * Estado do formulário de cadastro de transações
     */
    const [form, setForm] = useState({
        descricao: '',
        valor: '',
        tipo: 0, // 0 = Despesa | 1 = Receita
        categoriaId: '',
        pessoaId: ''
    });

    /**
     * useEffect executado na montagem do componente.
     * Carrega todas as informações necessárias para a tela:
     *  - Transações
     *  - Pessoas
     *  - Categorias
     */
    useEffect(() => {
        carregarDados();
    }, []);

    /**
     * Busca os dados necessários na API.
     * As requisições são feitas em paralelo para melhorar performance.
     */
    const carregarDados = async () => {
        const [t, p, c] = await Promise.all([
            TransacaoService.listar(),
            PessoaService.listar(),
            CategoriaService.listar()
        ]);

        setTransacoes(t.data);
        setPessoas(p.data);
        setCategorias(c.data);
    };

    /**
     * Mapeamento auxiliar para exibir o nome da pessoa
     * a partir do ID na listagem de transações.
     * Evita fazer buscas repetidas no array durante o render.
     */
    const pessoasMap = Object.fromEntries(
        pessoas.map(p => [String(p.id), p.nome])
    );

    /**
     * Mapeamento auxiliar para exibir o nome da categoria
     * a partir do ID na listagem de transações.
     * Evita fazer buscas repetidas no array durante o render.
     */
    const categoriasMap = Object.fromEntries(
        categorias.map(c => [String(c.id), c.descricao])
    );


    /**
     * Converte o tipo numérico da transação em texto,
     * utilizado principalmente nas mensagens de validação.
     */
    const tipoTexto = t => {
        switch (t) {
            case 0: return 'Despesa';
            case 1: return 'Receita';
            case 2: return 'Ambas';
            default: return '';
        }
    };

    /**
     * Evento de submit do formulário.
     * Contém validações de regra de negócio antes do envio para a API.
     */
    const handleSubmit = async (e) => {
        e.preventDefault();

        /**
         * Validação de idade:
         * Pessoas menores de 18 anos não podem ter receitas.
         */
        const pessoa = pessoas.find(
            p => String(p.id) === String(form.pessoaId)
        );

        if (pessoa && pessoa.idade < 18 && form.tipo === 1) {
            alert(`${pessoa.nome} é menor de idade e não pode ter receitas.`);
            return;
        }

        /**
         * Validação de finalidade da categoria:
         * A categoria precisa ser compatível com o tipo da transação
         * ou ter finalidade "Ambas".
         */
        const categoria = categorias.find(
            c => String(c.id) === String(form.categoriaId)
        );

        if (
            categoria &&
            categoria.finalidade !== 2 && 
            categoria.finalidade !== form.tipo
        ) {
            alert(
                `A categoria "${categoria.descricao}" é exclusiva para ${tipoTexto(categoria.finalidade)}.`
            );
            return;
        }

        try {
            // Cria a transação na API
            await TransacaoService.criar(form);

            // Limpa o formulário após o cadastro
            setForm({
                descricao: '',
                valor: '',
                tipo: 0,
                categoriaId: '',
                pessoaId: ''
            });

            // Recarrega os dados para atualizar a listagem
            carregarDados();
        } catch (error) {
            // Tratamento simples de erro retornado pela API
            alert(error.response?.data || 'Erro ao criar transação');
        }
    };

    return (
        <div>
            <h2>Cadastro de Transações</h2>

            {/* Formulário de lançamento de transações */}
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

                <input
                    type="number"
                    placeholder="Valor"
                    value={form.valor}
                    onChange={e =>
                        setForm({ ...form, valor: e.target.value })
                    }
                    min="0.01"
                    step="0.01"
                    required
                />

                <select
                    value={form.tipo}
                    onChange={e =>
                        setForm({ ...form, tipo: Number(e.target.value) })
                    }
                >
                    <option value={0}>Despesa</option>
                    <option value={1}>Receita</option>
                </select>

                <select
                    value={form.pessoaId}
                    onChange={e =>
                        setForm({ ...form, pessoaId: e.target.value })
                    }
                    required
                >
                    <option value="">Selecione a Pessoa</option>
                    {pessoas.map(p => (
                        <option key={p.id} value={p.id}>
                            {p.nome}
                        </option>
                    ))}
                </select>

                <select
                    value={form.categoriaId}
                    onChange={e =>
                        setForm({ ...form, categoriaId: e.target.value })
                    }
                    required
                >
                    <option value="">Selecione a Categoria</option>
                    {categorias.map(c => (
                        <option key={c.id} value={c.id}>
                            {c.descricao}
                        </option>
                    ))}
                </select>

                <button type="submit">Lançar</button>
            </form>

            {/* Listagem das transações cadastradas */}
            <table>
                <thead>
                    <tr>
                        <th>Pessoa</th>
                        <th>Descrição</th>
                        <th>Categoria</th>
                        <th>Tipo</th>
                        <th>Valor</th>
                    </tr>
                </thead>

                <tbody>
                    {transacoes.map(t => (
                        <tr key={t.id}>
                            <td>{pessoasMap[String(t.pessoaId)]}</td>
                            <td>{t.descricao}</td>
                            <td>{categoriasMap[String(t.categoriaId)]}</td>
                            <td>{t.tipo === 0 ? 'Despesa' : 'Receita'}</td>
                            <td>R$ {t.valor}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Transacoes;
