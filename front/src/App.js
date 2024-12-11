import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./pages/professor/Login";
import Cadastro from "./pages/professor/Cadastro";
import Home from "./pages/professor/Home";
import Workshops from "./pages/professor/Workshops";
import WorkshopCadastro from "./pages/professor/WorkshopCadastro";
import AlunoLogin from "./pages/aluno/Login"
import AlunoCad from "./pages/aluno/Cadastro"

const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/cadastro" element={<Cadastro />} />
                <Route path="/workshops" element={<Workshops />} />
                <Route path="/workshop-cadastro" element={<WorkshopCadastro />} />
                <Route path="/aluno-login" element={<AlunoLogin />} />
                <Route path="/aluno-cadastro" element={<AlunoCad />} />
            </Routes>
        </Router>
    );
};

export default App;
