import React, { useState, useEffect } from 'react';
import { PessoaService, CategoriaService } from '../services/api';

/**
 * Componente para exibição de relatórios de totais.
 * Lista totais por pessoa e por categoria com resumo geral.
 */
const Consultas = () => {
  const [totaisPessoa, setTotaisPessoa] = useState({ lista: [], resumoGeral: {} });
  const [totaisCategoria, setTotaisCategoria] = useState({ lista: [], resumoGeral: {} });

  useEffect(() => {
    carregarConsultas();
  }, []);

  const carregarConsultas = async () => {
    const [p, c] = await Promise.all([
      PessoaService.obterTotais(),
      CategoriaService.obterTotais()
    ]);
    setTotaisPessoa(p.data);
    setTotaisCategoria(c.data);
  };

  return (
    <div>
      <h2>Consulta de Totais por Pessoa</h2>
      <table>
        <thead>
          <tr>
            <th>Pessoa</th>
            <th>Receitas</th>
            <th>Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>
        <tbody>
          {totaisPessoa.lista.map((item, index) => (
            <tr key={index}>
              <td>{item.nome}</td>
              <td>R$ {item.totalReceitas}</td>
              <td>R$ {item.totalDespesas}</td>
              <td>R$ {item.saldo}</td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr>
            <th>TOTAL GERAL</th>
            <th>R$ {totaisPessoa.resumoGeral.totalGeralReceitas}</th>
            <th>R$ {totaisPessoa.resumoGeral.totalGeralDespesas}</th>
            <th>R$ {totaisPessoa.resumoGeral.saldoLiquido}</th>
          </tr>
        </tfoot>
      </table>

      <hr />

      <h2>Consulta de Totais por Categoria</h2>
      <table>
        <thead>
          <tr>
            <th>Categoria</th>
            <th>Receitas</th>
            <th>Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>
        <tbody>
          {totaisCategoria.lista.map((item, index) => (
            <tr key={index}>
              <td>{item.descricao}</td>
              <td>R$ {item.totalReceitas}</td>
              <td>R$ {item.totalDespesas}</td>
              <td>R$ {item.saldo}</td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr>
            <th>TOTAL GERAL</th>
            <th>R$ {totaisCategoria.resumoGeral.totalGeralReceitas}</th>
            <th>R$ {totaisCategoria.resumoGeral.totalGeralDespesas}</th>
            <th>R$ {totaisCategoria.resumoGeral.saldoLiquido}</th>
          </tr>
        </tfoot>
      </table>
    </div>
  );
};

export default Consultas;
