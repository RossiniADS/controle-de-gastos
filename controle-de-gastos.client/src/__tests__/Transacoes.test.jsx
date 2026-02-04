import React from 'react';
import { describe, expect, it, vi, beforeEach, afterEach } from 'vitest';
import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';

import Transacoes from '../pages/Transacoes';
import { CategoriaService, PessoaService, TransacaoService } from '../services/api';

vi.mock('../services/api', () => ({
    PessoaService: {
        listar: vi.fn(),
    },
    CategoriaService: {
        listar: vi.fn(),
    },
    TransacaoService: {
        listar: vi.fn(),
        criar: vi.fn(),
    },
}));

const pessoasFixture = [
    { id: 'pessoa-1', nome: 'Ana', idade: 16 },
    { id: 'pessoa-2', nome: 'Bruno', idade: 22 },
];

const categoriasFixture = [
    { id: 'cat-1', descricao: 'Salário', finalidade: 1 },
    { id: 'cat-2', descricao: 'Alimentação', finalidade: 0 },
];

describe('Transacoes', () => {
    beforeEach(() => {
        PessoaService.listar.mockResolvedValue({ data: pessoasFixture });
        CategoriaService.listar.mockResolvedValue({ data: categoriasFixture });
        TransacaoService.listar.mockResolvedValue({ data: [] });
        TransacaoService.criar.mockResolvedValue({});
        vi.spyOn(window, 'alert').mockImplementation(() => {});
    });

    afterEach(() => {
        vi.restoreAllMocks();
    });

    it('impede receita para pessoas menores de idade', async () => {
        render(<Transacoes />);

        await waitFor(() => {
            expect(screen.getByRole('option', { name: 'Ana' })).toBeInTheDocument();
        });

        await userEvent.type(screen.getByPlaceholderText('Descrição'), 'Receita teste');
        await userEvent.type(screen.getByPlaceholderText('Valor'), '100');
        await userEvent.selectOptions(screen.getAllByRole('combobox')[0], ['1']);
        await userEvent.selectOptions(screen.getAllByRole('combobox')[1], ['pessoa-1']);
        await userEvent.selectOptions(screen.getAllByRole('combobox')[2], ['cat-1']);

        await userEvent.click(screen.getByRole('button', { name: 'Lançar' }));

        expect(window.alert).toHaveBeenCalledWith('Ana é menor de idade e não pode ter receitas.');
        expect(TransacaoService.criar).not.toHaveBeenCalled();
    });

    it('impede categoria incompatível com o tipo da transação', async () => {
        render(<Transacoes />);

        await waitFor(() => {
            expect(screen.getByRole('option', { name: 'Bruno' })).toBeInTheDocument();
        });

        await userEvent.type(screen.getByPlaceholderText('Descrição'), 'Receita teste');
        await userEvent.type(screen.getByPlaceholderText('Valor'), '100');
        await userEvent.selectOptions(screen.getAllByRole('combobox')[0], ['1']);
        await userEvent.selectOptions(screen.getAllByRole('combobox')[1], ['pessoa-2']);
        await userEvent.selectOptions(screen.getAllByRole('combobox')[2], ['cat-2']);

        await userEvent.click(screen.getByRole('button', { name: 'Lançar' }));

        expect(window.alert).toHaveBeenCalledWith('A categoria "Alimentação" é exclusiva para Despesa.');
        expect(TransacaoService.criar).not.toHaveBeenCalled();
    });
});
