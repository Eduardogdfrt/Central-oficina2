import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../../pages/professor/Home.css";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";
import axios from "axios";

const WorkshopCadastro = () => {
    const [step, setStep] = useState(1); 
    const [name, setName] = useState('');
    const [date, setDate] = useState('');
    const [professorId, setProfessorId] = useState('');
    const [helperId, setHelperId] = useState('');
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    const navigate = useNavigate(); 

    const nextStep = () => setStep(step + 1);
    const prevStep = () => setStep(step - 1);

    const cadWorkshops = async (e) => {
        e.preventDefault();

        const formattedDate = new Date(date).toISOString(); 

        const workshopData = {
            name,
            data: formattedDate,
            professorId,
            helperId
        };

        console.log(workshopData); 
    
        try {
            const response = await axios.post("https://centraloficina2-327755630538.us-central1.run.app/api/Workshop/add", workshopData);
            
            if (response.status === 201) {
                setSuccess("Cadastro do workshop realizado com sucesso!");
                setError("");
                navigate("/workshops");
            } else {
                setError("Erro ao realizar o cadastro. Tente novamente.");
            }
        } catch (err) {
            console.error("Erro na solicitação:", err);
            setError(err.response?.data?.message || "Erro desconhecido");
        }
    };

    return (
        <div className="page">
            <Header title="CADASTRO" />
            <div className="content login">
                <Title text="WORKSHOPS" fontSize="3.5rem" margin="0px" />
                <p className="text">Cadastrar novos Workshops na plataforma</p>

                <div className="inputs">
                    <form className="form-container" onSubmit={step === 3 ? cadWorkshops : (e) => e.preventDefault()}>
                        
                        {step === 1 && (
                            <>
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
                            </>
                        )}

                        {step === 2 && (
                            <>
                                <p className="text no-width">ID do Professor Instrutor</p>
                                <Input
                                    type="text"
                                    placeholder="Digite o ID do instrutor"
                                    value={professorId}
                                    onChange={(e) => setProfessorId(e.target.value)}
                                    customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                                />
                            </>
                        )}

                        {step === 3 && (
                            <>
                                <p className="text no-width">IDs dos Alunos (separados por vírgula)</p>
                                <Input
                                    type="text"
                                    placeholder="Ex: 101, 102, 103"
                                    value={helperId}
                                    onChange={(e) => setHelperId(e.target.value)}
                                    customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
                                />
                            </>
                        )}

                        {error && <p style={{ color: "red" }}>{error}</p>}
                        {success && <p style={{ color: "green" }}>{success}</p>}

                        <div className="buttons">
                            {step > 1 && <Button text="Voltar" onClick={prevStep} type="button" />}
                            {step < 3 && <Button text="Próximo" onClick={nextStep} type="button" />}
                            {step === 3 && <Button text="Cadastrar" type="submit" />}
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default WorkshopCadastro;
