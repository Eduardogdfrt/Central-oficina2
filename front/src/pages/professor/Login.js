import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../../pages/professor/Home.css";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";
import axios from "axios";
import { useUser } from "../../contexts/UserContext";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(""); 
  const navigate = useNavigate();
  const { setUserId } = useUser();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      let isProfessor = false;
      let loginUrl = "";

      if (!email.includes('@')) {
        // caso o login seja feito com o ID do professor
        isProfessor = true;
        loginUrl = `http://localhost:5000/Professor/login?professorId=${email}&password=${password}`;
      } else {
        // caso o login seja feito com o e-mail do aluno
        loginUrl = `http://localhost:5000/Student/login?email=${email}&password=${password}`;
      }

      const response = await axios.get(loginUrl);

      if (response.status === 200) {
        console.log("Login Successful:", response.data);
        const userId = isProfessor ? response.data.professorId : response.data.studentId;
        
        setUserId(userId);

        if (isProfessor) {
          navigate("/workshops"); 
        } else {
          navigate("/workshops-aluno", { state: { studentId: userId } }); 
        }
      } else {
        setError("Login falhou. Verifique suas credenciais.");
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

    const  handleRegister = () => {

      navigate("/aluno-cadastro");
    };

  return (
    <div className="page">
      <Header title="CADASTRO" />
      <div className="content login">
        <Title text="LOGIN" fontSize="3.5rem" margin="0px" />
        <p className="text">Faça login para utilizar a plataforma</p>
        <div className="inputs">
          <form onSubmit={handleLogin} className="form-container">
            <p className="text no-width">ID do Professor ou Email</p>
            <Input
              type="text"
              placeholder="Informe o ID do professor ou seu e-mail"
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
            <Button text="CADASTRO ALUNO" onClick={handleRegister} />
          </form>
        </div>
      </div>
    </div>
  );
};

export default Login;