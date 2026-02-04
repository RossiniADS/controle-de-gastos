import React, { useState, useEffect } from 'react';
import { PessoaService, CategoriaService } from '../services/api';

/**
 * Componente responsável pela exibição de consultas e relatórios.
 * Aqui são apresentados:
 *  - Totais de receitas, despesas e saldo agrupados por pessoa
 *  - Totais de receitas, despesas e saldo agrupados por categoria
 *  - Um resumo geral ao final de cada tabela
 */
const Consultas = () => {

    /**
     * Estado que armazena os totais agrupados por pessoa.
     * Estrutura esperada:
     * {
     *   lista: [],
     *   resumoGeral: {}
     * }
     */
    const [totaisPessoa, setTotaisPessoa] = useState({
        lista: [],
        resumoGeral: {}
    });

    /**
     * Estado que armazena os totais agrupados por categoria.
     * Segue o mesmo padrão do estado de pessoas
     */
    const [totaisCategoria, setTotaisCategoria] = useState({
        lista: [],
        resumoGeral: {}
    });

    /**
     * useEffect executado apenas na montagem do componente.
     * Responsável por disparar a carga inicial das consultas.
     */
    useEffect(() => {
        carregarConsultas();
    }, []);

    /**
     * Método responsável por buscar os dados de consulta na API.
     * Utiliza Promise.all para executar as duas requisições em paralelo,
     * melhorando o tempo de resposta da tela.
     */
    const carregarConsultas = async () => {
        const [p, c] = await Promise.all([
            PessoaService.obterTotais(),
            CategoriaService.obterTotais()
        ]);

        // Atualiza os estados com os dados retornados pela API
        setTotaisPessoa(p.data);
        setTotaisCategoria(c.data);
    };

    return (
        <div>
            <h2>Consulta de Totais por Pessoa</h2>

            {/* Tabela de totais agrupados por pessoa */}
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

                {/* Rodapé da tabela com o resumo geral */}
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

            {/* Tabela de totais agrupados por categoria */}
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

                {/* Resumo geral das categorias */}
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
