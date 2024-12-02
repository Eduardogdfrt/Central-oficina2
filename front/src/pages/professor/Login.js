import React, {useState} from "react";
<<<<<<< Updated upstream
=======
import { useNavigate } from 'react-router-dom';
>>>>>>> Stashed changes
import "../../pages/professor/Home.css"
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

<<<<<<< Updated upstream
=======
    const navigate = useNavigate(); 

    const handleClick = () => {

        navigate('/workshops');
    };

>>>>>>> Stashed changes
    return (
        <div className="page">
            <Header title="CADASTRO"/>
            <div className="content login">
                <Title text="LOGIN" fontSize="3.5rem" margin="0px"/>
                <p className="text">Faça login para utilizar a plataforma</p>
                <div className="inputs">
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
<<<<<<< Updated upstream
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                    />
                </div>
                <Button text="ENTRAR"/>
=======
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                    />
                </div>
                <Button text="ENTRAR" onClick={handleClick}/>
>>>>>>> Stashed changes
            </div>
        </div>
    
    );
};

export default Login;
