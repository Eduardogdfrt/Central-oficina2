<<<<<<< Updated upstream
import React, {useState} from "react";
import "../../pages/professor/Home.css"
=======
import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';
import "../../pages/professor/Home.css";
>>>>>>> Stashed changes
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";

const Cadastro = () => {
<<<<<<< Updated upstream
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

=======
    const [nomeCompleto, setNomeCompleto] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate(); 

    const handleClick = () => {

        navigate('/login');
    };

>>>>>>> Stashed changes
    return (
        <div className="page">
            <Header title="LOGIN"/>
            <div className="content login">
                <Title text="CADASTRO" fontSize="3.5rem" margin="0px"/>
                <p className="text">Cadastre-se pra acessar a plataforma</p>
                <div className="inputs">
                    <p className="text no-width">Nome completo</p>
                    <Input
<<<<<<< Updated upstream
                    type="email"
                    placeholder="Digite seu nome"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                    />
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
                    onChange={(e) => setEmail(e.target.value)}
                    customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                    />
                </div>
                <Button text="ENTRAR"/>
            </div>
        </div>
    
=======
                        type="text"
                        placeholder="Digite seu nome"
                        value={nomeCompleto}
                        onChange={(e) => setNomeCompleto(e.target.value)}
                        customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                    />
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
                </div>
                <Button text="ENTRAR" onClick={handleClick}/>
            </div>
        </div>
>>>>>>> Stashed changes
    );
};

export default Cadastro;
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
