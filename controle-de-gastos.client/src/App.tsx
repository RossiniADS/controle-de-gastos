import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import Pessoas from './pages/Pessoas';
import Categorias from './pages/Categorias';
import Transacoes from './pages/Transacoes';
import Consultas from './pages/Consultas';


function App() {
    return (
        <Router>
            <div className="App">
                <Navbar />
                <main style={{ padding: '0 1rem' }}>
                    <Routes>
                        <Route path="/" element={<Pessoas />} />
                        <Route path="/categorias" element={<Categorias />} />
                        <Route path="/transacoes" element={<Transacoes />} />
                        <Route path="/consultas" element={<Consultas />} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
}

export default App;