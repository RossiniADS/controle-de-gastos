import React from 'react';
import { Link } from 'react-router-dom';

/**
 * Componente de navegação principal da aplicação.
 * Responsável por permitir o acesso às principais telas do sistema.
 */
const Navbar = () => {
  return (
    <nav style={{ padding: '1rem', backgroundColor: '#f0f0f0', marginBottom: '1rem' }}>
      <ul style={{ listStyle: 'none', display: 'flex', gap: '1rem', margin: 0, padding: 0 }}>
        <li><Link to="/">Pessoas</Link></li>
        <li><Link to="/categorias">Categorias</Link></li>
        <li><Link to="/transacoes">Transações</Link></li>
        <li><Link to="/consultas">Consultas</Link></li>
      </ul>
    </nav>
  );
};

export default Navbar;
