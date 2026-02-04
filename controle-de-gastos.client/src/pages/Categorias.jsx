import React, { useState, useEffect } from 'react';
import { CategoriaService } from '../services/api';

/**
 * Componente para gerenciamento de Categorias.
 * Realiza listagem e criação.
 */
const Categorias = () => {
    const [categorias, setCategorias] = useState([]);
    const [form, setForm] = useState({ descricao: '', finalidade: 'Ambas' });

    useEffect(() => {
        carregarCategorias();
    }, []);

    const carregarCategorias = async () => {
        const response = await CategoriaService.listar();
        setCategorias(response.data);
    };

    const finalidadeMap = {
        Despesa: 0,
        Receita: 1,
        Ambas: 2
    };

    const finalidadeLabel = {
        0: 'Despesa',
        1: 'Receita',
        2: 'Ambas'
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            descricao: form.descricao,
            finalidade: finalidadeMap[form.finalidade]
        };

        await CategoriaService.criar(payload);

        setForm({ descricao: '', finalidade: 'Ambas' });
        carregarCategorias();
    };

    return (
        <div>
            <h2>Cadastro de Categorias</h2>
            <form onSubmit={handleSubmit}>
                <input
                    placeholder="Descrição"
                    value={form.descricao}
                    onChange={e => setForm({ ...form, descricao: e.target.value })}
                    maxLength={400}
                    required
                />
                <select
                    value={form.finalidade}
                    onChange={e => setForm({ ...form, finalidade: e.target.value })}
                >
                    <option value="Despesa">Despesa</option>
                    <option value="Receita">Receita</option>
                    <option value="Ambas">Ambas</option>
                </select>
                <button type="submit">Cadastrar</button>
            </form>

            <ul>
                {categorias.map(c => (
                    <li key={c.id}>
                        {c.descricao} ({finalidadeLabel[c.finalidade]})
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Categorias;
