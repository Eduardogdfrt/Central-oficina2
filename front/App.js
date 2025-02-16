import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./pages/professor/Login";
import Cadastro from "./pages/professor/Cadastro";
import Home from "./pages/professor/Home";
import Workshops from "./pages/professor/Workshops";
import WorkshopCadastro from "./pages/professor/WorkshopCadastro";
import AlunoLogin from "./pages/aluno/Login"
import AlunoCad from "./pages/aluno/Cadastro"
import Certificado from "./pages/professor/Certificado";
import WorkshopDetails from "./pages/professor/WorkshopDetails";
import WorkshopsAluno from "./pages/aluno/WorkshopsAluno";
import { UserProvider } from "./contexts/UserContext";
import { AuthProvider } from "./contexts/AuthContext";
import GerarCertificado from "./pages/professor/GerarCertificado";

const App = () => {
    return (
        <AuthProvider>
        <UserProvider>
            <Router>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/cadastro" element={<Cadastro />} />
                    <Route path="/workshops" element={<Workshops />} />
                    <Route path="/workshops-aluno" element={<WorkshopsAluno />} />
                    <Route path="/workshop-cadastro" element={<WorkshopCadastro />} />
                    <Route path="/workshop/:id" element={<WorkshopDetails />} />
                    <Route path="/gerar-certificado" element={<Certificado />} />
                    <Route path="/certificado" element={<GerarCertificado />} />
                    <Route path="/aluno-login" element={<AlunoLogin />} />
                    <Route path="/aluno-cadastro" element={<AlunoCad />} />
                </Routes>
            </Router>'
        </UserProvider>
        </AuthProvider>
    );
};

export default App;
