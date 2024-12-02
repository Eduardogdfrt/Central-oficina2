import React, {useState} from "react";
<<<<<<< Updated upstream
import "../../pages/professor/Home.css"
=======
import { useNavigate } from 'react-router-dom';
import "../../pages/aluno/Home.css"
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
                <p className="text">Cadastre-se pra acessar a plataforma e registrar sua presença</p>
                <div className="inputs">
                    <p className="text no-width">Nome completo</p>
                    <Input
                    type="email"
                    placeholder="Digite seu nome"
<<<<<<< Updated upstream
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
=======
                    value={nomeCompleto}
                    onChange={(e) => setNomeCompleto(e.target.value)}
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                    onChange={(e) => setEmail(e.target.value)}
                    customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                    />
                </div>
                <Button text="ENTRAR"/>
=======
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

export default Cadastro;

