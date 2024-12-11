import React, {useState} from "react";
import { useNavigate } from "react-router-dom";
import "../../pages/professor/Home.css"
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";
import axios from "axios";

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(""); 
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.get(`https://centraloficina2-hml.azurewebsites.net/Student/login?email=${email}&password=${password}`);
      
      if (response.status === 200) {
        console.log("Login Successful:", response.data);
        navigate("/workshops");
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

    return (
        <div className="page">
            <Header title="CADASTRO"/>
            <div className="content login">
                <Title text="LOGIN" fontSize="3.5rem" margin="0px"/>
                <p className="text">Faça login para registrar sua presença</p>
                <div className="inputs">
                    <form onSubmit={handleLogin}>
                    <p className="text no-width">Email</p>
                    <Input
                    type="email"
                    placeholder="Digite seu e-mail"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                    />
                    <p className="text no-width">Senha</p>
                    <Input
                    type="password"
                    placeholder="Digite sua senha"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                    />
                <Button text="ENTRAR" type="submit"/>
                </form>
                </div>
            </div>
        </div>
    
    );
};

export default Login;
