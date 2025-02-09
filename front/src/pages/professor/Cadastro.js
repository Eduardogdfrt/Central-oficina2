import React, { useState } from "react";
import axios from "axios";
import "../../pages/professor/Home.css";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";

const Cadastro = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [specialty, setSpecialty] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [professorId, setProfessorId] = useState(null);

  const handleCadastro = async (e) => {
    e.preventDefault();

    
    const professorData = {
      professorId: 0,  
      name: name,
      email: email,
      password: password,
      specialty: specialty,
    };

    console.log("Dados enviados:", professorData);

    try {
      const response = await axios.post(
        "http://localhost:5000/Professor/add",  
        professorData
      );
      console.log("Resposta completa do servidor:", response);
      console.log("Dados do servidor:", response.data); 

      if (response.status === 201) {
        const { professorId, name } = response.data;  
        console.log("ID recebido:", professorId);
        setProfessorId(professorId);  
        setSuccess(`Cadastro realizado com sucesso! Seu ID é ${professorId}.`);
        setError("");
      } else {
        setError("Erro ao realizar o cadastro. Tente novamente.");
      }
    } catch (err) {
      console.error("Erro na solicitação:", err);
      if (err.response) {
        setError(`Erro: ${err.response.data.message}`);
      } else if (err.request) {
        setError("Erro de rede. Verifique sua conexão.");
      } else {
        setError("Erro ao configurar a solicitação.");
      }
    }
  };

  return (
    <div className="page">
      <Header title="CADASTRO" />
      <div className="content">
        <Title text="CADASTRO" fontSize="3.5rem" margin="0px" />
        <p className="text">Cadastre-se para acessar a plataforma</p>
        <div className="inputs">
          <form onSubmit={handleCadastro} className="form-container">
            <p className="text no-width">Nome completo</p>
            <Input
              type="text"
              placeholder="Digite seu nome"
              value={name}
              onChange={(e) => setName(e.target.value)}
              customStyle={{
                marginBottom: "10px",
                padding: "10px",
                fontSize: "16px",
              }}
            />
            <p className="text no-width">Email</p>
            <Input
              type="email"
              placeholder="Digite seu e-mail"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              customStyle={{
                marginBottom: "10px",
                padding: "10px",
                fontSize: "16px",
              }}
            />
            <p className="text no-width">Senha</p>
            <Input
              type="password"
              placeholder="Digite sua senha"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              customStyle={{
                marginBottom: "10px",
                padding: "10px",
                fontSize: "16px",
              }}
            />
            <p className="text no-width">Especialidade</p>
            <Input
              type="text"
              placeholder="Digite sua especialidade"
              value={specialty}
              onChange={(e) => setSpecialty(e.target.value)}
              customStyle={{
                marginBottom: "10px",
                padding: "10px",
                fontSize: "16px",
              }}
            />
            {error && <p style={{ color: "red" }}>{error}</p>}
            {success && (
              <div>
                <p style={{ color: "green" }}>{success}</p>
                {professorId && <p>Seu ID é: {professorId}</p>}
              </div>
            )}
            <Button text="CADASTRAR" type="submit" />
          </form>
        </div>
      </div>
    </div>
  );
};

export default Cadastro;