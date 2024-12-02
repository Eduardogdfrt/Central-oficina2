import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../../pages/professor/Home.css";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";
import axios from "axios";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(""); // Estado para erros
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.get(`http://localhost:5000/Professor/login?professorId=${email}&password=${password}`);
      
      if (response.status === 200 && response.data.success) {
        console.log("Login Successful:", response.data);
        navigate("/workshops");
      } else {
        setError("Login falhou. Verifique suas credenciais.");
      }
    } catch (err) {
      console.error("Erro na solicitação:", err);
      if (err.response) {
        // A solicitação foi feita e o servidor respondeu com um status fora do intervalo de 2xx
        setError(`Erro: ${err.response.data.message}`);
      } else if (err.request) {
        // A solicitação foi feita, mas nenhuma resposta foi recebida
        setError("Erro de rede. Verifique sua conexão.");
      } else {
        // Algo aconteceu na configuração da solicitação que acionou um erro
        setError("Erro ao configurar a solicitação.");
      }
    }
  };

  return (
    <div className="page">
      <Header title="CADASTRO" />
      <div className="content login">
        <Title text="LOGIN" fontSize="3.5rem" margin="0px" />
        <p className="text">Faça login para utilizar a plataforma</p>
        <div className="inputs">
          <form onSubmit={handleLogin}>
            <p className="text no-width">Id do Professor</p>
            <Input
              type="text"
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
            {error && <p style={{ color: "red" }}>{error}</p>}
            <Button text="ENTRAR" type="submit" />
          </form>
        </div>
      </div>
    </div>
  );
};

export default Login;
