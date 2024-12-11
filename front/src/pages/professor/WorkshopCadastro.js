import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';
import "../../pages/aluno/Home.css";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";
import axios from "axios";

const WorkshopCadastro = () => {
    const [name, setName] = useState('');
    const [date, setDate] = useState('');
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    const navigate = useNavigate(); 

    const cadWorkshops = async (e) => {
        e.preventDefault();


        const formattedDate = new Date(date).toISOString(); 

        const workshopData = {
          name: name,
          data: formattedDate,
        };

        console.log(workshopData); 
    
        try {
          const response = await axios.post(
            "https://centraloficina2-hml.azurewebsites.net/api/Workshop", 
            workshopData
          );
          
          if (response.status === 201) {
            setSuccess("Cadastro do workshop realizado com sucesso!");
            setError("");
            navigate('/workshops');
          } else {
            setError("Erro ao realizar o cadastro. Tente novamente.");
          }
        } catch (err) {
          console.error("Erro na solicitação:", err);
          if (err.response) {
            setError(`Erro: ${err.response.data.message || 'Erro desconhecido'}`);
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
                <Title text="WORKSHOPS" fontSize="3.5rem" margin="0px"/>
                <p className="text">Cadastrar novos Workshops na plataforma</p>
                <div className="inputs">
                    <form onSubmit={cadWorkshops}>
                        <p className="text no-width">Nome</p>
                        <Input
                            type="text"
                            placeholder="Digite o nome"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                        />
                        <p className="text no-width">Data</p>
                        <Input
                            type="date" 
                            value={date}
                            onChange={(e) => setDate(e.target.value)}
                            customStyle={{ marginBottom: "10px", padding: "10px", fontSize: "16px" }}
                        />
                        {error && <p style={{ color: "red" }}>{error}</p>}
                        {success && <p style={{ color: "green" }}>{success}</p>}
                        <Button text="CADASTRAR" type="submit"/>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default WorkshopCadastro;
