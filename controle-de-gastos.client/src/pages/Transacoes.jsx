import React, { useState, useEffect } from 'react';
import { TransacaoService, PessoaService, CategoriaService } from '../services/api';

/**
 * Componente para gerenciamento de Transações.
 * Inclui validações de idade e finalidade de categoria.
 */
const Transacoes = () => {
    const [transacoes, setTransacoes] = useState([]);
    const [pessoas, setPessoas] = useState([]);
    const [categorias, setCategorias] = useState([]);
    const [form, setForm] = useState({
        descricao: '',
        valor: '',
        tipo: 0,
        categoriaId: '',
        pessoaId: ''
    });

    useEffect(() => {
        carregarDados();
    }, []);

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

    const pessoasMap = Object.fromEntries(
        pessoas.map(p => [String(p.id), p.nome])
    );

    const tipoTexto = t => (t === 0 ? 'Despesa' : 'Receita');

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Validação local de idade
        const pessoa = pessoas.find(p => String(p.id) === String(form.pessoaId));
        if (pessoa && pessoa.idade < 18 && form.tipo === 1) {
            alert(`${pessoa.nome} é menor de idade e não pode ter receitas.`);
            return;
        }

        // Validação local de categoria
        const categoria = categorias.find(c => String(c.id) === String(form.categoriaId));
        if (categoria && categoria.finalidade !== 'Ambas' && categoria.finalidade !== form.tipo) {
            alert(`A categoria "${categoria.descricao}" é exclusiva para ${tipoTexto(categoria.finalidade)}.`);
            return;
        }

        try {
            console.log(form)
            await TransacaoService.criar(form);
            setForm({ descricao: '', valor: '', tipo: 'Despesa', categoriaId: '', pessoaId: '' });
            carregarDados();
        } catch (error) {
            alert(error.response?.data || 'Erro ao criar transação');
        }
    };

    return (
        <div>
            <h2>Cadastro de Transações</h2>
            <form onSubmit={handleSubmit}>
                <input
                    placeholder="Descrição"
                    value={form.descricao}
                    onChange={e => setForm({ ...form, descricao: e.target.value })}
                    maxLength={400} required
                />
                <input
                    type="number"
                    placeholder="Valor"
                    value={form.valor}
                    onChange={e => setForm({ ...form, valor: e.target.value })}
                    min="0.01" step="0.01" required
                />
                <select value={form.tipo} onChange={e => setForm({ ...form, tipo: Number(e.target.value) })}>
                    <option value={0}>Despesa</option>
                    <option value={1}>Receita</option>
                </select>
                <select value={form.pessoaId} onChange={e => setForm({ ...form, pessoaId: e.target.value })} required>
                    <option value="">Selecione a Pessoa</option>
                    {pessoas.map(p => <option key={p.id} value={p.id}>{p.nome}</option>)}
                </select>
                <select value={form.categoriaId} onChange={e => setForm({ ...form, categoriaId: e.target.value })} required>
                    <option value="">Selecione a Categoria</option>
                    {categorias.map(c => <option key={c.id} value={c.id}>{c.descricao}</option>)}
                </select>
                <button type="submit">Lançar</button>
            </form>

            <table>
                <thead>
                    <tr>
                        <th>Descrição</th>
                        <th>Valor</th>
                        <th>Tipo</th>
                        <th>Pessoa</th>
                    </tr>
                </thead>
                <tbody>
                    {transacoes.map(t => (
                        <tr key={t.id}>
                            <td>{t.descricao}</td>
                            <td>R$ {t.valor}</td>
                            <td>{t.tipo === 0 ? 'Despesa' : 'Receita'}</td>
                            <td>{pessoasMap[String(t.pessoaId)]}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Transacoes;
