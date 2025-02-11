import React, { useState } from "react";
import "../../pages/professor/Home.css";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";
import axios from "axios";

const Cadastro = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [birthDate, setBirthDate] = useState(""); 
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const handleCadastro = async (e) => {
    e.preventDefault();
    const studentData = {
      name: name,
      email: email,
      password: password,
      birthDate: birthDate, 
    };

    console.log(studentData);

    try {
      const response = await axios.post("http://localhost:5000/Student/add", studentData);

      if (response.status === 201) {
        setSuccess("Cadastro realizado com sucesso!");
        setError("");
        setName("");
        setEmail("");
        setPassword("");
        setBirthDate("");
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
      <Header title="LOGIN" />
      <div className="content login">
        <Title text="CADASTRO" fontSize="3.5rem" margin="0px" />
        <p className="text">Cadastre-se para acessar a plataforma e registrar sua presença</p>
        <div className="inputs">
          <form onSubmit={handleCadastro}>
            <p className="text no-width">Nome completo</p>
            <Input
              type="text"
              placeholder="Digite seu nome"
              value={name}
              onChange={(e) => setName(e.target.value)}
              customStyle={{ marginBottom: "10px", padding: "10px", fontSize: "16px" }}
            />
            <p className="text no-width">Email</p>
            <Input
              type="email"
              placeholder="Digite seu e-mail"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              customStyle={{ marginBottom: "10px", padding: "10px", fontSize: "16px" }}
            />
            <p className="text no-width">Senha</p>
            <Input
              type="password"
              placeholder="Digite sua senha"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              customStyle={{ marginBottom: "10px", padding: "10px", fontSize: "16px" }}
            />
            <p className="text no-width">Data de Nascimento</p>
            <Input
              type="date" 
              value={birthDate}
              onChange={(e) => setBirthDate(e.target.value)}
              customStyle={{ marginBottom: "10px", padding: "10px", fontSize: "16px" }}
            />
            {error && <p style={{ color: "red" }}>{error}</p>}
            {success && <p style={{ color: "green" }}>{success}</p>}
            <Button text="CADASTRAR" type="submit" />
          </form>
        </div>
      </div>
    </div>
  );
};

export default Cadastro;
