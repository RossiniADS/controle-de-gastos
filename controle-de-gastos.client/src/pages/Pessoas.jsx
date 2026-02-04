import React, { useState, useEffect } from 'react';
import { PessoaService } from '../services/api';

/**
 * Componente para gerenciamento de Pessoas.
 * Realiza listagem, criação, edição e deleção.
 */
const Pessoas = () => {
  const [pessoas, setPessoas] = useState([]);
  const [form, setForm] = useState({ nome: '', idade: '' });
  const [editandoId, setEditandoId] = useState(null);

  useEffect(() => {
    carregarPessoas();
  }, []);

  const carregarPessoas = async () => {
    const response = await PessoaService.listar();
    setPessoas(response.data);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (editandoId) {
      await PessoaService.editar(editandoId, form);
    } else {
      await PessoaService.criar(form);
    }
    setForm({ nome: '', idade: '' });
    setEditandoId(null);
    carregarPessoas();
  };

  const handleDeletar = async (id) => {
    if (window.confirm('Ao deletar esta pessoa, todas as suas transações serão apagadas. Confirmar?')) {
      await PessoaService.deletar(id);
      carregarPessoas();
    }
  };

  return (
    <div>
      <h2>Cadastro de Pessoas</h2>
      <form onSubmit={handleSubmit}>
        <input 
          placeholder="Nome" 
          value={form.nome} 
          onChange={e => setForm({...form, nome: e.target.value})} 
          maxLength={200}
          required 
        />
        <input 
          type="number" 
          placeholder="Idade" 
          value={form.idade} 
          onChange={e => setForm({...form, idade: e.target.value})} 
          required 
        />
        <button type="submit">{editandoId ? 'Atualizar' : 'Cadastrar'}</button>
      </form>

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
                <button onClick={() => { setForm(p); setEditandoId(p.id); }}>Editar</button>
                <button onClick={() => handleDeletar(p.id)}>Deletar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Pessoas;
