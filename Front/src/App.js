import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./pages/professor/Login";
import Cadastro from "./pages/professor/Cadastro";
import Home from "./pages/professor/Home";
import Workshops from "./pages/professor/Workshops";
import WorkshopCadastro from "./pages/professor/WorkshopCadastro";

const App = () => {
    return (
        <Router>
            <Routes>
<<<<<<< Updated upstream
                <Route path="/" element={<Login />} />
                <Route path="/cadastro" element={<Cadastro />} />
                <Route path="/home" element={<Home />} />
=======
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/cadastro" element={<Cadastro />} />
>>>>>>> Stashed changes
                <Route path="/workshops" element={<Workshops />} />
                <Route path="/workshop-cadastro" element={<WorkshopCadastro />} />
            </Routes>
        </Router>
    );
};

export default App;
